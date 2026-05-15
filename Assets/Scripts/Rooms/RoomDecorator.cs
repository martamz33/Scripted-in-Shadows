using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDecorator : MonoBehaviour
{
    [Header("Decorations of type of room")]
    public GameObject decorationNormal;
    public GameObject decorationWarehouse;
    public GameObject decorationEquipment;
    public GameObject decorationLife;
    public GameObject decorationWriter;
    public GameObject decorationBook;
    public GameObject decorationPersecution;

    void Start()
    {
        TurnOffAllTheRooms();

        if(MapManager.Instance != null && MapManager.Instance.currentRoomData != null)
        {
            RoomType actualType = MapManager.Instance.currentRoomData.roomType;

            switch (actualType)
            {
                case RoomType.Nomral:
                    if(decorationNormal != null) decorationNormal.SetActive(true);
                    break;
                case RoomType.Warehouse:
                    if(decorationWarehouse != null) decorationWarehouse.SetActive(true);
                    break;
                case RoomType.Equipment:
                    if(decorationEquipment != null) decorationEquipment.SetActive(true);
                    break;
                case RoomType.Life:
                    if(decorationLife != null) decorationLife.SetActive(true);
                    break;
                case RoomType.Writer:
                    if(decorationWriter != null) decorationWriter.SetActive(true);
                    break;
                case RoomType.Book:
                    if(decorationBook != null) decorationBook.SetActive(true);
                    break;
                case RoomType.Pusecution:
                    if(decorationPersecution != null) decorationPersecution.SetActive(true);
                    break;
            }
        }
    }

    private void TurnOffAllTheRooms()
    {
        if(decorationNormal != null) decorationNormal.SetActive(false);
        if(decorationWarehouse != null) decorationWarehouse.SetActive(false);
        if(decorationEquipment != null) decorationEquipment.SetActive(false);
        if(decorationLife != null) decorationLife.SetActive(false);
        if(decorationWriter != null) decorationWriter.SetActive(false);
        if(decorationBook != null) decorationBook.SetActive(false);
        if(decorationPersecution != null) decorationPersecution.SetActive(false);
    }
}
