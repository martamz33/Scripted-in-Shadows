using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomTypeButton : MonoBehaviour
{
    public TextMeshPro roomNameText;
    private RoomData data;

    public void SetUp(RoomData roomData)
    {
        data = roomData;
        roomNameText.text = data.roomType.ToString();
    }

    public void OnClick()
    {
        MapManager.Instance.LoadSelectedRoom(data);
    }
}
