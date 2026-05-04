using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bookmark", menuName = "Roguelike/Item/Bookmark")]
public class bookMark : ScriptableObject
{
   public Sprite icon;
   public int basePrice =50;
   
   [Header("Stats")]
   public int maxHealthChange = 5;
   public float maxAttackMultiplier = 0.1f;
   public float maxRechargeChange = 1.5f;

   public string ApplyEffect(GameObject player)
    {
        int statToChange = Random.Range(0, 3);

        bool isPositive = Random.value > 0.5f; 

        string positive = isPositive ? "ha mejorado" : "ha empeorado";
        string resultApplyEffect = "";

        switch (statToChange)
        {
            case 0:
                int hAmount = Random.Range(1, maxHealthChange + 1);
                PlayerStatsModifier.ModifyHealth(player, isPositive ? hAmount : -hAmount);
                resultApplyEffect = "La Vitalidad " + positive;
                break;
            case 1:
                float aAmount = Random.Range(0.01f, maxAttackMultiplier);
                PlayerStatsModifier.ModifyDamage(player, isPositive ? aAmount : -aAmount);
                resultApplyEffect = "El ataque " + positive;
                break;
            case 2:
                float rAmount = Random.Range(0.5f, maxRechargeChange);
                PlayerStatsModifier.ModifyRecharge(player, isPositive ? rAmount : -rAmount);
                resultApplyEffect = "La Velocidad de recarga " + positive;
                break;
        }

        return resultApplyEffect;
    }
}
