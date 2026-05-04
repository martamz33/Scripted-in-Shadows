using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldKnockBack : MonoBehaviour
{
    public float force = 10f;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D rbEnemy = other.gameObject.GetComponent<Rigidbody2D>();

            if(rbEnemy != null)
            {
                Vector2 direction  = (other.transform.position - transform.position).normalized;

                rbEnemy.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }
}
