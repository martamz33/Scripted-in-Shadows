using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiruAttack : MonoBehaviour
{
    [Header("Settings")]
    public int damage = 4;
    public float speed = 5f;
    public float lifeTime = 4f;

    [Header("Effects")]
    public GameObject prefabImpactParticles;

    private Vector3 targetDirection;
    
    void Start()
    {
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
        if(other.CompareTag("Player"))
        {
            Debug.Log("Ha chocado");
            other.GetComponent<GhostHealth>().TakeDamage(damage);
            Exploit();
        }

        if(other.CompareTag("Ground"))
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
