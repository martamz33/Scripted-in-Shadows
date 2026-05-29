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
    public LayerMask groundLayer;

    public bool IsAttacking => isAttacking;

    [Header("VFX")]
    public ParticleSystem dustParticles;

    private bool isAttacking;
    private Rigidbody2D rb;
    private Collider2D colDwarf;
    private Collider2D colBall;

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Idle;
        rb = GetComponent<Rigidbody2D>();
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

        animator.SetTrigger("transformToBall");
        yield return new WaitForSeconds(0.5f);

        colBall.enabled = true;

        // Asegurarnos de que usa físicas dinámicas
        if(rb != null) 
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;
        }

        for(int i = 0; i< 3; i++)
        {
            Vector3 targetDirection = (player.position - transform.position).normalized;
            float dirX = targetDirection.x > 0 ? 1 : -1;
            Flip(dirX);

            float timer = 0;
            while(timer < dashDuration)
            {
                rb.velocity = new Vector2(dirX * rollSpeed, rb.velocity.y);
                timer += Time.deltaTime;
                yield return null;
            }
            
            // Frenar al terminar el dash
            if(rb != null) rb.velocity = new Vector2(0, rb.velocity.y);

            if(dustParticles!=null) dustParticles.Stop();

            yield return new WaitForSeconds(waitBetweenDashes);
        }

        animator.SetTrigger("transformToHuman");
        yield return new WaitForSeconds(0.5f);

        if(dustParticles!=null) dustParticles.Stop();

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
