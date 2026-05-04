using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpiritOfVendetta", menuName = "Roguelike/PowerUp/SpritiOfVendetta")]
public class SpiritOfVendetta : PowerUpScriptable {
    public GameObject prefabSprit;

    public override void ApplyEffect(GameObject player)
    {
        if(!player.GetComponent<VendettaLogic>())
        {
            VendettaLogic logic = player.AddComponent<VendettaLogic>();
            logic.Setup(this);
        }
    }    
    
}