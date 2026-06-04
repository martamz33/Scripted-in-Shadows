using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerAttack : MonoBehaviour
{
    [Header("Settings")]
    public int damage = 4;
    public float launchForce = 10f;
    public float upwardArc = 1.5f;

    [Header("Effects")]
    public GameObject prefabImpactParticles;
    public AudioClip explosionSound;

    private Rigidbody2D rg;
    
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void setUpLauch(float distance)
    {
        Debug.Log("Ha lanzado la bola");
        rg = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) return;

        //horizontal direction to player
        float directionX = (player.transform.position.x > transform.position.x) ? 1 : -1;

        //vector that combined the direction with an upwards arc
        float vx = distance * 0.55f; 
        float vy = upwardArc * 5f;

        Vector2 finalVelocity = new Vector2(directionX * vx, vy);

        //apply the initial force
        rg.AddForce(finalVelocity, ForceMode2D.Impulse);

        //the rock to rottate
        rg.AddTorque(Random.Range(-5f, 5f), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<GhostHealth>().TakeDamage(damage);
            }

            ExploitRock();
        }
    }

    private void ExploitRock()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        
        if(prefabImpactParticles !=null)
        {
            GameObject particles = Instantiate(prefabImpactParticles, transform.position, Quaternion.identity);
            Destroy(particles, 2f);
        }

        Destroy(gameObject);
    }
}
