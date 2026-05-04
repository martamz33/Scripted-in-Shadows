using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyBase
{
    [Header("Patrol Settings")]
    public Transform initialPosition;
    public Transform finalPosition;
    public float speedWalk = 3.5f;
    private Vector3 targetDestination;

    [Header("Chase Settings")]
    public float chaseSpeed = 6f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;

    [Header("Attacks Setting")]
    public Collider2D weaponCol;
    public bool IsAttacking => isAttaking;

    private bool isAttaking;
    private float lastAttackTime;
    
    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Walk;
        targetDestination = finalPosition.position;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnWalk(float distance)
    {
        if(distance < distanceDetection)
        {
            currentState = EnemyState.Attack;
            return;
        }

        //1.Move to destination
        transform.position = Vector3.MoveTowards(transform.position, targetDestination, speedWalk * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetDestination) < 0.25f)
        {
            targetDestination = (targetDestination == initialPosition.position) ? finalPosition.position : initialPosition.position;
            Flip();
        }
    }
    
    protected override void OnAttack(float distance)
    {
        if(distance > distanceDetection && !isAttaking)
        {
            currentState = EnemyState.Walk;
            Flip();
            return;
        }

        LookAtThePlayer();

        if(distance > attackRange && !isAttaking)
        {
            Vector3 targetPos = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, chaseSpeed * Time.deltaTime);
        }
        else if(distance <= attackRange && !isAttaking && Time.time >= lastAttackTime + attackCooldown)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        isAttaking = true;
        animator.SetTrigger("attack");
    }

    private void Flip()
    {
        float direction = targetDestination.x - transform.position.x;
        if(direction > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (direction < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void LookAtThePlayer()
    {
        if (player.position.x > transform.position.x) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }

    public void OpenWeaponCollider()
    {
        if(weaponCol != null) weaponCol.enabled = true;
    }

    public void CloseWeaponCollider()
    {
        if(weaponCol != null) weaponCol.enabled = false;
    }

    private void FinishAttack()
    {
        isAttaking = false;
        lastAttackTime = Time.time;
        CloseWeaponCollider();
    }
}
