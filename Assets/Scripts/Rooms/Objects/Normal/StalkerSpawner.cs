using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerSpawner : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public GameObject stalkerPrefab;

    void Start()
    {
        if(MapManager.Instance != null && MapManager.Instance.currentRoomData.roomType == RoomType.Pusecution)
        {
            Instantiate(stalkerPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
