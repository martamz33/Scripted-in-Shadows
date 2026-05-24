using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [Header("Shop UI References")]
    public GameObject shopCanvasRef;
    public Button[] buttonsRef;
    public TextMeshProUGUI[] pricesRef;
    public Image[] iconsRef;
    public Image[] inksRef;
    public GameObject[] postersRef;

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

        if (MapManager.Instance != null && MapManager.Instance.currentRoomData != null)
        {
            RoomType type = MapManager.Instance.currentRoomData.roomType;
            
            if (type == RoomType.Shop && ShopManager.Instance != null)
            {
                // Conectamos la UI al ShopManager persistente
                ShopManager.Instance.SetUpShopUI(
                    shopCanvasRef, 
                    buttonsRef, 
                    pricesRef, 
                    iconsRef, 
                    inksRef, 
                    postersRef
                );
                
                // Generamos la tienda
                ShopManager.Instance.GenerateShopItems();
            }
        }
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

        // 1. SI ES VOLADOR: No cae
        if (enemyScript != null && enemyScript.isFlying)
        {
            TryAssignPatrol(enemy, sp);
            yield break; 
        }

        // 2. Apagamos IA
        if (enemyScript != null) enemyScript.enabled = false;

        // --- CORRECCIÓN: Buscamos el colisionador aquí para poder usarlo abajo ---
        Collider2D enemyCollider = enemy.GetComponentInChildren<Collider2D>();
        // Si tienes varios, asegúrate de que sea el que no es trigger
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
            
            // Usamos el colisionador encontrado para calcular el origen del Raycast
            Vector2 rayOrigin = (enemyCollider != null) 
                ? enemyCollider.bounds.center - new Vector3(0, enemyCollider.bounds.extents.y, 0)
                : enemy.transform.position;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.5f, groundLayer);

            if (hit.collider != null)
            {
                isGrounded = true;
            }
            yield return null;
        }

        // 3. Activamos al aterrizar
        if (enemy != null)
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Dynamic; // Mantén Dynamic
                rb.gravityScale = 1f; // Asegura que tenga gravedad
                rb.constraints = RigidbodyConstraints2D.FreezeRotation; // ¡Crucial! Evita que se caigan de lado
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

    private void OnDestroy() 
    {
        if (roomData != null && roomData.roomType == RoomType.Shop && ShopManager.Instance != null)
        {
            
            ShopManager.Instance.CloseShop(); 
        }
    }
    
}
