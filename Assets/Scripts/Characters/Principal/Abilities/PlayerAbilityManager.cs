using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    [Header("Scripts of Abilities (Close by deafult)")]
    public AbilityBase scriptAbility1; 
    public AbilityBase scriptAbility2;
    public AbilityBase scriptAbility3;

    public void ActivateAbility(AbilityType type)
    {
        if(scriptAbility1 != null) scriptAbility1.enabled = false;
        if(scriptAbility2 != null) scriptAbility2.enabled = false;
        if(scriptAbility3 != null) scriptAbility3.enabled = false;

        switch(type)
        {
            case AbilityType.Manchas:
                if(scriptAbility1 != null) scriptAbility1.enabled = true;
                break;
            case AbilityType.Pluma:
                if(scriptAbility2 != null) scriptAbility2.enabled = true;
                break;
            case AbilityType.Monstruo:
                if(scriptAbility3 != null) scriptAbility3.enabled = true;
                break;
        }

    }
}
