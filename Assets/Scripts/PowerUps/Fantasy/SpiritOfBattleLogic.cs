using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritOfBattleLogic : MonoBehaviour
{
    private SpiritOfBattle data;
    private bool isCharged = false;
    private GhostAbility ghostAbility;

    public void Setup(SpiritOfBattle config)
    {
        data = config;
        ghostAbility = GetComponent<GhostAbility>();

        if(ghostAbility != null)
        {
            ghostAbility.OnAbilitySuccessfullyUsed += SetCharged;
        }
    }

    private void OnDestroy()
    {
        if(ghostAbility != null)
        {
            ghostAbility.OnAbilitySuccessfullyUsed -= SetCharged;
        }
    }

    private void SetCharged()
    {
        isCharged = true;
    }

    void Update()
    {
        if(isCharged && Input.GetMouseButtonDown(0))
        {
            LauchEnergy();
            isCharged = false;
        }
    }

    private void LauchEnergy()
    {
        GameObject e = Instantiate(data.prefabEnergy, transform.position, transform.rotation);
        
        EnergyBlast energy = e.GetComponent<EnergyBlast>();
        if(energy != null)
        {
            energy.Setup(data);
        }
    }
}
