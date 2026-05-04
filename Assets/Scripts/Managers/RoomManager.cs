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

                TryAssignPatrol(spawnedElement, sp);
            }

            if(sp.spawnOnlyOnce)
            {
                sp.active = false;
                yield break;
            }

            yield return new WaitForSeconds(sp.delay);
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
