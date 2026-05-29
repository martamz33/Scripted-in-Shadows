using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : EnemyBase
{
    [Header("Settings")]
    public Transform initialPosition;
    public Transform finalPosition;
    public float speedWalk = 3.5f;
    private Vector3 targetDestination;

    [Header("Attack Settings")]
    public GameObject rockPrefab;
    public Transform pointToCatapulte;
    public float delayAttackTowe = 3f;
    private float cronoAttack;
    
    protected override void Start()
    {
        base.Start();
        targetDestination = finalPosition != null ? finalPosition.position : transform.position;
        currentState = EnemyState.Walk;
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
        //1.Move to destination (horizontal only, let physics handle Y)
        float newX = Mathf.MoveTowards(rg.position.x, targetDestination.x, speedWalk * Time.deltaTime);
        rg.MovePosition(new Vector2(newX, rg.position.y));

        if(Mathf.Abs(rg.position.x - targetDestination.x) < 0.25f)
        {
            if(initialPosition == null || finalPosition == null) return;
            targetDestination = (targetDestination == initialPosition.position) ? finalPosition.position : initialPosition.position;
            Flip();
        }
        

        //the attack is integrated with the walk to stop some seconds
        if(distance < distanceDetection)
        {
            LookAtThePlayer();
            cronoAttack += Time.deltaTime;
            if(cronoAttack >= delayAttackTowe)
            {
                TriggerAttack();
            }
        }
                
    }

    protected override void OnAttack(float distance)
    {
        OnWalk(distance);        
    }

    private void TriggerAttack()
    {
        animator.SetTrigger("attack");
        cronoAttack = 0;
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

    private void LaunchRock()
    {
        if(rockPrefab !=null)
        {
            GameObject rock = Instantiate(rockPrefab, pointToCatapulte.position, Quaternion.identity);

            towerAttack attack = rock.GetComponent<towerAttack>();

            if(attack != null)
            {
                attack.damage = this.damage;
            }
            
            float distPlayer = Mathf.Abs(player.position.x - transform.position.x);

            rock.GetComponent<towerAttack>().setUpLauch(distPlayer);
        }
    }

}
