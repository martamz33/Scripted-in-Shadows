using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : EnemyBase
{
    [Header("Attack Settings")]
    public GameObject orbPrefabExplosion;
    public GameObject orbPrefabLaser;
    public Transform pointToCreateOrb;
    public float attackCooldown = 2f;
    public float attackRange = 5f;

    [Header("Teleport Settings")]
    public float safeDistance = 3f;
    public float teleportRadius = 8f;
    public float teleportCooldown = 5f;

    private Vector3 anchorPoint;
    private bool isBusy;
    private float lastAttackTime;
    private float lastTeleportTime;
    private Collider2D col;
    private SpriteRenderer sprite;

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Idle;
        anchorPoint = transform.position;
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnIdle(float distance)
    {
        if(isBusy) return;

        if(distance < safeDistance && Time.time >= lastTeleportTime + teleportCooldown)
        {
            currentState = EnemyState.Teleport;
            return;
        }

        if(distance < distanceDetection)
        {
            currentState = EnemyState.Attack;
            return;
        }
    }

    protected override void OnTeleport(float distance)
    {
        if(!isBusy)
        {
            LookAtThePlayer();
            StartTeleport();
        }
    }

    protected override void OnAttack(float distance)
    {
        if(distance < safeDistance && Time.time >= lastTeleportTime + teleportCooldown)
        {
            currentState = EnemyState.Teleport;
            return;
        }

        if(distance >= distanceDetection)
        {
            currentState = EnemyState.Idle;
            return;
        }

        if(!isBusy)
        {
            if(Time.time > lastAttackTime + attackCooldown)
            {
                LookAtThePlayer();
                StartAttack();
            }
        }
    }

    //attack functions
    private void StartAttack()
    {
        isBusy = true;
        animator.SetTrigger("attack");
    }

    private void createOrb()
    {
        if(pointToCreateOrb == null) return;

        GameObject prefabToSpawn = (Random.value > 0.5) ? orbPrefabExplosion : orbPrefabLaser;

        if(prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, pointToCreateOrb.position, Quaternion.identity);

            if(prefabToSpawn.GetComponent<explosionOrb>())
            {
                explosionOrb orb = prefabToSpawn.GetComponent<explosionOrb>();

                orb.damage = this.damage;
            }
            else
            {
                laserOrb orb = prefabToSpawn.GetComponent<laserOrb>();

                orb.damage = this.damage;
            }
        }
    }

    private void FinishAttack()
    {
        isBusy = false;
        lastAttackTime = Time.time;
        currentState = EnemyState.Idle;

        if(Random.value > 0.7)
        {
            currentState = EnemyState.Teleport;
        }
    }

    //teleport functions
    private void StartTeleport()
    {
        isBusy = true;
        lastTeleportTime = Time.time;
        animator.SetTrigger("teleport");
    }

    private void OnVanish()
    {
        col.enabled = false;
        sprite.enabled = false;

        // 1. Calculamos una posición segura relativa al punto de anclaje
        Vector2 randomOffset = Random.insideUnitCircle.normalized * teleportRadius;
        Vector3 targetPos = anchorPoint + new Vector3(randomOffset.x, randomOffset.y, 0);

        // 2. Comprobamos si hay suelo debajo para que no aparezca en el aire o dentro de un muro
        RaycastHit2D hit = Physics2D.Raycast(targetPos + Vector3.up * 2, Vector2.down, 5f, LayerMask.GetMask("ground"));
        
        if (hit.collider != null)
        {
            // Aparece justo encima del suelo detectado
            transform.position = hit.point + Vector2.up * 0.5f; 
        }
        else
        {
            // Si no hay suelo, intentamos al menos aparecer en el targetPos original
            transform.position = targetPos;
        }

        animator.SetTrigger("teleportIn");
    }

    private void OnInitionTeleportBack()
    {
        col.enabled = true;
        sprite.enabled = true;
    }

    private void finishTeleport()
    {
        isBusy = false;
        currentState = EnemyState.Idle;
    }

    //utilities
    private void LookAtThePlayer()
    {
        if (player.position.x > transform.position.x) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Application.isPlaying ? anchorPoint : transform.position, teleportRadius);
        
        // Dibujamos el área de pánico (si entras aquí, huye)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, safeDistance);
    }
}
