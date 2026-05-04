using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunicShield : MonoBehaviour
{
    private int damage;

    public void SetUp(int d, float lifeTime)
    {
        damage = d;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
