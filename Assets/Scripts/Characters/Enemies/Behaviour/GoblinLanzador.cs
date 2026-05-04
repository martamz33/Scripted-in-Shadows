using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinLanzador : EnemyBase
{
    [Header("Patrol Settings")]
    public Transform initialPosition;
    public Transform finalPosition;
    public float speedWalk = 3.5f;
    private Vector3 targetDestination;

    [Header("Attack Settings")]
    public GameObject prefabDagger;
    public Transform pointToCreateDagger;

    [Header("Burst Settings")]
    public float cooldownAttack = 5f;
    public float timeBetweenShots = 1f;
    public int shotBurst = 3;
    
    private float cronoAttack = 0f;
    private bool isAttacking = false;
    private int currentShots = 0;

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
        if(distance > distanceDetection)
        {
            currentShots = 0;
            currentState = EnemyState.Walk;
            Flip();
            return;
        }

        if(!isAttacking)
        {
            LookAtThePlayer();

            float waitTime = (currentShots >= shotBurst) ? timeBetweenShots : cooldownAttack;

            if(cronoAttack >= waitTime)
            {
                if(currentShots >= shotBurst)
                {
                    currentShots = 0;
                }
                StartAttack();
            }
            else
            {
                cronoAttack += Time.deltaTime;
            }
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        animator.SetTrigger("lanzar");
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

    private void throwDagger()
    {
        if(prefabDagger!=null)
        {
            GameObject Dagger = Instantiate(prefabDagger, pointToCreateDagger.position, Quaternion.identity);

            DaggerGoblin daggerScript = Dagger.GetComponent<DaggerGoblin>();
            if(daggerScript != null)
            {
                daggerScript.damage = this.damage;
            }
            Destroy(Dagger, 3.5f);
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;
        cronoAttack = 0;

        currentShots ++;
    }

}
