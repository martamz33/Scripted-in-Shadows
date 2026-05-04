using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class projectileBoss : MonoBehaviour
{
    [Header("Configuration")]
    public modeMovementProjectileBoss modeMovement = modeMovementProjectileBoss.Lineal;

    public float velocity = 10f;
    public int baseDamage = 10;
    public int finalDamage;
    public float timeOfLife;

    [Header("Impact")]
    public bool destroyProjectileWithWall = true;
    public bool destroyWhenCollidesWithPlayer = true;
    public GameObject vfxImpact;

    private Rigidbody2D rg;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();

        // 0. Apply 
        float dmgMult = GameManager.Instance != null ? GameManager.Instance.enemyDamageMultiplier : 1f;
        finalDamage = Mathf.RoundToInt(baseDamage * dmgMult);

        // 1. Autodestroy
        Destroy(gameObject, timeOfLife);

        // 2. Apply movement different if mode
        if(modeMovement == modeMovementProjectileBoss.Lineal)
        {
            rg.gravityScale = 0f;

            float direccionX = Mathf.Sign(transform.localScale.x);
            rg.velocity = new Vector2(direccionX * velocity, 0f);
        }
        else if(modeMovement == modeMovementProjectileBoss.Physics_Gravity)
        {
            rg.gravityScale = 3f;
        }
    }

    // --- DECTECTION IF TRIGGERS  ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessImpact(collision.gameObject);
    }

    // --- DECTECTION IF NORMAL COLLIDERS ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessImpact(collision.gameObject);
    }

    private void ProcessImpact(GameObject collideObject)
    {
        if(collideObject.CompareTag("Player"))
        {
            if(destroyWhenCollidesWithPlayer)
                DestroyProjectile();
        }
        else if(collideObject.CompareTag("Ground"))
        {
            if(destroyProjectileWithWall)
                DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if (vfxImpact != null)
        {
            Instantiate(vfxImpact, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
