using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EchosOfThePast", menuName = "Roguelike/PowerUp/EchosOfThePast")]
public class EchosOfThePast : PowerUpScriptable
{
    public float probability = 0.15f;
    public float reducedProbability = 0.05f;

    private float currentProbability;
    private GhostAbility ghostAbility;

    public override void ApplyEffect(GameObject player)
    {
        ghostAbility = player.GetComponent<GhostAbility>();

        if(ghostAbility != null)
        {
            currentProbability = probability;

            ghostAbility.OnAbilitySuccessfullyUsed -= TryEcho;
            ghostAbility.OnAbilitySuccessfullyUsed += TryEcho;
        }
    }

    private void TryEcho()
    {
        if(ghostAbility == null) return;

        float roll = Random.Range(0f, 1f);
        Debug.Log($"LA probabilidad es de {roll}");

        if(roll <= currentProbability)
        {
            ghostAbility.RepeatAbility(duration);

            currentProbability = reducedProbability;
        }
        else
        {
            currentProbability = probability;
        }
    }
}
