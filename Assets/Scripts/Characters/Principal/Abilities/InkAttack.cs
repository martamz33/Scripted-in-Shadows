using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkAttack : MonoBehaviour
{
    [Header("Attack Setting")]
    public int damage = 15;
    public float speed = 5f;
    public float detectionRadious = 100f;
    public float lifeTime = 10f;

    [Header("VFX")]
    public GameObject explosionParticles;

    private Transform targetDestination;
    private float time;

    void Start()
    {
       FindNearestEnemy(); 
    }

    void Update()
    {
        if(targetDestination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDestination.position, speed * Time.deltaTime);     

            if(time >= lifeTime)
            {
                Explosion();
            }
            else
            {
                time += Time.deltaTime;
            }
        }
        else
        {
            FindNearestEnemy();
        }
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < closestDistance && distance <= detectionRadious)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        targetDestination = closestEnemy;
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);

                Explosion();

                Destroy(gameObject);
            }
        }
    }

    private void Explosion()
    {
        if(explosionParticles!= null)
        {
            GameObject vfx = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            
            Destroy(vfx, 2f);
        }
    }
}
