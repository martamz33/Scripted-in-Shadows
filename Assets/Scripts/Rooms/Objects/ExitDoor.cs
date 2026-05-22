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
            // 1. Teletransportar al centro de la puerta
            col.transform.position = transform.position;

            // 2. Parar físicas y poner en Idle
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if(rb != null) rb.velocity = Vector2.zero;
            
            Animator anim = col.GetComponent<Animator>();
            if(anim != null) anim.Play("Idle"); // Asegúrate de que tu estado se llama "Idle"

            // 3. Congelar movimiento
            if (GameManager.Instance != null)
            {
                GameManager.Instance.FreezePlayer(true);
            }
            else
            {
                col.GetComponent<GhostMovement>().enabled = false;
            }

            // 4. Mostrar UI
            if (MapManager.Instance != null)
            {
                SetupUI(MapManager.Instance.GetNextRoomOptions());
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

    public void OnDoorSelected(RoomData selectedRoom)
    {
        roomSelectionCanvas.SetActive(false);

        if(MapManager.Instance != null)
        {
            MapManager.Instance.LoadSelectedRoom(selectedRoom);
        }
    }
}
