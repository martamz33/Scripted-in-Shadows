using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monoloito : Interactuable
{
    [Header("Recompensas Destructibles")]
    [Tooltip("El objeto que el jugador puede romper para conseguir tinta (Almacén)")]
    public GameObject breakableInkPrefab;
    
    [Tooltip("El objeto que el jugador puede romper para conseguir vida (SelfHelp / Life)")]
    public GameObject breakableHealthPrefab;
    
    [Tooltip("Dónde aparecerá el objeto destructible")]
    public Transform spawnPoint;

    private bool hasInteractuated = false;

    public override void Interact()
    {
        if(hasInteractuated) return;

        if(MapManager.Instance == null || MapManager.Instance.currentRoomData == null) return;

        hasInteractuated = true;

        if(iconInteractuable != null) iconInteractuable.SetActive(false);
        
        RoomType typeOfRoom = MapManager.Instance.currentRoomData.roomType;

        switch (typeOfRoom)
        {  
            case RoomType.Warehouse:
                SpawnBreakable(breakableInkPrefab);
                break;
            case RoomType.Life:
                SpawnBreakable(breakableHealthPrefab);
                break;
            case RoomType.Equipment:
                GiveEquipment();
                break;
            default:
                break;
        }
    }

    private void SpawnBreakable(GameObject prefab)
    {
        if(prefab != null || spawnPoint != null)
        {
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void GiveEquipment()
    {
        if(UIManager.instance != null)
        {
            UIManager.instance.OpenMenuOfGeneralPowerUps();
        }
    }
}
