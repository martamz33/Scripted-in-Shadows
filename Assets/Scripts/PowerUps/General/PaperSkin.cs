using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaperSkin", menuName = "Roguelike/PowerUp/PaperSkin")]
public class PaperSkin : PowerUpScriptable
{
    public float healthIncreasePercentage = -0.5f;
    public float extraHealPercentage = 0.15f;

    public override void ApplyEffect(GameObject player)
    {
        GhostHealth ghostHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<GhostHealth>();

        if(ghostHealth != null)
        {
            ghostHealth.ModifyMaxHealthPercentage(healthIncreasePercentage);

            ghostHealth.multiplier += extraHealPercentage;

            Debug.Log("Piel de hierro aplicada");
        }
    }
}
