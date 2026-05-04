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
        if(RoomManager.Instance.roomData.roomType == RoomType.Challenge) 
        {
            challengeActive = true;

            timeLimit = RoomManager.Instance.timeLimit;

            if(RoomManager.Instance.roomData.objectiveChallenge == ObjectiveRoom.health)
            {
                //subscribe to player health system
            }
        }

        RoomManager.Instance.finalDoor.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if(!challengeActive || failed) return;

        if(RoomManager.Instance.roomData.objectiveChallenge == ObjectiveRoom.health)
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
        if(RoomManager.Instance.roomData.objectiveChallenge == ObjectiveRoom.health)
        {
            FailChallenge();
        }
    }

    private void ExecuteEnemiesBehaviour()
    {
        if(RoomManager.Instance.roomData.objectiveChallenge == ObjectiveRoom.enemies)
        {
            WinChallenge();
        }
    }

    private void FailChallenge()
    {
        RoomManager.Instance.finalDoor.GetComponent<SpriteRenderer>().enabled = true;
        failed = true;
        RoomManager.Instance.playerToFinalDoor();
        Debug.Log("Reto fallido");
    }

    private void WinChallenge()
    {
        RoomManager.Instance.finalDoor.GetComponent<SpriteRenderer>().enabled = true;
        challengeActive = false;
        Debug.Log("Reto superado");
    }

}
