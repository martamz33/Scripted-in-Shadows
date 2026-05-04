using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trollAttack : MonoBehaviour
{
    private Collider2D col;
    private troll Troll;
    void Start()
    {
        col = GetComponent<Collider2D>();

        Troll = GetComponentInParent<troll>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player") && Troll.IsAttacking)
        {
            other.gameObject.GetComponent<GhostHealth>().TakeDamage(Troll.damage);
            col.enabled = false;
        }    
    }
}
