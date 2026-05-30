using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionOrb : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 3;
    public float speed = 5f;
    public float lifeTime = 4f;

    [Header("VFX")]
    public GameObject prefabImpactParticles;

    private Vector3 targetDirection;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player!=null)
        {
            targetDirection = (player.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            Destroy(gameObject);
        }

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        GhostHealth health = other.GetComponent<GhostHealth>();

        if(health != null)
        {
            Debug.Log("Ha chocado con el cuerpo del jugador");
            health.TakeDamage(damage);
            Exploit();
        }
        else if(other.CompareTag("Ground") || other.CompareTag("Ground"))
        {
            Exploit();
        }
    }

    private void Exploit()
    {
        if(prefabImpactParticles != null)
        {
            GameObject particles = Instantiate(prefabImpactParticles, transform.position, Quaternion.identity);

            Destroy(particles, 2f);
        }

        Destroy(gameObject);
    }
}
