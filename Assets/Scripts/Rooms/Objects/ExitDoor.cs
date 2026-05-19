using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExitDoor : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("El Canvas o Panel que contiene los botones de selección de sala")]
    public GameObject roomSelectionCanvas; 
    
    [Tooltip("Arrastra aquí los 3 botones de la UI")]
    public Button[] doorButtons; 
    
    [Tooltip("Arrastra aquí los textos de los botones (para poner el nombre de la sala)")]
    public TextMeshProUGUI[] doorTexts;

    private void Start()
    {
        if(roomSelectionCanvas != null)
        {
            roomSelectionCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.FreezePlayer(true);
            }
            else
            {
                col.GetComponent<GhostMovement>().enabled = false;
                col.GetComponent<GhostMovement>().isMovementActive = false;
            }

            if (MapManager.Instance != null)
            {
                List<RoomData> nextRooms = MapManager.Instance.GetNextRoomOptions();
                SetupUI(nextRooms);
            }
        }
    }

    private void SetupUI(List<RoomData> roomOptions)
    {
        roomSelectionCanvas.SetActive(true);

        for(int i = 0; i < doorButtons.Length; i++)
        {
            if( i < roomOptions.Count)
            {
                doorButtons[i].gameObject.SetActive(true);   

                RoomData roomToLoad = roomOptions[i];

                if (doorTexts.Length > i && doorTexts[i] != null)
                {
                    doorTexts[i].text = roomToLoad.roomType.ToString(); 
                }

                doorButtons[i].onClick.RemoveAllListeners();

                doorButtons[i].onClick.AddListener(() => OnDoorSelected(roomToLoad));
            }
            else
            {
                doorButtons[i].gameObject.SetActive(false);
            }
            
        }
    }

    private void OnDoorSelected(RoomData selectedRoom)
    {
        roomSelectionCanvas.SetActive(false);

        if(MapManager.Instance != null)
        {
            MapManager.Instance.LoadSelectedRoom(selectedRoom);
        }
    }
}
