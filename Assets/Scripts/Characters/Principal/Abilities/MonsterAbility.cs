using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAbility : AbilityBase
{
    public GameObject tsunami;
    public override void PerformAbility()
    {
        GameObject t = Instantiate(tsunami, transform.position, Quaternion.identity);

        if (transform.localScale.x < 0)
        {
            Vector3 scale = t.transform.localScale;
            scale.x *= -1;
            t.transform.localScale = scale;
        }
    }
}
