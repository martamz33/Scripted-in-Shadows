using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Room Progression")]
    public int roomsCleared = 0;
    public int roomsBeforeBoss = 10;

    [Header("Normal Room Layouts")]
    public List<string> normalRoomScenes;
    private string lastLayoutUsed = "";

    [Header("Challenge Room Layouts (6 Escenas en total)")]
    [Tooltip("Pon aquí las 2 escenas del reto de Tiempo")]
    public List<string> timeChallengeScenes;
    
    [Tooltip("Pon aquí las 2 escenas del reto de No Daño")]
    public List<string> noDamageChallengeScenes;
    
    [Tooltip("Pon aquí las 2 escenas del reto de Matar Enemigos")]
    public List<string> killEnemiesChallengeScenes;    

    [Header("Current Run Data")]
    public RoomData currentRoomData;
    public ObjectiveRoom currentChallenge = ObjectiveRoom.None;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public void StartFirstRandomRoom()
    {
        roomsCleared = 0;
        if(GameManager.Instance != null) GameManager.Instance.lastRoomWasBoss = false;

        string sceneToLoad = GetRandomNormalLayout();
        lastLayoutUsed = sceneToLoad;

        SceneManager.LoadScene(sceneToLoad);
    }

    private string GetRandomNormalLayout()
    {
        if(normalRoomScenes.Count == 0) return "";

        // Create the temporal list without the last room
        List<string> availableLayouts = new List<string>();
        foreach(string layout in normalRoomScenes)
        {
            if(layout != lastLayoutUsed)
            {
                availableLayouts.Add(layout);
            }
        }

        int randomIndex = Random.Range(0, availableLayouts.Count);
        return availableLayouts[randomIndex];
    }

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

        currentRoomData = data;
        currentChallenge = ObjectiveRoom.None;

        if(data.roomType == RoomType.Challenge)
        {
            // 1 = time, 2 = no damage, 3 = killEnemies
            currentChallenge = (ObjectiveRoom)Random.Range(1,4);

            string sceneToLoad = "";

            switch (currentChallenge)
            {
                case ObjectiveRoom.time:
                    if (timeChallengeScenes.Count > 0)
                        sceneToLoad = timeChallengeScenes[Random.Range(0, timeChallengeScenes.Count)];
                    break;

                case ObjectiveRoom.health:
                    if (noDamageChallengeScenes.Count > 0)
                        sceneToLoad = noDamageChallengeScenes[Random.Range(0, noDamageChallengeScenes.Count)];
                    break;

                case ObjectiveRoom.enemies:
                    if (killEnemiesChallengeScenes.Count > 0)
                        sceneToLoad = killEnemiesChallengeScenes[Random.Range(0, killEnemiesChallengeScenes.Count)];
                    break;
            }

            if(!string.IsNullOrEmpty(sceneToLoad))
            {
                lastLayoutUsed = sceneToLoad;
                SceneManager.LoadScene(sceneToLoad);
            }
        }

        else if (data.roomType == RoomType.Normal || data.roomType == RoomType.Warehouse || data.roomType == RoomType.Equipment 
            || data.roomType == RoomType.Pusecution || data.roomType == RoomType.Writer || data.roomType == RoomType.Book)
        {
            string sceneToLoad = GetRandomNormalLayout();
            lastLayoutUsed = sceneToLoad;
            SceneManager.LoadScene(sceneToLoad);
        }
        else 
        {
            if (data.sceneName != null)
            {
                lastLayoutUsed = data.sceneName;
                SceneManager.LoadScene(data.sceneName); 
            }
            else
            {
                Debug.LogError("No has asignado un roomLayoutPrefab a la sala: " + data.roomType);
            }
        }
    }
}
