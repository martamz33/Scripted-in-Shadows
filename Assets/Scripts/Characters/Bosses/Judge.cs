using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum PointToBalance
{
    UpLeft, UpRight, HorizontalRight, HorizontalLeft, Bounce
}

[System.Serializable]
public struct PointToDispara
{
    public PointToBalance pointName;
    public Transform transformPoint;
}

public class Judge : bossBase
{
    [Header("Phase Reference")]
    public GameObject visualPhase1;
    public GameObject visualPhase2;

    [Header("Prefabs (Proyectiles)")]
    // Phase 1
    public GameObject prefabLittleHammer;
    public GameObject prefabBigHammer;
    public GameObject prefabWave;
    public GameObject prefabHammerSky;
    // Phase 2
    public GameObject prefabLittleBalance;
    public GameObject prefabBounceBalance;
    public GameObject prefabBigBalance;

    [Header("Spawn Points")]
    public Transform ThroughPoint;
    public Transform floorPoint;
    public List<PointToDispara> listOfPoints;

    [Header("Hover Settings (Float)")]
    public float floatVelocity = 2f;
    public float floatHeight = 0.3f;
    private float originalPosYPhase1;
    private float originalPosYPhase2;

    private int rangeHitHammer = 3;

    private Dictionary<PointToBalance, Transform> dictionaryOfPoints = new Dictionary<PointToBalance, Transform>();
    
    protected override void Start()
    {
        base.Start();

        animator = visualPhase1.GetComponent<Animator>();

        originalPosYPhase1 = visualPhase1.transform.localPosition.y;
        originalPosYPhase2 = visualPhase2.transform.localPosition.y;

        foreach(var point in listOfPoints)
        {
            if(!dictionaryOfPoints.ContainsKey(point.pointName))
            {
                dictionaryOfPoints.Add(point.pointName, point.transformPoint);
            }
        }

        // --- PHASE 1: put the attacks in the stock
        ConfigureStock(new int[] { 0, 1, 2, 3});

        // Start the fight
        currentState = bossStates.Waiting;
    }

    protected override void Update()
    {
        base.Update();

        if(!isAttacking && currentState != bossStates.Waiting && player != null)
        {
            // 1. Movimiento suave hacia el jugador (solo eje X)
            float speed = 2f; // Ajusta esta velocidad
            Vector3 targetPos = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else if(!isAttacking)
        {
            float nuevoY = Mathf.Sin(Time.time * floatVelocity) * floatHeight;
        
            if (currentState == bossStates.Phase1 || currentState == bossStates.Transcition)
            {
                visualPhase1.transform.localPosition = new Vector3(visualPhase1.transform.localPosition.x, originalPosYPhase1 + nuevoY, visualPhase1.transform.localPosition.z);
            }
            else if (currentState == bossStates.Phase2)
            {
                visualPhase2.transform.localPosition = new Vector3(visualPhase2.transform.localPosition.x, originalPosYPhase2 + nuevoY, visualPhase2.transform.localPosition.z);
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        // Trigger the transformation if necessary
        if(actualHealth <= totalHealth/2 && currentState == bossStates.Phase1)
        {
            StartCoroutine(transitionToPhase2());
        }
        else if(actualHealth <= 0)
        {
            StartCoroutine(DeadRoutine());
        }
    }

    private IEnumerator transitionToPhase2()
    {
        currentState = bossStates.Transcition;
        isAttacking = false;
        StopAllCoroutines();

        rb.velocity = Vector2.zero;
        GameManager.Instance.FreezePlayer(true);
        animator.SetTrigger("transformation");

        yield return new WaitForSeconds(2f);

        animator = visualPhase2.GetComponent<Animator>();

        ConfigureStock(new int[] { 6, 7 });
        currentState = bossStates.Phase2;
        isAttacking = false;
        GameManager.Instance.FreezePlayer(false);
    }

    protected override IEnumerator PatternsPhase1()
    {
        isAttacking = true;

        int selectedAttack = ObtainNextAttack();

        yield return StartCoroutine(ExecutePatternsPhase1(selectedAttack));

        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }

    protected IEnumerator ExecutePatternsPhase1(int selectedAttack)
    {
        switch(selectedAttack)
        {
            case 0: // Invoke Big Hammers
                yield return StartCoroutine(InvokeAttack());
                break;
            case 1: // Through the Little hammer
                yield return StartCoroutine(throughHammerLittle());
                break;
            case 2: // Through the Big hammer
                yield return StartCoroutine(throughHammerBig());
                break;
            case 3: // Melee Little
                yield return StartCoroutine(hitHammerLittle());
                break;
            case 4: // Melee Big
                yield return StartCoroutine(hitHammerBig());
                break;
            case 5: // Earthquake attack
                yield return StartCoroutine(hitHammerRafagas());
                break;
        }
    }

    protected override IEnumerator PatternsPhase2()
    {
        isAttacking = false;

        int selectedAttack = ObtainNextAttack();

        yield return StartCoroutine(ExecutePatternsPhase2(selectedAttack));

        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }

    protected IEnumerator ExecutePatternsPhase2(int selectedAttack)
    { 
        switch(selectedAttack)
        {
            case 6: // Invoque Giant Balances
                yield return StartCoroutine(InvokeBigBalance());
                break;
            case 7: // Through Balance (4 variations)
                yield return StartCoroutine(throughBalance());
                break;
            case 8: // Attack with the balance
                yield return StartCoroutine(attackBalance());
                break;
            case 9: // Wave of Energy
                yield return StartCoroutine(eneryWaves());
                break;
            case 10: // Combo caos (Through with bounce a blance and attack with other)
                yield return StartCoroutine(caosCombo_TroughtAndMelee());
                break;
        }     
    }

    // --- COROUTINE ATTACKS  PHASE 1---
    private IEnumerator InvokeAttack()
    {
        SeeThePlayer();
        animator.SetTrigger("Invocation");
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator throughHammerLittle()
    {
        int quantity = Random.Range(1, 4);

        for(int i = 0; i < quantity; i++)
        {
            SeeThePlayer();
            animator.SetTrigger("throughHammerL");
            yield return new WaitForSeconds(0.6f);
        } 
        yield return new WaitForSeconds(0.5f);
    }
    
    private IEnumerator throughHammerBig()
    {
        int quantity = Random.Range(1, 3);

        for(int i = 0; i < quantity; i++)
        {
            SeeThePlayer();
            animator.SetTrigger("throughHammerB");
            yield return new WaitForSeconds(0.7f);
        } 
        yield return new WaitForSeconds(0.8f);
    }

    private IEnumerator hitHammerLittle()
    {
        for(int i = 0; i < rangeHitHammer; i++)
        {
            SeeThePlayer();
            animator.SetTrigger("hitHammerL");

            float direccionX = transform.localScale.x > 0 ? 1f : -1f;
            rb.velocity = new Vector2(direccionX * 5f, 0f);

            yield return new WaitForSeconds(0.3f);
            rb.velocity = Vector2.zero;

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator hitHammerBig()
    {
        SeeThePlayer();
        animator.SetTrigger("hitHammerB");

        float direccionX = transform.localScale.x > 0 ? 1f : -1f;
        rb.velocity = new Vector2(direccionX * 3f, 0f);
        yield return new WaitForSeconds(0.5f);

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(1.2f);
    }

    private IEnumerator hitHammerRafagas()
    {
        float bordeElegidoX = Random.value > 0.5f ? 10f : -10f; 
        Vector2 destino = new Vector2(bordeElegidoX, transform.position.y);

        animator.SetFloat("velocity", 5f);

        while (Mathf.Abs(transform.position.x - bordeElegidoX) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destino, 8f * Time.deltaTime);
            yield return null;
        }

        animator.SetFloat("velocity", 0f);
        SeeThePlayer();

        int quantityWaves = Random.Range(4, 7);
        for (int i = 0; i < quantityWaves; i++)
        {
            animator.SetTrigger("hammerBig");
            yield return new WaitForSeconds(0.3f); 
        }

        yield return new WaitForSeconds(1f);
    }

    // --- Attack funtions ---
    public void Event_InstantiateHammerSky()
    {
        StartCoroutine(RutineHammerSky());
    }

    private IEnumerator RutineHammerSky()
    {
        int numberOfWaves = 3;
        float heightSpawns = 12f;

        for(int wave = 0; wave < numberOfWaves; wave ++)
        {
            float[] distanceCenter = { 8f, 5f, 2f };

            foreach(float dist in distanceCenter)
            {
                Vector2 center = player.position;

                Instantiate(prefabHammerSky, new Vector2(center.x - dist, center.y + heightSpawns), Quaternion.identity);
                Instantiate(prefabHammerSky, new Vector2(center.x + dist, center.y + heightSpawns), Quaternion.identity);

                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void Event_InstantiateWavesHammer()
    {
        GameObject wave = Instantiate(prefabWave, floorPoint.position, Quaternion.identity);

        float direccionBoss = Mathf.Sign(transform.localScale.x); 
        wave.transform.localScale = new Vector3(Mathf.Abs(wave.transform.localScale.x) * direccionBoss, wave.transform.localScale.y, wave.transform.localScale.z);
    }

    public void Event_InstantiateThoughHammerL()
    {
        GameObject hammer = Instantiate(prefabLittleHammer, ThroughPoint.position, Quaternion.identity);
        projectileBoss proj = hammer.GetComponent<projectileBoss>();
        if(proj != null) proj.directionX = Mathf.Sign(transform.localScale.x);
    }

    public void Event_InstantiateThoughHammerB()
    {
        GameObject bigHammer = Instantiate(prefabBigHammer, ThroughPoint.position, Quaternion.identity);
    
        // Calcular dirección real hacia el jugador
        Vector2 direction = (player.position - ThroughPoint.position).normalized;
        
        // Aplicar fuerza en esa dirección
        Rigidbody2D rbHammer = bigHammer.GetComponent<Rigidbody2D>();
        if(rbHammer != null)
        {
            rbHammer.AddForce(direction * 10f, ForceMode2D.Impulse);
            
            // Opcional: rotar el martillo para que mire al jugador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bigHammer.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // --- COROUTINES PHASE 2 ---
    private IEnumerator InvokeBigBalance()
    {
        animator.SetTrigger("invoke");
        yield return new WaitForSeconds(0.4f);
    }

    private IEnumerator throughBalance()
    {
        animator.SetTrigger("throughBalance");
        yield return new WaitForSeconds(0.8f);
    }

    private IEnumerator attackBalance()
    {
        animator.SetTrigger("attackBalance");

        float dir = transform.localScale.x > 0 ? 1f : -1f;
        rb.velocity = new Vector2(dir * 8f, rb.velocity.y); 

        yield return new WaitForSeconds(0.6f);
        rb.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(0.4f);
    }

    private IEnumerator eneryWaves()
    {
        int variation = Random.Range(0, 4);
        animator.SetInteger("variations", variation);
        
        animator.SetTrigger("energyWave");
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator caosCombo_TroughtAndMelee()
    {
        animator.SetTrigger("attackBalance");
        yield return new WaitForSeconds(0.3f);

        Transform spawnBounce = GetPointFromDictionary(PointToBalance.Bounce);

        GameObject crazyBalance = Instantiate(prefabBounceBalance, spawnBounce.position, Quaternion.identity);
        Rigidbody2D rgBalance = crazyBalance.GetComponent<Rigidbody2D>();

        float dirX = transform.localScale.x > 0 ? 1f : -1f;
        rgBalance.AddForce(new Vector2(dirX * 5f, 10f), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f); 
        yield return StartCoroutine(attackBalance());
    }

    // --- events Phase2
    public void Event_InvokeBigBalances()
    {
        Vector2 posJugador = GameObject.FindGameObjectWithTag("Player").transform.position;

        for (int i = 0; i < 4; i++) 
        {
            Vector2 spawnCielo = new Vector2(posJugador.x + Random.Range(-6f, 6f), posJugador.y + 12f);
            Instantiate(prefabBigBalance, spawnCielo, Quaternion.identity);
        }
    }

    public void Event_ThoughBalance(string enumNameString)
    {
        if(System.Enum.TryParse(enumNameString, out PointToBalance pointEnum))
        {
            Transform realSpawnPoint = GetPointFromDictionary(pointEnum);
            int currentVariation = animator.GetInteger("variations");

            switch(currentVariation)
            {
                case 0: 
                    Shot(realSpawnPoint);
                    break;
                case 1:
                    float angle = Random.Range(-15f, 16f);
                    ShotInAngle(angle, realSpawnPoint);
                    break;
                case 2:
                    ShotInParabolic(realSpawnPoint);
                    break;
                case 3:
                    Shot(realSpawnPoint);
                    break;
            }
        }
    }

    public void Shot(Transform pointBalance)
    {
        Instantiate(prefabLittleBalance, pointBalance.position, Quaternion.identity);
    }

    public void ShotInAngle(float angle, Transform pointBalance)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(prefabLittleBalance, pointBalance.position, pointBalance.rotation * rotation);
    }

    public void ShotInParabolic(Transform pointBalance)
    {
        GameObject balance = Instantiate(prefabLittleBalance, pointBalance.position, Quaternion.identity);
        Rigidbody2D rbBal = balance.GetComponent<Rigidbody2D>();

        if(rbBal != null)
        {
            rbBal.gravityScale = 2f; 

            float dirX = transform.localScale.x > 0 ? 1f : -1f;
            
            rbBal.AddForce(new Vector2(dirX * 6f, 8f), ForceMode2D.Impulse);
        }
    }

    public void Event_EnergyWave(string enumNameString)
    {
        if(System.Enum.TryParse(enumNameString, out PointToBalance pointEnum))
        {
            Transform realSpawnPoint = GetPointFromDictionary(pointEnum);

            Instantiate(prefabWave, realSpawnPoint.position, Quaternion.identity);
            
            GameObject onda2 = Instantiate(prefabWave, realSpawnPoint.position, Quaternion.identity);
            onda2.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private Transform GetPointFromDictionary(PointToBalance pointEnum)
    {
        if(dictionaryOfPoints.TryGetValue(pointEnum, out Transform foundPoint))
        {
            return foundPoint;
        }
        
        //Debug.LogWarning("Point " + pointEnum + " missing from the list. Using ThroughPoint as fallback.");
        return ThroughPoint; 
    }
}
