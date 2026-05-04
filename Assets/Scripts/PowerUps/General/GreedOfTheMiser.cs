using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GreedOfTheMiser", menuName = "Roguelike/PowerUp/GreedOfTheMiser")]
public class GreedOfTheMiser : PowerUpScriptable
{
    public override void ApplyEffect(GameObject player)
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.ActivateGreed();
        }
    }
}
