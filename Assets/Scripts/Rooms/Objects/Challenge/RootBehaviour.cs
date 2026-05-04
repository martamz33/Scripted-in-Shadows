using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public GhostMovement player;

    private PlatformEffector2D effector;
    private bool playerOnPlatform;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if(playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            if(player!=null)
            {
                StartCoroutine(FallThrough());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") )
        {
            playerOnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") )
        {
            playerOnPlatform = false;
        }
    }

    private IEnumerator FallThrough()
    {
        player.SetFallingPlatform();

        effector.rotationalOffset = 180f;
        yield return new WaitForSeconds(0.5f);
        effector.rotationalOffset = 0f;
    }
}
