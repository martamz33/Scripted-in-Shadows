using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAttack : MonoBehaviour
{
    public float speed = 7f;
    private int damage;
    private Transform target;
    
    public void Setup(int damageSpirit)
    {
        damage = damageSpirit;
        FindNearestEnemy();

        Destroy(gameObject, 5f);
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            // Calculamos la distancia
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            
            // Si es el más cercano, nos lo guardamos
            if (dist < minDistance)
            {
                minDistance = dist;
                target = enemy.transform;
            }
        }
    }

    void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            //Look at the direction
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                
                Destroy(gameObject);
            }
        }
    }
}
