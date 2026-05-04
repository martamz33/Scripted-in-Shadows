using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoom", menuName = "Roguelike/SalaData")]
public class RoomData : ScriptableObject
{
    public RoomType roomType;
    public ObjectiveRoom objectiveChallenge;
    public GameObject roomLayoutPrefab;
    //falta List<Enemy> que es la lista de enemigos posibles
    public float probability;
}
