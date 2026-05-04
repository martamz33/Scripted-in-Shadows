using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SprintOfInk", menuName = "Roguelike/PowerUp/SprintOfInk")]
public class SprintOfInk : PowerUpScriptable
{
    public GameObject prefabInk;
    public float offsetDistance = 1.5f;

    private Transform playerTransform;

    public override void ApplyEffect(GameObject player)
    {
        GoshtDash ghostDash = player.GetComponent<GoshtDash>();

        if(ghostDash != null)
        {
            playerTransform = player.transform;

            ghostDash.OnDashExecuted -= SpankInkPuddle;
            ghostDash.OnDashExecuted += SpankInkPuddle;
        }
    }

    private void SpankInkPuddle()
    {
        if(prefabInk != null && playerTransform != null)
        {
            GameObject spank = Instantiate(prefabInk, playerTransform.position, Quaternion.identity);

            spank.transform.SetParent(playerTransform);

            spank.transform.localPosition = new Vector3(-offsetDistance, 0, 0);

            InkPuddle inkPuddle = spank.GetComponent<InkPuddle>();
            if(inkPuddle != null)
            {
                inkPuddle.SetUp(damage);
            }
        }
    }
}
