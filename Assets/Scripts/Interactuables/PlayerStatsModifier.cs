using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatsModifier 
{
    public static void ModifyHealth(GameObject player, int amount)
    {
        GhostHealth health = player.GetComponent<GhostHealth>();

        if(health != null)
        {
            health.AddFlatMaxHealth(amount);
        }

        Debug.Log("La vida ha sido modificada a " + health.actualHealth);
    }

    public static void ModifyDamage(GameObject player, float amount)
    {
        GhostAttack attack = player.GetComponent<GhostAttack>();

        if(attack != null)
        {
            attack.damageMultiplier += amount;
            attack.damageMultiplier = Mathf.Max(0.1f, attack.damageMultiplier);
        }

        Debug.Log("El daño ha sido modificada a " + attack.damageMultiplier);
    }

    public static void ModifyRecharge(GameObject player, float amount)
    {
        GhostAbility ability = player.GetComponent<GhostAbility>();

        if(ability != null)
        {
            ability.totalTime += amount;
            ability.totalTime = Mathf.Max(ability.totalTime, 1f);
        }

        Debug.Log("El recargo de la habilidad ha sido modificada a " + ability.totalTime);
    }
}
