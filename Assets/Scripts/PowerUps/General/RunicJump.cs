using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunicJump", menuName = "Roguelike/PowerUp/RunicJump")]
public class RunicJump : PowerUpScriptable
{
    public GameObject prefabRunic;

    private Transform currentPlayerPosition;
    private Transform playerTransform;

    public override void ApplyEffect(GameObject player)
    {
        GhostMovement gm = player.GetComponent<GhostMovement>();

        if(gm != null)
        {
            playerTransform = player.transform;

            currentPlayerPosition = player.transform.Find("runicCreate");

            gm.OnJumpExecuted -= SpawnRune;

            gm.OnJumpExecuted += SpawnRune;
        }
    }

    private void SpawnRune()
    {
        if(prefabRunic != null && currentPlayerPosition != null)
        {
            GameObject rune = Instantiate(prefabRunic, currentPlayerPosition.position, Quaternion.identity);

            rune.transform.SetParent(playerTransform);

            RunicShield shield = rune.GetComponent<RunicShield>();
            if(shield != null)
            {
                shield.SetUp(damage, duration);
            }
        }
    }
}
