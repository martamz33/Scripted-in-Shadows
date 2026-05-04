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
        if(other.gameObject.CompareTag("Player") && dwarf.IsAttacking)
        {
            other.gameObject.GetComponent<GhostHealth>().TakeDamage(dwarf.damage);
        }
    }
}
