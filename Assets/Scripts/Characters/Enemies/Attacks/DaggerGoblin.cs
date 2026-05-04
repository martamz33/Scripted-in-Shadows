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
            float dirX = (player.transform.position.x > transform.position.x) ? 1f : -1f;

            targetDirection = new Vector3(dirX, 0, 0);
            float angle =(dirX > 180) ? 0 : 180;
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
