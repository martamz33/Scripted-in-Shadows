using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inkItem : MonoBehaviour
{
    public int inkValue = 2;

    [Header("Audio")]
    public AudioClip pickupSound;
    private Rigidbody2D rg;
    private Collider2D col;
    private bool isCollected = false; 

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rg.velocity = Vector2.zero;
        rg.angularVelocity = 0;
    }
    private void FixedUpdate() 
    {
        if (!rg.isKinematic && Mathf.Abs(rg.velocity.y) < 0.1f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
            if (hit.collider != null && hit.collider.CompareTag("Ground"))
            {
                rg.isKinematic = true;
                col.isTrigger = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            rg.isKinematic = true; 
            col.isTrigger = true;  
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            InkManager.instance.AddInk(inkValue);
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}