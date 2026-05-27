using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Aparition Zone
[System.Serializable]
public class ArenaZone
{
    public SpawnType typeOfZone;
    public Transform spawnPoint;    
    public Transform patrolPointA;  
    public Transform patrolPointB;
}

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    public SpawnType enemyType;
    public GameObject individualSpawnVFX;
}

[System.Serializable]
public class Wave
{
    public string waveName = "Oleada 1";
    public int numberOfEnemies;
    public EnemySpawnInfo[] enemiesToSpawn; // Cambiado para usar el nuevo tipo
    public float spawnDelay = 1f;
}

public class ArenaManager : MonoBehaviour
{
    [Header("Arena Settings")]
    public ArenaZone[] spawnZones;   
    public GameObject doors; 

    public Wave[] waves;        

    [Header("Spawn Fall Settings")] 
    public LayerMask groundLayer;

    [Header("Arena Events")]    
    [Tooltip("Se ejecuta justo cuando empieza la arena (ej: cerrar puertas)")]
    public UnityEvent onArenaStart;
    
    [Tooltip("Se ejecuta cuando se elimina la última oleada (ej: abrir puertas, dar recompensa)")]
    public UnityEvent onArenaFinish;

    private int currentWaveIndex = 0;
    private bool arenaStarted = false;
    private bool isSpawning = false;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Update()
    {
        if (arenaStarted && !isSpawning)
        {
            activeEnemies.RemoveAll(enemy => enemy == null);

            if (activeEnemies.Count == 0)
            {
                StartNextWave();
            }
        }
    }

    public void StartArena()
    {
        if (arenaStarted) return;
        arenaStarted = true;
        currentWaveIndex = 0;

        onArenaStart?.Invoke();
        
        StartNextWave();
    }

    private void StartNextWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            FinishArena();
            return;
        }

        Wave currentWave = waves[currentWaveIndex];
        StartCoroutine(SpawnWave(currentWave));
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        for (int i = 0; i < wave.numberOfEnemies; i++)
        {
            EnemySpawnInfo chosenEnemyInfo = wave.enemiesToSpawn[Random.Range(0, wave.enemiesToSpawn.Length)];
            ArenaZone compatibleZone = GetRandomCompatibleZone(chosenEnemyInfo.enemyType);

            if (compatibleZone != null)
            {
                // 1. AUMENTAMOS el desvío a 2 metros para que no nazcan fusionados y se apilen
                Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0, 0);
                Vector3 finalSpawnPos = compatibleZone.spawnPoint.position + randomOffset;

                // 2. Instanciar VFX
                if(chosenEnemyInfo.individualSpawnVFX != null)
                {
                    GameObject vfx = Instantiate(chosenEnemyInfo.individualSpawnVFX, finalSpawnPos, Quaternion.identity);
                    Destroy(vfx, 2f);
                }

                // 3. Instanciar Enemigo
                GameObject spawnedEnemy = Instantiate(chosenEnemyInfo.enemyPrefab, finalSpawnPos, Quaternion.identity);
                
                // 4. ¿QUÉ TIPO DE ENEMIGO ES?
                if (chosenEnemyInfo.enemyType == SpawnType.WalkEnemy)
                {
                    // Si camina, congelamos su rotación y lo hacemos caer
                    Rigidbody2D rb = spawnedEnemy.GetComponent<Rigidbody2D>();
                    if(rb != null) rb.constraints = RigidbodyConstraints2D.FreezeRotation;

                    StartCoroutine(MakeEnemyFall(spawnedEnemy, compatibleZone));
                }
                else
                {
                    // Si es IDLE, no lo hacemos caer. Se queda exactamente donde tú lo pusiste en el mapa.
                    Rigidbody2D rb = spawnedEnemy.GetComponent<Rigidbody2D>();
                    if(rb != null)
                    {
                        rb.velocity = Vector2.zero;
                        rb.bodyType = RigidbodyType2D.Kinematic; // Kinematic hace que no le afecte la gravedad
                    }
                }

                activeEnemies.Add(spawnedEnemy);
            }
            else
            {
                Debug.LogWarning("No hay zonas de spawn configuradas para el tipo: " + chosenEnemyInfo.enemyType);
            }

            yield return new WaitForSeconds(wave.spawnDelay);
        }

        isSpawning = false;
        currentWaveIndex++;
    }

    private IEnumerator MakeEnemyFall(GameObject enemy, ArenaZone az)
    {
        if (enemy == null) yield break;

        EnemyBase enemyScript = enemy.GetComponent<EnemyBase>();
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();

        if (enemyScript != null && enemyScript.isFlying)
        {
            TryAssignPatrol(enemy, az);
            yield break; 
        }

        if (enemyScript != null) enemyScript.enabled = false;

        Collider2D enemyCollider = enemy.GetComponentInChildren<Collider2D>();
        foreach(var col in enemy.GetComponentsInChildren<Collider2D>())
        {
            if(!col.isTrigger) { enemyCollider = col; break; }
        }

        bool isGrounded = false;
        float maxFallTime = 3f; 
        float fallTimer = 0f;

        while (enemy != null && !isGrounded && fallTimer < maxFallTime)
        {
            fallTimer += Time.deltaTime;
            
            Vector2 rayOrigin = (enemyCollider != null) 
                ? (Vector2)enemyCollider.bounds.center - new Vector2(0, enemyCollider.bounds.extents.y)
                : (Vector2)enemy.transform.position;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 1f, groundLayer);

            if (hit.collider != null || (fallTimer > 0.2f && rb != null && Mathf.Abs(rb.velocity.y) < 0.05f))
            {
                isGrounded = true; 
            }
            yield return null;
        }

        if (enemy != null)
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero; 
                rb.bodyType = RigidbodyType2D.Dynamic; 
                rb.gravityScale = 1f; 
                rb.constraints = RigidbodyConstraints2D.FreezeRotation; 

                enemy.layer = LayerMask.NameToLayer("Ignore Raycast");
            }
            
            if (enemyScript != null) enemyScript.enabled = true; 
            
            TryAssignPatrol(enemy, az);
        }
    }

    private ArenaZone GetRandomCompatibleZone(SpawnType requiredType)
    {
        List<ArenaZone> validZones = new List<ArenaZone>();

        foreach (ArenaZone zone in spawnZones)
        {
            if (zone.typeOfZone == requiredType)
            {
                validZones.Add(zone);
            }
        }

        if (validZones.Count > 0)
        {
            return validZones[Random.Range(0, validZones.Count)];
        }

        return null;
    }

    private void TryAssignPatrol(GameObject element, ArenaZone az)
    {
        if(az.patrolPointA == null || az.patrolPointB == null) return;

        troll t = element.GetComponent<troll>();
        if(t != null)
        {
            t.initialPosition = az.patrolPointA;
            t.finalPosition = az.patrolPointB;
        }

        Goblin g = element.GetComponent<Goblin>();
        if(g!= null)
        {
            g.initialPosition = az.patrolPointA;
            g.finalPosition = az.patrolPointB;
        }

        GoblinLanzador gl = element.GetComponent<GoblinLanzador>();
        if(gl!= null)
        {
            gl.initialPosition = az.patrolPointA;
            gl.finalPosition = az.patrolPointB;
        }

        FlyTower ft = element.GetComponent<FlyTower>();
        if(ft!= null)
        {
            ft.initialPoint = az.patrolPointA;
            ft.finalPoint = az.patrolPointB;
        }

        NormalTower nt = element.GetComponent<NormalTower>();
        if(nt!= null)
        {
            nt.initialPosition = az.patrolPointA;
            nt.finalPosition = az.patrolPointB;
        }
    }

    private void FinishArena()
    {
        arenaStarted = false;

        onArenaFinish?.Invoke();
        Debug.Log("Arena completada.");
    }
}
