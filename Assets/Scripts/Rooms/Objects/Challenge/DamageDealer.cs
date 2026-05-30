using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 3; // O puedes hacer que tome el valor de la Potion

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos si lo que ha entrado es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            GhostHealth playerHealth = other.gameObject.GetComponent<GhostHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}