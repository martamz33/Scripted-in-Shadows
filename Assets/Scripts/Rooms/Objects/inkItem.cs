using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inkItem : MonoBehaviour
{
    public int inkValue = 2;

    private Rigidbody2D rg;
    private Collider2D col;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            rg.isKinematic = true;
            col.isTrigger = true;
            
            rg.velocity = Vector2.zero;
        }

        if(other.gameObject.CompareTag("Player"))
        {
            InkManager.instance.AddInk(inkValue);

            Destroy(gameObject);
        }
    }
}
