using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncreease : MonoBehaviour
{
    public int healthInt = 7;
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        // Empezamos asegurándonos de que NO es trigger para que choque con el suelo
        col.isTrigger = false;
    }

    // Detectamos cuando toca el suelo (o cualquier cosa sólida al caer)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Convertimos a Trigger para que el jugador lo pueda atravesar y recoger
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Solo recogemos si YA ES Trigger (evita recogerlo mientras cae)
        if(col.isTrigger && other.gameObject.CompareTag("Player"))
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
