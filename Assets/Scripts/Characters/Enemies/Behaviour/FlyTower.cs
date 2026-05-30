using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTower : EnemyBase
{
    [Header("Patrol Settings")]
    public Transform initialPoint;
    public Transform finalPoint;
    public float patrolSpeed = 2f;
    private Transform patrolTarget;

    [Header("Chase Seetings")]
    public float chaseSpeed = 3.5f;
    public float detectionRange = 7f;

    [Header("Explosion Settings")]
    public float explosionRadious = 3f;
    public GameObject explosionEffect;

    [Header("VFX")]
    public ParticleSystem particlesSystemEffect;

    [Header("Orientation Settings")]
    public TowerType towerType;
    public float rotationSpeed = 5f;
    public float rotationOffset = 90f;

    private SpriteRenderer sprite;
    private bool isParpadeando;

    protected override void Start()
    {
        base.Start();
        patrolTarget = initialPoint;
        currentState = EnemyState.Fly;
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void SetPatrolPoints(Transform initial, Transform final)
    {
        initialPoint = initial;
        finalPoint = final;
        patrolTarget = initial;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnFly(float distance)
    {
        if(distance < detectionRange)
        {
            currentState = EnemyState.Attack;
            if(!isParpadeando) StartCoroutine(FlashRojo());
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolTarget.position, patrolSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, patrolTarget.position) < 0.25)
        {
            patrolTarget = (patrolTarget == initialPoint) ? finalPoint : initialPoint;
        }

        Vector3 moveDir = (patrolTarget.position - transform.position).normalized;
        Flip(moveDir);
    }

    protected override void OnAttack(float distance)
    {
        if(distance < 0.6f)
        {
            explode();
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * chaseSpeed * Time.deltaTime;

        Flip(direction);

        if(distance > detectionRange)
        {
            currentState = EnemyState.Fly;
            patrolTarget = finalPoint;
        }
    }

    private IEnumerator FlashRojo()
    {
        isParpadeando = true;
        while(currentState == EnemyState.Attack)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
        sprite.color = Color.white;
        isParpadeando = false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player") ||
        (currentState == EnemyState.Attack && other.gameObject.CompareTag("Ground")))
        {
            explode();
        }    
    }

    private void explode()
    {
        if(explosionEffect!=null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, explosionRadious, LayerMask.GetMask("Player"));
        GhostHealth health = hit?.GetComponent<GhostHealth>();
        if(health != null)
        {
            health.TakeDamage(damage);
        }

        Death();
    }

    private void Flip(Vector3 dir)
    {
        if(dir == Vector3.zero) return;

        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle + rotationOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadious);
    }
}
