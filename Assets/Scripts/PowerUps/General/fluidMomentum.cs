using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "fluidMomentum", menuName = "Roguelike/PowerUp/fluidMomentum")]
public class fluidMomentum : PowerUpScriptable
{
    public override void ApplyEffect(GameObject player)
    {
        GoshtDash dash = GameObject.FindGameObjectWithTag("Player").GetComponent<GoshtDash>();

        if(dash != null)
        {
            dash.EnableDoubleDash();
        }
    }
}
