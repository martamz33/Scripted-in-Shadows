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

    [Header("Audio Settings")]
    public AudioSource audioSource;
    [Tooltip("Sonido al dar el golpe (hacer daño)")]
    public AudioClip attackSound;

    private bool isAttaking;
    private float lastAttackTime;
    
    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Walk;
        targetDestination = (finalPosition != null) ? finalPosition.position : transform.position;
        lastAttackTime = -attackCooldown;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public override void SetPatrolPoints(Transform initial, Transform final)
    {
        initialPosition = initial;
        finalPosition = final;
        targetDestination = final.position;
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

        //1.Move to destination (horizontal only, let physics handle Y)
        float newX = Mathf.MoveTowards(rg.position.x, targetDestination.x, speedWalk * Time.deltaTime);
        rg.MovePosition(new Vector2(newX, rg.position.y));

        if(Mathf.Abs(rg.position.x - targetDestination.x) < 0.25f)
        {
            if(initialPosition == null || finalPosition == null) return;
            targetDestination = (targetDestination == initialPosition.position) ? finalPosition.position : initialPosition.position;
            Flip();
        }
    }
    
    protected override void OnAttack(float distance)
    {
        if(distance > distanceDetection  && !isAttaking)
        {
            currentState = EnemyState.Walk;
            targetDestination = (initialPosition != null) ? initialPosition.position : transform.position;
            Flip();
            return;
        }

        LookAtThePlayer();

        if(distance > attackRange)
        {
            float newX = Mathf.MoveTowards(rg.position.x, player.position.x, chaseSpeed * Time.deltaTime);
            rg.MovePosition(new Vector2(newX, rg.position.y));
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

        if (attackSound != null && audioSource != null)
        {
            // Variamos ligeramente el tono para que no suene repetitivo si pega muchas veces
            audioSource.pitch = 1f + Random.Range(-0.1f, 0.1f);
            audioSource.PlayOneShot(attackSound);
        }
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
