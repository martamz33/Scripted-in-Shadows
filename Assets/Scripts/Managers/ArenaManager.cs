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
            // 1. Select a random enemy
            EnemySpawnInfo chosenEnemyInfo = wave.enemiesToSpawn[Random.Range(0, wave.enemiesToSpawn.Length)];
            
            // 2. Search a zone that match the type of enemy
            ArenaZone compatibleZone = GetRandomCompatibleZone(chosenEnemyInfo.enemyType);

            if (compatibleZone != null)
            {
                // 3. Instantiate
                GameObject spawnedEnemy = Instantiate(chosenEnemyInfo.enemyPrefab, compatibleZone.spawnPoint.position, Quaternion.identity);

                // 4. Assign patrol points if is a walking enemy
                if (chosenEnemyInfo.enemyType == SpawnType.WalkEnemy)
                {
                    TryAssignPatrol(spawnedEnemy, compatibleZone);
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
