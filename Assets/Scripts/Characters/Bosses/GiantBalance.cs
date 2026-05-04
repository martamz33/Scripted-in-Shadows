using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GiantBalance : MonoBehaviour
{
    public int damage = 20;
    public int finalDamage;
    public float lifeTime = 5f;
    public GameObject vfxImpact;

    private Rigidbody2D rg;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();

        rg.gravityScale = 4f;

        Destroy(gameObject, lifeTime);

        float dmgMult = GameManager.Instance != null ? GameManager.Instance.enemyDamageMultiplier : 1f;
        finalDamage = Mathf.RoundToInt(damage * dmgMult);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GhostHealth>().TakeDamage(finalDamage);
        }

        if(other.gameObject.CompareTag("Ground"))
        {
            DestroyBalance();
        }
    }

    private void DestroyBalance()
    {
        if(vfxImpact != null)
        {
            Instantiate(vfxImpact, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
