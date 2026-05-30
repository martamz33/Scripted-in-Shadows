using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trollAttack : MonoBehaviour
{
    private Collider2D col;
    private troll Troll;
    private bool wasAttacking = false;

    void Start()
    {
        col = GetComponent<Collider2D>();
        Troll = GetComponentInParent<troll>();
    }

    void Update()
    {
        if(Troll == null) return;
        bool currentlyAttacking = Troll.IsAttacking;
        if(currentlyAttacking && !wasAttacking)
            col.enabled = true;
        wasAttacking = currentlyAttacking;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(Troll == null) return;
        if(other.gameObject.CompareTag("Player") && Troll.IsAttacking)
        {
            other.gameObject.GetComponent<GhostHealth>().TakeDamage(Troll.damage);
            col.enabled = false;
        }
    }
}
