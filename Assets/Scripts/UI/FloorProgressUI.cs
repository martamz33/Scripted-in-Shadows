using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class FloorProgressUI : MonoBehaviour
{
    [Header("Configuration of the UI")]
    [Tooltip("El panel vacío que tiene el Vertical Layout Group")]
    public Transform containerNodes; 
    [Tooltip("El prefab del circulito (que solo tiene un componente Image)")]
    public GameObject prefabNode;

    [Header("State Sprites")]
    public Sprite completedSprite;
    public Sprite actualSprite;
    public Sprite pendingSprite;
    public Sprite bossSprite;

    [Header("Options")]
    [Tooltip("Si es true, la sala 1 empieza abajo y el boss arriba. Si es false, al revés.")]
    public bool startDown = true;

    void Start()
    {
        ActualizeProgressBar();
    }

    public void ActualizeProgressBar()
    {
        foreach(Transform child in containerNodes)
        {
            Destroy(child.gameObject);
        }

        int totalRooms = MapManager.Instance.roomsBeforeBoss+1;

        int actualRoom = MapManager.Instance.roomsCleared;

        for(int i = 0; i < totalRooms; i++)
        {
            GameObject newNode = Instantiate(prefabNode, containerNodes);
            Image imageNode = newNode.GetComponent<Image>();

            if(i == totalRooms-1)
            {
                imageNode.sprite = bossSprite;
                newNode.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 140);
            }
            else if(i < actualRoom)
            {
                imageNode.sprite = completedSprite;
            }
            else if(i == actualRoom)
            {
                imageNode.sprite = actualSprite;
            }
            else
            {
                imageNode.sprite = pendingSprite;
            }

            if (startDown)
            {
                newNode.transform.SetAsFirstSibling();
            }

        }
    }
}
