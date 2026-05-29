using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class troll : EnemyBase
{
    [Header("Patrol Settings")]
    public Transform initialPosition;
    public Transform finalPosition;
    public float speedWalk = 3.5f;
    private Vector3 targetDestination;

    [Header("Attack Settings")]
    public float attackCooldown = 2f;
    public Collider2D weaponCol;
    public bool IsAttacking => isAttaking;

    [Header("Dash Settings")]
    public float speedDash = 8f;
    public float dashDuration = 0.2f;

    //private arguments
    private bool isAttaking;
    private float lastAttackTime;

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Walk;
        targetDestination = finalPosition != null ? finalPosition.position : transform.position;
        lastAttackTime = -attackCooldown;

        if(weaponCol != null) weaponCol.enabled = false;
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
        if(distance > distanceDetection && !isAttaking)
        {
            currentState = EnemyState.Walk;
            Flip();
            return;
        }

        if(!isAttaking) LookAtThePlayer();

        if(!isAttaking && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(performDashAttack());
        }
    }

    //attact functions
    private IEnumerator performDashAttack()
    {
        isAttaking = true;
        OpenWeaponCollider();
        Vector3 dashDir = (player.position.x > transform.position.x) ? Vector3.right : Vector3.left;

        float timer = 0;
        while(timer < dashDuration)
        {
            // En lugar de modificar transform.position directamente:
            Vector2 newPos = rg.position + (Vector2)dashDir * speedDash * Time.deltaTime;
            rg.MovePosition(newPos); 

            timer += Time.deltaTime;
            yield return null;
        }
        animator.SetTrigger("attack");
    }

    private void FinishAttack()
    {
        isAttaking = false;
        lastAttackTime = Time.time;
        CloseWeaponCollider();
    }

    //utilities
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
}
