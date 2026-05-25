using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenHero : bossBase
{
    [Header("Prefabs (Projectiles and Magic)")]
    public GameObject prefabWave;
    public GameObject prefabSoulsDown;
    public GameObject prefabSoulsUp;
    public GameObject explosionFloor;

    [Header("Poitnt of Spawn")]
    public Transform pointFloor;
    public Transform pointWall;

    [Header("Procedural Animation")]
    public float velocity = 3f;
    public float heightFloat = 0.25f;
    public float angleInclination = 20f;
    public float velocityInclination = 10f;

    [Header("Visual Effects")]
    public float intensityShaking = 0.1f;
    private bool isShacking = false;

    private float posYOriginal;
    private float posXOriginal;
    private Vector3 shakeCenter;
    private int variantActualWave = 0;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        posXOriginal = transform.localPosition.x;
        posYOriginal = transform.localPosition.y;

        ConfigureStock(new int[] { 0, 1, 2, 3, 4 });

        currentState = bossStates.Waiting;
    }

    protected override void Update()
    {
        base.Update();

        // --- PROCEDURAL ANIMATION ---
        if (isAttacking)
        {
            transform.localRotation = Quaternion.identity;
            if(isShacking) 
            {
               Vector2 vibration = Random.insideUnitCircle * intensityShaking;
               transform.localPosition = new Vector3(
                    shakeCenter.x + vibration.x,
                    shakeCenter.y + vibration.y,
                    transform.localPosition.z);
            }
            return;
        }
        else if(currentState != bossStates.Dead && !isAttacking)
        {
            float velocityX = rb.velocity.x;
            Quaternion objectiveRotation = Quaternion.identity;

            // 1. Lean when movement
            if(Mathf.Abs(velocityX) > 0.5f)
            {
                float movementDirection = Mathf.Sin(velocityX);
                float eyeDirection = Mathf.Sign(transform.localScale.x);
                float fineInclination = angleInclination * -movementDirection * eyeDirection;
                objectiveRotation = Quaternion.Euler(0, 0, fineInclination);
            }

            // Apply the rotation swoly
            transform.localRotation = Quaternion.Lerp(transform.localRotation, objectiveRotation, Time.deltaTime * velocityInclination);

            //2. Oscilation when not movement
            if(isShacking)
            {
                Vector2 vibration = Random.insideUnitCircle * intensityShaking;

                transform.localPosition = new Vector3(
                    transform.localPosition.x + vibration.x,
                    posYOriginal + vibration.y,
                    transform.localPosition.z);
            }
            else if(Mathf.Abs(velocityX) <= 0.5f && !isAttacking && rb.velocity.y == 0)
            {
                float newY = Mathf.Sin(Time.time * velocity) * heightFloat;
                transform.localPosition = new Vector3(transform.localPosition.x, posYOriginal + newY, transform.localPosition.z);
            }
            else
            {
                float actualY = Mathf.Lerp(transform.localPosition.y, posYOriginal, Time.deltaTime * 5f);
                transform.localPosition = new Vector3(transform.localPosition.x, actualY, transform.localPosition.z);
            }
        }
        
    }

    protected override IEnumerator PatternsPhase1()
    {
        isAttacking = true;

        int selectedAttack = ObtainNextAttack();
        yield return StartCoroutine(ExecutePatternsPhase1(selectedAttack));

        yield return new WaitForSeconds(1.2f);
        isAttacking = false;
    }

    protected IEnumerator ExecutePatternsPhase1(int selectedAttack)
    {
        switch(selectedAttack)
        {
            case 0: yield return StartCoroutine(WaveThrust()); break;
            case 1: yield return StartCoroutine(DoublePhaseThrust()); break;
            case 2: yield return StartCoroutine(NailSwordAndSouls()); break;
            case 3: yield return StartCoroutine(FallInChopped()); break;
            case 4: yield return StartCoroutine(BrutalCharge()); break;
        }
    }

    private IEnumerator WaveThrust()
    {
        SeeThePlayer();
        variantActualWave = Random.Range(0, 2);

        animator.SetInteger("waveVariant", variantActualWave);
        animator.SetTrigger("waveAttack");

        yield return new WaitForSeconds(1f);
    }

    public void Event_LaunchWave()
    {
        GameObject wave = null;
        if(variantActualWave == 0)
        {
            wave = Instantiate(prefabWave, transform.position, Quaternion.identity);
        }
        else
        {
            wave = Instantiate(prefabWave, transform.position, Quaternion.identity);
        }

        float dirX = transform.localScale.x > 0 ? 1f : -1f;
        Vector3 waveScale = wave.transform.localScale;
        waveScale.x = Mathf.Abs(waveScale.x) * dirX;
        wave.transform.localScale = waveScale;
    }

    private IEnumerator DoublePhaseThrust()
    {
        SeeThePlayer();
        animator.SetTrigger("phase1Thrust");

        transform.position += new Vector3(0, 0.1f, 0); 
        rb.gravityScale = 0;

        float dirX = transform.localScale.x > 0 ? 1f : -1f;
        rb.velocity = new Vector2(dirX * 8f, 0f);
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.2f);

        SeeThePlayer();
        animator.SetTrigger("phase2Thrust");
        dirX = transform.localScale.x > 0 ? 1f : -1f;
        rb.velocity = new Vector2(dirX * 12f, 0f); // Segunda fase más agresiva
        yield return new WaitForSeconds(0.4f);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;

        yield return new WaitForSeconds(0.8f);
    }

    private IEnumerator NailSwordAndSouls()
    {
        rb.gravityScale = 3f;
        rb.velocity = new Vector2(0, -15f); 
        
        yield return new WaitUntil(() => Mathf.Abs(rb.velocity.y) < 0.1f);
        
        rb.velocity = Vector2.zero;

        SeeThePlayer();
        animator.SetTrigger("nailSword");

        shakeCenter = transform.localPosition;

        isShacking = true;
        yield return new WaitForSeconds(3.5f);

        isShacking = false;
        yield return new WaitForSeconds(0.3f);

        animator.SetTrigger("takeOutSword");
        yield return new WaitForSeconds(0.3f); 

        GameObject vfx = Instantiate(explosionFloor, pointFloor.position, Quaternion.identity);
        Destroy(vfx, 2f);
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.5f);
    }

    public void Event_LaunchSouls()
    {
        int typeOfSouls = Random.Range(0, 2);
        float dirX = transform.localScale.x > 0 ? 1f : -1f;
        Vector3 safeSpawnPoint = pointFloor.position + new Vector3(0, 0.5f, 0);

        if(typeOfSouls == 0)
        {
            GameObject wallSouls = Instantiate(prefabSoulsDown, safeSpawnPoint, Quaternion.identity);
            
            Vector3 wallScale = wallSouls.transform.localScale;
            wallScale.x = Mathf.Abs(wallScale.x) * dirX;
            wallSouls.transform.localScale = wallScale;

            Rigidbody2D rbWall = wallSouls.GetComponent<Rigidbody2D>();
            if(rbWall != null)
                rbWall.velocity = new Vector2(dirX * 8f, 0f);

        }
        else
        {
            int quantity = 7;
            float angleOpening = 60f;
            float initialAngle = -angleOpening / 2f;
            float paseAngle = angleOpening / (quantity-1);

            for(int i = 0; i < quantity; i++)
            {
                float actualAngle = initialAngle + (paseAngle * i);
                Quaternion rotateSouls = Quaternion.Euler(0, 0, actualAngle);

                GameObject soul = Instantiate(prefabSoulsUp, pointFloor.position, rotateSouls);

                Rigidbody2D rbSoul = soul.GetComponent<Rigidbody2D>();
                if(rbSoul != null)
                {
                    rbSoul.gravityScale = 0f;
                    rbSoul.velocity = (Vector2)(rotateSouls * Vector3.up) * 8f;
                }
            }
        }
    }

    private IEnumerator FallInChopped()
    {
        for(int i = 0; i < 3; i++)
        {
            animator.SetTrigger("goSky");

            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, 25f);
            yield return new WaitForSeconds(0.8f);

            rb.velocity = Vector2.zero;

            transform.position = new Vector2(player.position.x, player.position.y + 12f);
            SeeThePlayer();

            yield return new WaitForSeconds(0.5f);

            animator.SetTrigger("fallInChopped");
            rb.gravityScale = 3f;
            rb.velocity = new Vector2(0f, -30f);

            yield return new WaitUntil(() => Mathf.Abs(rb.velocity.y) < 0.1f);

            rb.velocity = Vector2.zero;
            animator.SetTrigger("impactFloor");
            yield return new WaitForSeconds(0.5f);
        }

        rb.gravityScale = 0f; 
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator BrutalCharge()
    {
        SeeThePlayer();
        animator.SetTrigger("prepareCharge");

        yield return new WaitForSeconds(0.8f);

        float dirX = transform.localScale.x > 0 ? 1f : -1f;

        transform.position += new Vector3(0, 0.1f, 0);
        rb.gravityScale = 0f;

        rb.velocity = new Vector2(dirX * 22f, 0f);

        yield return new WaitForSeconds(0.4f);

        rb.velocity = Vector2.zero;
        animator.SetTrigger("thrustCharge");

        rb.velocity = new Vector2(dirX * 5f, 0f);
        yield return new WaitForSeconds(0.3f);
        animator.SetTrigger("impactFloor");
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(1f);
    }
}
