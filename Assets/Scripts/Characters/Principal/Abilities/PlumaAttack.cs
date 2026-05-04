using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumaAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 2;
    public float speed = 5.5f;
    public float amplitude = 1.5f;
    public float lifeTime = 4f;

    [Header("VFX")]
    public GameObject explosionParticles;

    [Header("Damage Ticks")]
    public float timeBetweenTicks = 0.5f; 
    
    private Dictionary<Collider2D, float> enemyTimers = new Dictionary<Collider2D, float>();

    private float timer = 0f;

    void Update()
    {
        if(timer >= lifeTime)
        {
            Explosion();
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;

            float x = Mathf.Sin(Time.time * speed) * amplitude;

            transform.localPosition = new Vector3(x, 0, 0);
        }
        
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(!enemyTimers.ContainsKey(other))
            {
                enemyTimers.Add(other, 0f);
            }

            enemyTimers[other] -= Time.deltaTime;
            if(enemyTimers[other] <= 0f)
            {
                EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();

                if(enemy != null)
                {
                    enemy.TakeDamage(damage);

                    enemyTimers[other] = timeBetweenTicks;
                }
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy") && enemyTimers.ContainsKey(other))
        {
            enemyTimers.Remove(other);
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
