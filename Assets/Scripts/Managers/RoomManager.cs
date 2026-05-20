using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RewardZoneMapping
{
    public RoomType roomType;
    public GameObject rewardPrefab;
}

public class RoomManager : MonoBehaviour
{
    //singleton
    public static RoomManager Instance;

    [Header("TypeOfRoom")]
    public RoomData roomData;

    [Header("Reward Zone ")]
    public List<RewardZoneMapping> rewardZones;
    public bool rewardZoneSpawned = false;

    [Header("Room Settings")]
    public Transform initialDoor;
    public GameObject finalDoor;
    public Transform exitConnectionPoint;
    public List<SpawnPoints> spawnPoints;
    public float timeLimit;
    public bool isTheFirstRoom;

    [Header("Spawn Fall Settings")]
    public LayerMask groundLayer;
    public float enemyFallSpeed = 8f;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        SpawnerOfElement();

        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnRoomEntered();
        }

        PositionPlayer();

        GenerateRewardZone();
    }

    //position player
    private void PositionPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player!=null && initialDoor!=null)
        {
            player.transform.position = initialDoor.position;
            player.GetComponent<GhostMovement>().enabled = true;
            player.GetComponent<GhostMovement>().isMovementActive = true;

            if(GameManager.Instance != null && isTheFirstRoom)
            {
                GameManager.Instance.OnStartNewRun(player);
            }
        }
    }

    public void playerToFinalDoor()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player!=null && finalDoor!=null)
        {
            player.transform.position = finalDoor.transform.position;
        }
    }
    
    //to spawn different elements
    private void SpawnerOfElement()
    {
        foreach(SpawnPoints sp in spawnPoints)
        {
            StartCoroutine(InfiniteSpawn(sp));
        }
    }

    private IEnumerator InfiniteSpawn(SpawnPoints sp)
    {
        while(sp.active)
        {
            GameObject prefabToSpawn = ChooseElement(sp);

            if(prefabToSpawn != null)
            {
                GameObject spawnedElement = Instantiate(prefabToSpawn, sp.point.position, Quaternion.identity);

                StartCoroutine(MakeEnemyFall(spawnedElement, sp));
            }

            if(sp.spawnOnlyOnce)
            {
                sp.active = false;
                yield break;
            }

            yield return new WaitForSeconds(sp.delay);
        }
    }

    private IEnumerator MakeEnemyFall(GameObject enemy, SpawnPoints sp)
    {
        if (enemy == null) yield break;

        EnemyBase enemyScript = enemy.GetComponent<EnemyBase>();
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();

        // 1. SI ES VOLADOR
        if (enemyScript != null && enemyScript.isFlying)
        {
            TryAssignPatrol(enemy, sp);
            yield break; 
        }

        // 2. Apagamos la IA
        if (enemyScript != null) enemyScript.enabled = false;

        // 3. Buscar el colisionador REAL (ignorando los que son "Trigger" como áreas de visión)
        Collider2D enemyCollider = null;
        Collider2D[] colliders = enemy.GetComponentsInChildren<Collider2D>();
        foreach(Collider2D col in colliders)
        {
            if(!col.isTrigger) 
            {
                enemyCollider = col;
                break; // Encontramos el cuerpo sólido, dejamos de buscar
            }
        }

        float dynamicDistance = 0.5f; 
        bool isGrounded = false;
        
        // TIMEOUT DE SEGURIDAD: Evita bucles infinitos si algo sale mal
        float maxFallTime = 5f;
        float fallTimer = 0f;

        // Bucle hasta que toque el suelo (o pasen 5 segundos)
        while (enemy != null && !isGrounded && fallTimer < maxFallTime)
        {
            fallTimer += Time.deltaTime;
            Vector2 rayOrigin = enemy.transform.position;

            if (enemyCollider != null)
            {
                rayOrigin = enemyCollider.bounds.center;
                dynamicDistance = enemyCollider.bounds.extents.y + 0.05f; 
            }

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, dynamicDistance, groundLayer);

            if (hit.collider != null)
            {
                // ¡Tocó el suelo!
                isGrounded = true;
            }
            else
            {
                // Si es Kinematic O si es Dynamic pero su Gravity Scale es 0
                if (rb != null)
                {
                    if (rb.bodyType == RigidbodyType2D.Kinematic || (rb.bodyType == RigidbodyType2D.Dynamic && rb.gravityScale == 0))
                    {
                        enemy.transform.position += Vector3.down * enemyFallSpeed * Time.deltaTime;
                    }
                }
                else 
                {
                    // Backup por si algún enemigo no tiene Rigidbody2D
                    enemy.transform.position += Vector3.down * enemyFallSpeed * Time.deltaTime;
                }
            }

            yield return null;
        }

        // 4. Una vez en el suelo (o si el tiempo expiró), lo activamos todo
        if (enemy != null)
        {
            if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Frenar inercia
            }

            if (enemyScript != null) enemyScript.enabled = true;
            TryAssignPatrol(enemy, sp);
        }
    }

    private GameObject ChooseElement(SpawnPoints sp)
    {
        if(sp.isSpecific)
        {
            return sp.specificPrefab;
        }
        else
        {
            if(sp.randomPrefabs != null && sp.randomPrefabs.Count > 0)
            {
                int randomIndex = Random.Range(0, sp.randomPrefabs.Count);
                return sp.randomPrefabs[randomIndex];
            }
        }
        return null;
    }

    private void TryAssignPatrol(GameObject element, SpawnPoints sp)
    {
        if(sp.patrolPointA == null || sp.patrolPointB == null) return;

        troll t = element.GetComponent<troll>();
        if(t != null)
        {
            t.initialPosition = sp.patrolPointA;
            t.finalPosition = sp.patrolPointB;
        }

        Goblin g = element.GetComponent<Goblin>();
        if(g!= null)
        {
            g.initialPosition = sp.patrolPointA;
            g.finalPosition = sp.patrolPointB;
        }

        GoblinLanzador gl = element.GetComponent<GoblinLanzador>();
        if(gl!= null)
        {
            gl.initialPosition = sp.patrolPointA;
            gl.finalPosition = sp.patrolPointB;
        }

        FlyTower ft = element.GetComponent<FlyTower>();
        if(ft!= null)
        {
            ft.initialPoint = sp.patrolPointA;
            ft.finalPoint = sp.patrolPointB;
        }

        NormalTower nt = element.GetComponent<NormalTower>();
        if(nt!= null)
        {
            nt.initialPosition = sp.patrolPointA;
            nt.finalPosition = sp.patrolPointB;
        }
    }

    public void GenerateRewardZone()
    {
        if(rewardZoneSpawned) return;
        rewardZoneSpawned = true;

        GameObject prefabToSpawn = null;

        foreach(RewardZoneMapping mapping in rewardZones)
        {
            if(mapping.roomType == roomData.roomType)
            {
                prefabToSpawn = mapping.rewardPrefab;
                break;
            }
        }

        if(prefabToSpawn != null && exitConnectionPoint != null)
        {
            GameObject rewardRoom = Instantiate(prefabToSpawn);

            Transform rewardEntrance = rewardRoom.transform.Find("Entrance");

            if(rewardEntrance != null)
            {
                Vector3 offset = exitConnectionPoint.position - rewardEntrance.position;
                rewardRoom.transform.position += offset;
            }
            else
            {
               rewardRoom.transform.position = exitConnectionPoint.position; 
            }
        }
    }
        
    public void StopAllTraps()
    {
        StopAllCoroutines();
    }
    
}
