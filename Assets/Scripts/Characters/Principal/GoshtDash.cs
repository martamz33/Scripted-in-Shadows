using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoshtDash : MonoBehaviour
{
    public event System.Action OnDashExecuted;

    [Header("Dash Configuration")]
    public float dashSpeed = 2.0f;
    public float dashDuration = 0.2f;
    public float dashDelay = 1f;
    
    [Header("Power Up Settings")]
    public int maxDashes = 1;
    public int currentDashes;

    [Header("Audio Settings")]
    public AudioClip dashSound;
    public AudioSource audioSource;

    private Rigidbody2D rb;
    private GhostMovement gm;
    private Animator animator;
    private GhostAttack ghostAttack;
    private bool isDashing = false;
    private float rechargeTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GetComponent<GhostMovement>();
        animator = GetComponent<Animator>();
        ghostAttack = GetComponent<GhostAttack>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        currentDashes = maxDashes;
    }

    void Update()
    {
        if(currentDashes < maxDashes && !isDashing)
        {
            rechargeTimer += Time.deltaTime;
            if(rechargeTimer >= dashDelay)
            {
                currentDashes ++;
                rechargeTimer = 0;
            }
        }

        if(!gm.isMovementActive && !isDashing) return;
        
        if(Input.GetKeyDown(KeyCode.LeftShift) && currentDashes > 0 && !isDashing)
        {
            ghostAttack.CancelAllAttacks();
            StartCoroutine(Dash());

            OnDashExecuted?.Invoke();
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        currentDashes --;
        rechargeTimer = 0f;

        if (dashSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dashSound);
        }

        //initialise the animation
        animator.SetTrigger("Dash");

        gm.isMovementActive = false; //stop the script of movement

        //save the direction of the dash
        float inputDir = Input.GetAxisRaw("Horizontal");
        float finalDir = 0f;

        if(inputDir != 0)
        {
            finalDir = inputDir;
        }
        else
        {
            finalDir = transform.localScale.x;
        }

        //apply the velocity
        rb.velocity = new Vector2(dashSpeed * finalDir, 0f);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;
        rb.gravityScale = originalGravity;
        gm.UpdateValueY();
        gm.isMovementActive = true; //return the movement of the ghost because the dash is finished

        //yield return new WaitForSeconds(dashDelay);

        isDashing = false;
    }

    public void EnableDoubleDash()
    {
        maxDashes = 2;
        currentDashes = maxDashes;
    }
}
