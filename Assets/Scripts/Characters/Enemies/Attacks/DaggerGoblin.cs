using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerGoblin : MonoBehaviour
{
    [Header("Settings")]
    public int damage;
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
            bool lookRight = player.transform.position.x > transform.position.x;

            targetDirection = lookRight ? Vector3.right : Vector3.left;
            float angle = lookRight ? 0 : 180;
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
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GhostHealth>().TakeDamage(damage);
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
