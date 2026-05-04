using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncreease : MonoBehaviour
{
    public int healthInt = 7;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GhostHealth health = other.gameObject.GetComponent<GhostHealth>();
            if(health != null)
            {
                health.Heal(healthInt);
            }

            Destroy(gameObject);
        }
    }
}
