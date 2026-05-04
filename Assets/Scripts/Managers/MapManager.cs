using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //singleton
    public static MapManager Instance;

    //parameter
    [Header("Room Types")]
    public List<RoomData> allPosibleRooms;
    public RoomData shopRoom;
    public RoomData bossRoom;
    public RoomData helpRoom;

    public int roomsCleared = 0;
    public int roomsBeforeBoss = 10;

    public List<RoomData> GetNextRoomOptions()
    {
        List<RoomData> selectedRooms = new List<RoomData>();

        //Is boss turn?
        if(roomsCleared == roomsBeforeBoss)
        {
            selectedRooms.Add(bossRoom);
            return selectedRooms;
        }

        //Is the boss dead?
        if(GameManager.Instance.lastRoomWasBoss)
        {
            selectedRooms.Add(helpRoom);
            return selectedRooms;
        }

        //Is the room before the boss?
        if(roomsCleared == roomsBeforeBoss-1)
        {
            selectedRooms.Add(shopRoom);
            return selectedRooms;
        }

        //the other options
        while(selectedRooms.Count < 3)
        {
            RoomData data = PickRoomProbability();
            if(notSameRooms(selectedRooms, data))  
                selectedRooms.Add(data);
        }

        return selectedRooms;
    }

    //comprobar that the options rooms not the same
    private bool notSameRooms(List<RoomData> selected, RoomData data)
    {
        foreach(RoomData room in selected)
        {
            if(data.roomType == room.roomType) return false;
        }
        return true;
    }

    //select the rooms 
    private RoomData PickRoomProbability()
    {
        float totalWeigth = 0;
        foreach (var room in allPosibleRooms)
        {
            totalWeigth += room.probability;
        }

        float randomValue = Random.Range(0, totalWeigth);
        float cumulativeWeigth = 0;

        foreach(var room in allPosibleRooms)
        {
            cumulativeWeigth += room.probability;
            if(randomValue <= cumulativeWeigth)
            {
                return room;
            }
        }

        return allPosibleRooms[0];
    }

    //transport to the ext room
    public void LoadSelectedRoom(RoomData data)
    {   roomsCleared ++;
        Debug.Log("The next room is " + data.roomType.ToString());
    }
}
