using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeDoesNotForget", menuName = "Roguelike/PowerUp/TimeDoesNotForget")]
public class TimeDoesNotForget : PowerUpScriptable
{
    public override void ApplyEffect(GameObject player)
    {
        //1.Save if we have this power
        PlayerPrefs.SetInt("TimeDoesNotForgetUnlocked", 1);
        PlayerPrefs.Save();

        if(GameManager.Instance!=null)
        {
            GameManager.Instance.ApplyTimeDoesNotForget(player);
        }
    }
}
