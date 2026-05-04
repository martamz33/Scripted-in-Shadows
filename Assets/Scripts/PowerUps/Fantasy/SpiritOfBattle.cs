using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpiritOfBattle", menuName = "Roguelike/PowerUp/SpritiOfBattle")]
public class SpiritOfBattle : PowerUpScriptable
{
    public GameObject prefabEnergy;

    public override void ApplyEffect(GameObject player)
    {
        if(!player.GetComponent<SpiritOfBattleLogic>())
        {
            SpiritOfBattleLogic l = player.AddComponent<SpiritOfBattleLogic>();
            l.Setup(this);
        }
    }
}
