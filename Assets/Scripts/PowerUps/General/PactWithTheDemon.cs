using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PactWithTheDemon", menuName = "Roguelike/PowerUp/PactWithTheDemon")]
public class PactWithTheDemon : PowerUpScriptable
{
    public float multiplier = 0.15f;

    public override void ApplyEffect(GameObject player)
    {
        GhostAttack attack = GameObject.FindGameObjectWithTag("Player").GetComponent<GhostAttack>();
        if(attack != null)
        {
            attack.damageMultiplier += multiplier;
        }

        if(GameManager.Instance != null)
        {
            GameManager.Instance.enemyDamageMultiplier += 0.15f;
        }

        Debug.Log("El multiplier de daño es: " + attack.damageMultiplier);
        Debug.Log("El multiplier de enemigo es: " + GameManager.Instance.enemyDamageMultiplier);
    }
}
