using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IronSkin", menuName = "Roguelike/PowerUp/IronSkin")]
public class IronSkinPowerUp : PowerUpScriptable
{
    public float healthIncreasePercentage = 0.5f;

    public override void ApplyEffect(GameObject player)
    {
        GhostHealth ghostHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<GhostHealth>();

        if(ghostHealth != null)
        {
            ghostHealth.ModifyMaxHealthPercentage(healthIncreasePercentage);

            ghostHealth.canHeal = false;

            Debug.Log("Piel de hierro aplicada");
        }
    }
}
