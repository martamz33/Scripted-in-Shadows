using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfBall : MonoBehaviour
{
    private Dwarf dwarf;

    void Start()
    {
        dwarf = GetComponentInParent<Dwarf>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && dwarf != null && dwarf.IsAttacking)
        {
            other.gameObject.GetComponent<GhostHealth>().TakeDamage(dwarf.damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && dwarf != null && dwarf.IsAttacking)
        {
            GhostHealth health = collision.gameObject.GetComponent<GhostHealth>();
            if(health != null) health.TakeDamage(dwarf.damage);
        }
    }
}
