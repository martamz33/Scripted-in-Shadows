using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public event System.Action OnJumpExecuted;

    [Header("Movement Settings")]
    public float movementSpeed = 5f; //velocity of the movement
    public float floatAmplitude = 0.2f; //height oscilation
    public float floatFrequency = 2f; //frequency of the oscilation
    public float minimunHeight = 1f;
    public float acceleration = 20f;
    public float deceleration = 20f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public float jumpGravity = 9.8f;
    public float fallMultiplier = 1.5f;
    public float jumpCutMultiplier = 0.5f;
    public List<LayerMask> groundLayer;

    [Header("Float Settings")]
    public float floatDelay = 2f;

    [Header("Audio Settings")]
    [Tooltip("AudioSource para el sonido en bucle del movimiento")]
    public AudioSource movementAudioSource; 
    [Tooltip("AudioSource general para efectos (salto)")]
    public AudioSource sfxAudioSource;
    public AudioClip jumpSound;

    [Header("Others")]
    public bool isMovementActive = true;
    public bool isOscilationActive = true;
    [HideInInspector] public bool IsGrounded => isGrounded;

    //private variables
    private Rigidbody2D rg;
    private Animator animator;

    private float baseY;
    private float verticalVelocity;
    private int jumpCount;
    private int jumpsMax;
    private bool isGrounded;
    private bool isFalling;
    private float timerToFloat;
    private bool canFloat;
    private bool ignorePlatforms;
    private Vector2 externalVelocity;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.gravityScale = 0f;
        animator = GetComponent<Animator>();
        baseY = transform.position.y;
        jumpCount = 0;
        jumpsMax = 2;
        timerToFloat = 0;
        canFloat = false;
        GetCombinedLayerMask();
    }

    void Update()
    {
        if(!isMovementActive) 
        {
            // Si el movimiento se desactiva (ej. por un diálogo), apagamos el sonido por seguridad
            if (movementAudioSource != null && movementAudioSource.isPlaying)
            {
                movementAudioSource.Stop();
            }
            return; 
        }
        
        isGrounded = isNearGround();
        animator.SetBool("isGrounded", isGrounded);

        HandleMovement();
        HandleJump();
        HandleVerticalMovement();
    
    }

    //to combine the layermask in one
    private int GetCombinedLayerMask()
    {
        int combinedMask = 0;
        foreach (LayerMask mask in groundLayer)
        {
            if(ignorePlatforms && mask == LayerMask.GetMask("platform")) continue;
            combinedMask |= mask.value;
        }
        return combinedMask;
    }

    private void HandleMovement()
    {
        //Movement 
        float moveInput = Input.GetAxisRaw("Horizontal");

        float targetVelocityX = moveInput * movementSpeed;

        float accelRate = (Mathf.Abs(moveInput) > 0.01f) ? acceleration : deceleration;

        float currentVelocityX = rg.velocity.x - externalVelocity.x;
        currentVelocityX = Mathf.MoveTowards(currentVelocityX, targetVelocityX, accelRate * Time.deltaTime);

        rg.velocity = new Vector2(currentVelocityX + externalVelocity.x, rg.velocity.y);

        //Absolute value to initiate or not the float aniamtion
        animator.SetFloat("Speed", Mathf.Abs(currentVelocityX));

        if (movementAudioSource != null)
        {
            // If ismoving
            if (Mathf.Abs(moveInput) > 0.01f)
            {
                if (!movementAudioSource.isPlaying)
                {
                    movementAudioSource.Play();
                }
            }
            else // If not
            {
                if (movementAudioSource.isPlaying)
                {
                    movementAudioSource.Stop();
                }
            }
        }

        //Flip
        if(moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleJump()
    {
        isGrounded = isNearGround();
        animator.SetBool("isGrounded", isGrounded);

        if(isGrounded && verticalVelocity <= 0f)
        {
            jumpCount = 0;

            float groundY = getGroundY();
            baseY = groundY + minimunHeight;

            if(!canFloat)
            {
                timerToFloat += Time.deltaTime;
                if(timerToFloat > floatDelay)
                {
                    canFloat = true;
                    timerToFloat = 0f;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && jumpCount <jumpsMax)
        {
            verticalVelocity = jumpForce;
            jumpCount++;
            canFloat = false; //stop the float
            timerToFloat = 0f; //restart the timer

            if (sfxAudioSource != null && jumpSound != null)
            {
                sfxAudioSource.PlayOneShot(jumpSound);
            }

            animator.SetTrigger("Jump");

            if(OnJumpExecuted != null) OnJumpExecuted.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Space) && verticalVelocity > 0f)
        {
            verticalVelocity *= jumpCutMultiplier; 
        }
    }

    private void HandleVerticalMovement()
    {
        if(jumpCount!= 0 || isFalling)
        {
            float currentGravity = jumpGravity;
            if(verticalVelocity < 0)
            {
                currentGravity *= fallMultiplier;
            }

            verticalVelocity -= currentGravity * Time.deltaTime;
            rg.velocity = new Vector2(rg.velocity.x, verticalVelocity + externalVelocity.y);

            if(isGrounded && verticalVelocity <=0f)
            {
                verticalVelocity = 0f;
                rg.velocity = new Vector2(rg.velocity.x, 0f); 
                isFalling = false;
                jumpCount = 0;

                baseY = transform.position.y;
            }
        }
        else if(!isGrounded)
        {
            isFalling = true;
            verticalVelocity = rg.velocity.y;
            canFloat = false;
            timerToFloat = 0f;
        }
        else if(canFloat && isOscilationActive)//If not jumping oscilation
        {
            //Movement float vertical
            float targetY = baseY + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

            float smoothness = 5f; 
            float oscillationVelocity = (targetY - transform.position.y) * smoothness;
            
            rg.velocity = new Vector2(rg.velocity.x, oscillationVelocity + externalVelocity.y);

            baseY += externalVelocity.y * Time.deltaTime;
        }
        else
        {            
            timerToFloat += Time.deltaTime;
            if(timerToFloat > floatDelay)
            {
                canFloat = true;
                timerToFloat = 0f;
            }
        }
    }

    private bool isNearGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, minimunHeight + 0.2f, GetCombinedLayerMask());
        
        if(hit.collider !=null)
        {
            MovingPlatforms platform = hit.collider.GetComponent<MovingPlatforms>();
            if(platform !=null)
            {
                SetExternalVelocity(platform.GetVelocity());
            }
            else
            {
                SetExternalVelocity(Vector2.zero);
            }
            return true;
        }
        SetExternalVelocity(Vector2.zero);
        return false;
    }

    private float getGroundY()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, minimunHeight + 0.2f, GetCombinedLayerMask());
        return hit.collider ? hit.point.y : transform.position.y - minimunHeight;
    }

    //to actualise the value of y on the dash script
    public void UpdateValueY()
    {
        baseY = transform.position.y;
    }

    //to go down in some platform
    public void SetFallingPlatform()
    {
        isFalling = true;
        canFloat = false;
        verticalVelocity = -2f;
        StartCoroutine(IgnorePlatforms());
        animator.SetTrigger("goDown");
    }

    private IEnumerator IgnorePlatforms()
    {
        ignorePlatforms = true;
        yield return new WaitForSeconds(0.5f);
        ignorePlatforms = false;
    }

    public void SetExternalVelocity(Vector2 v)
    {
        externalVelocity = v;
    }
    
}
