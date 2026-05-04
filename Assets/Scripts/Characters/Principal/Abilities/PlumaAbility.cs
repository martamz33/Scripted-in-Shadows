using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumaAbility : AbilityBase
{
    public GameObject pluma;
    public override void PerformAbility()
    {
        GameObject p = Instantiate(pluma, transform.position, Quaternion.identity);

        p.transform.SetParent(this.transform);
    }
}
