using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachasAbility : AbilityBase
{
    public List<GameObject> projectiles;
    public BoxCollider2D spawnArea;

    public override void PerformAbility()
    {
        SpawnInkClouds();
    }

    private void SpawnInkClouds()
    {
        if(spawnArea == null) return;

        Bounds bounds = spawnArea.bounds;

        for(int i = 0; i < projectiles.Count; i++)
        {
            float px = Random.Range(bounds.min.x, bounds.max.x);
            float py = Random.Range(bounds.min.y, bounds.max.y);

            Vector3 randomPos = new Vector3(px, py, transform.position.z);

            Instantiate(projectiles[i], randomPos, Quaternion.identity);
        }
    }
}
