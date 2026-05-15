using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public static RoomBehaviour Instance;

    //commom
    private bool failed;
    private bool challengeActive;

    //timer challenge
    private float timeLimit;

    //del health nada hasta que tenga el dano del jugador

    //enemies challenge
    private int currentKills;
    private int numberOfEnemies;

    void Start()
    {
        if (MapManager.Instance != null && MapManager.Instance.currentRoomData != null)
        {
            if (MapManager.Instance.currentRoomData.roomType == RoomType.Challenge) 
            {
                challengeActive = true;

                timeLimit = RoomManager.Instance.timeLimit;

                if (MapManager.Instance.currentChallenge == ObjectiveRoom.health)
                {
                    // subscribe to player health system
                }
            }
        }

        if (RoomManager.Instance.finalDoor != null)
        {
            RoomManager.Instance.finalDoor.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Update()
    {
        if(!challengeActive || failed) return;

        if(MapManager.Instance.currentChallenge == ObjectiveRoom.time)
        {
            ExecuteTimerBehaviour();
        }
    }

    private void ExecuteTimerBehaviour()
    {
        timeLimit -= Time.deltaTime;
        if(timeLimit <= 0)
        {
            FailChallenge();
        }
    }

    private void ExecuteHealthBehaviour()
    {
        if(MapManager.Instance.currentChallenge == ObjectiveRoom.health)
        {
            FailChallenge();
        }
    }

    private void ExecuteEnemiesBehaviour()
    {
        if(MapManager.Instance.currentChallenge == ObjectiveRoom.enemies)
        {
            currentKills++;
            if (currentKills >= numberOfEnemies)
            {
                WinChallenge();
            }
        }
    }

    private void FailChallenge()
    {
        if (RoomManager.Instance.finalDoor != null)
        {
            RoomManager.Instance.finalDoor.GetComponent<SpriteRenderer>().enabled = true;
        }
        failed = true;
        RoomManager.Instance.playerToFinalDoor();
        Debug.Log("Reto fallido");
    }

    private void WinChallenge()
    {
        if (RoomManager.Instance.finalDoor != null)
        {
            RoomManager.Instance.finalDoor.GetComponent<SpriteRenderer>().enabled = true;
        }
        challengeActive = false;
        Debug.Log("Reto superado");
    }

}
