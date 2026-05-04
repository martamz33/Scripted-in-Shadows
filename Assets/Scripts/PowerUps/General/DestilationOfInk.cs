using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestilationOfInk", menuName = "Roguelike/PowerUp/DestilationOfInk")]
public class DestilationOfInk : PowerUpScriptable
{
    public float probabilityBase = 0.15f;
    public float reducedProbability = 0.05f;

    private float currentProbability;
    private float TimeToRestoreAbility = 0f;
    private GhostHealth ghostHealth;

    public override void ApplyEffect(GameObject player)
    {
        ghostHealth = player.GetComponent<GhostHealth>();

        if(ghostHealth != null)
        {
            currentProbability = probabilityBase;
            TimeToRestoreAbility = 0f;

            EnemyBase.OnEnemyDamaged -= TryHeal;
            EnemyBase.OnEnemyDamaged += TryHeal;
        }
    }

    private void TryHeal()
    {
        if(ghostHealth == null) return;

        //check the clock
        if(Time.time >= TimeToRestoreAbility)
        {
            currentProbability = probabilityBase;
        }

        //probability
        float randomProbability = Random.Range(0f, 1f);
        Debug.Log("La probabilidad es: " + randomProbability);

        if(randomProbability <= currentProbability)
        {
            ghostHealth.Heal(1);
            currentProbability = reducedProbability;
            TimeToRestoreAbility = Time.time + duration;
        }
    }

}
