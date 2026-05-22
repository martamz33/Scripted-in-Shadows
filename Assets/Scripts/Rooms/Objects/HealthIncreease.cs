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
            // 1. Convertimos a Trigger
            col.isTrigger = true;

            // 2. Apagamos la gravedad y el movimiento para que no flote ni se mueva
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0; // Esto evita que siga calculando fuerzas
                rb.bodyType = RigidbodyType2D.Kinematic; // Lo congelamos en el sitio
            }
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
