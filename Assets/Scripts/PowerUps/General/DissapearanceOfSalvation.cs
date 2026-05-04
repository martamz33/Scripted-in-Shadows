using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DissapearanceOfSalvation", menuName = "Roguelike/PowerUp/DissapearanceOfSalvation")]
public class DissapearanceOfSalvation : PowerUpScriptable
{
    public float minTime = 32f;
    public float maxTime = 60f;

    public override void ApplyEffect(GameObject player)
    {
        GhostHealth health = player.GetComponent<GhostHealth>();

        if(health != null)
        {
            health.DissapearanceOfSalvationInvulneraty(minTime, maxTime, duration);
        }
    }
}
