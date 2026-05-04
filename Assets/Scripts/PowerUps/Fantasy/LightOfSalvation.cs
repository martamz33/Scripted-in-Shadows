using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightOfSalvation", menuName = "Roguelike/PowerUp/LightOfSalvation")]
public class LightOfSalvation : PowerUpScriptable
{
    public GameObject prefabShield;

    [Header("Tiempos del Escudo")]
    public float minCooldown = 7f;  
    public float maxCooldown = 22f;  
    public float shieldDuration = 2f; 

    public override void ApplyEffect(GameObject player)
    {
        if(!player.GetComponent<lightShield>())
        {
            lightShield l = player.AddComponent<lightShield>();

            l.Setup(this);
        }
    }
}
