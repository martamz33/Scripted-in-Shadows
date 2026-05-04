using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : EnemyBase
{
    [Header("Ball Settings")]
    public float rollSpeed = 8f;
    public float dashDuration = 0.5f;
    public float waitBetweenDashes = 0.3f;
    public float coolDownAfterAttack = 2f;

    public bool IsAttacking => isAttacking;

    [Header("VFX")]
    public ParticleSystem dustParticles;

    private bool isAttacking;
    private Collider2D colDwarf;
    private Collider2D colBall;

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Idle;
        colDwarf = GetComponent<Collider2D>();
        colBall = transform.Find("ColliderBall").GetComponent<Collider2D>();

        colBall.enabled = false;
        colDwarf.enabled = true;
    }

    protected override void Update()
    {
        base.Update();
        fixParticulesRotation();
    }

    protected override void OnIdle(float distance)
    {
        if(distance < distanceDetection && !isAttacking)
        {
            currentState  = EnemyState.Attack;
            return;
        }
    }

    protected override void OnAttack(float distance)
    {
        if(distance > distanceDetection)
        {
            currentState  = EnemyState.Idle;
        }
        if(!isAttacking) StartCoroutine(BallSequence());        
    }

    private void fixParticulesRotation()
    {
        if(dustParticles != null)
        {
            dustParticles.transform.rotation = Quaternion.Euler(-90, 0, 0);

            Vector3 pos = dustParticles.transform.position;
        }
    }

    private IEnumerator BallSequence()
    {
        isAttacking = true;

        float groundY = transform.position.y;
        
        animator.SetTrigger("transformToBall");
        yield return new WaitForSeconds(0.5f);

        colDwarf.enabled = false;
        colBall.enabled = true;

        for(int i = 0; i< 3; i++)
        {
            Vector3 targetDirection = (player.position - transform.position);
            Vector3 attackDir = new Vector3(targetDirection.x, 0, 0).normalized;
            Flip(attackDir.x);

            if(dustParticles !=null) dustParticles.Play();

            float timer = 0;
            while(timer < dashDuration)
            {
                Vector3 nextPosition = transform.position + (attackDir * rollSpeed * Time.deltaTime);
                
                RaycastHit2D hit= Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("ground"));

                if(hit.collider != null)
                {
                    nextPosition.y = hit.point.y + 0.5f;
                }
                else
                {
                    nextPosition.y -= 9.8f * Time.deltaTime;
                }
                
                transform.position = nextPosition;
                timer+= Time.deltaTime;
                groundY = nextPosition.y;
                yield return null;
            }

            if(dustParticles!=null) dustParticles.Stop();

            yield return new WaitForSeconds(waitBetweenDashes);
        }

        animator.SetTrigger("transformToHuman");
        yield return new WaitForSeconds(0.5f);

        if(dustParticles!=null) dustParticles.Stop();

        transform.position = new Vector3(transform.position.x, groundY, 0);

        colDwarf.enabled = true;
        colBall.enabled = false;
        yield return new WaitForSeconds(coolDownAfterAttack);

        isAttacking = false;
        currentState = EnemyState.Idle;
    }

    private void Flip(float dirX)
    {
        if(dirX > 0) transform.localScale = new Vector3(1, 1, 1);
        else if(dirX < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
}
