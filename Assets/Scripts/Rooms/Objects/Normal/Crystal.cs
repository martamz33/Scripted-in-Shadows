using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [Header("Transform Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Settings")]
    public float delay = 2.5f;
    public GameObject teleportVFX; 

    private Vector2 destiny;
    private GhostMovement gm;
    private SpriteRenderer playerSprite;
    private Animator animator;

    private static bool isTeleporting = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(isTeleporting) return;

        if(col.CompareTag("Player"))
        {
            gm = col.GetComponent<GhostMovement>();
            playerSprite = col.GetComponent<SpriteRenderer>();
            animator = col.GetComponent<Animator>();

            if(gm!=null && playerSprite != null)
            {
                StartCoroutine(Transport());
            }
            
        }
    }
    
    IEnumerator Transport()
    {
        isTeleporting = true;

        //to evade the inercia
        Rigidbody2D rg = gm.GetComponent<Rigidbody2D>();
        if(rg!=null)
        {
            rg.velocity = Vector2.zero;
            rg.angularVelocity = 0;
            rg.isKinematic = true;
        }

        //center the player on the center of the crystal
        gm.transform.position = transform.position;

        //reproduce the particle system
        if(teleportVFX!=null)
        {
            GameObject vfx = Instantiate(teleportVFX, gm.transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        //know where to move the aplyer
        float distanceToA = Vector2.Distance(transform.position, pointA.position);
        destiny = (distanceToA < 0.2f) ? (Vector2)pointB.position : (Vector2)pointA.position;
        destiny += new Vector2(0.75f, 0);

        gm.isMovementActive = false;
        
        animator.Play("Idle", 0, 0f);
        yield return new WaitForSeconds(delay);

        playerSprite.enabled = false;

        yield return new WaitForSeconds(delay);

        gm.transform.position = destiny;
        
        if(teleportVFX!=null)
        {
            GameObject vfx = Instantiate(teleportVFX, gm.transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        Color c = playerSprite.color;
        c.a = 0;
        playerSprite.color = c;
        playerSprite.enabled = true;

        float duration = 1f;
        float currentTime = 0f;

        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            c.a = Mathf.Clamp01(currentTime/duration);
            playerSprite.color = c;
            yield return null;
        }

        if(rg !=null) rg.isKinematic = false;

        gm.isMovementActive = true;
        yield return new WaitForSeconds(0.2f);
        isTeleporting = false;
    }
}
