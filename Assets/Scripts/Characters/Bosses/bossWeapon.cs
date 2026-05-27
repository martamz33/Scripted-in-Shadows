using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossWeapon : MonoBehaviour
{
    int damage = 10;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            var healthComponent = other.gameObject.GetComponent<GhostHealth>();

            if(healthComponent != null)
            {
                healthComponent.TakeDamage(damage);
            }        
        }
    }
}
