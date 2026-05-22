using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public int damage = 20;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto que toca es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            // Busca el script de vida del jugador (ajusta según tu nombre de script)
            // Por ejemplo, si tienes un HealthManager o similar:
            var health = other.gameObject.GetComponent<GhostHealth>(); 
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}