using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPoints 
{
    public bool active = true;
    public bool spawnOnlyOnce = true;
    public Transform point;
    public float delay = 5f;

    [Header("Enemy Pool Settings")]
    public bool isSpecific;
    public GameObject specificPrefab;       // Puede ser un Troll o una Poción
    public List<GameObject> randomPrefabs;
    
    [Header("Patrol points")]
    public Transform patrolPointA;
    public Transform patrolPointB;
}
