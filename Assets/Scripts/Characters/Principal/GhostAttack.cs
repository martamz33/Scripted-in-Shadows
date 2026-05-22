using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ConfigAttack
{
    public GhostAttackType typeAttack;
    public GameObject prefabAttack;
    public Transform startAttackPoint;
    public Transform finalAttackPoint;
    public int baseDamage;
}

public class GhostAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackDuration = 0.4f;
    public float downVelocity = 20f;
    public float upVelocity = 10f;
    public float damageMultiplier = 1f;
    public List<ConfigAttack> listOfAttacks = new List<ConfigAttack>();

    [Header("Queue Setting")]
    public int maxQueueSize = 3;

    private Rigidbody2D rg;
    private GhostMovement gm;
    private Animator animator;
    private bool isAttacking = false;
    private Queue<TypeAttack> attackQueue = new Queue<TypeAttack>();
    private Coroutine queueCoroutine;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        gm = GetComponent<GhostMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!gm.isMovementActive) return;

        HandleInput();
    }

    private void HandleInput()
    {
        TypeAttack? attackToQueue = null;
        bool esAtaquePrioritario = false;

        // 1. Attack Down (Prioridad máxima)
        if(!gm.IsGrounded && Input.GetMouseButtonDown(1) && Input.GetAxisRaw("Vertical") < -0.1f)
        {
            attackToQueue = TypeAttack.AtaqueA;
            esAtaquePrioritario = true;
        }
        // 2. Up Attack (Prioridad alta)
        else if(Input.GetMouseButtonDown(0) && (Input.GetAxisRaw("Vertical") > 0.1f || Input.GetKey(KeyCode.Space)))
        {
            attackToQueue = TypeAttack.AtaqueUp;
            esAtaquePrioritario = true;
        }
        // 3. Main Attack
        else if(Input.GetMouseButtonDown(0))
        {
            attackToQueue = TypeAttack.AtaqueP;
        }
        // 4. Secundary Attack
        else if(Input.GetMouseButtonDown(1))
        {
            attackToQueue = TypeAttack.AtaqueS;
        }

        if(attackToQueue.HasValue)
        {
            // Si es un ataque hacia arriba o abajo, limpiamos la basura de la cola para que responda al instante
            if (esAtaquePrioritario)
            {
                attackQueue.Clear();
            }

            if(attackQueue.Count < maxQueueSize)
            {
                attackQueue.Enqueue(attackToQueue.Value);
                
                if(!isAttacking)
                {
                    StartCoroutine(ProcessAttackQueue());
                }
            }
        }
    }

    private IEnumerator ProcessAttackQueue()
    {
        isAttacking = true;

        while(attackQueue.Count > 0)
        {
            TypeAttack nextAttack = attackQueue.Dequeue();

            yield return StartCoroutine(PerformAttack(nextAttack));
        }

        isAttacking = false;
    }

    private IEnumerator PerformAttack(TypeAttack attack)
    {
        gm.isOscilationActive = false;

        animator.SetTrigger(attack.ToString());

        if(attack == TypeAttack.AtaqueA)
        {
            float originalSpeed = gm.movementSpeed;
            gm.movementSpeed = 0;

            while(!gm.IsGrounded)
            {
                rg.velocity = new Vector2(0, -downVelocity);
                yield return null;
            }
            rg.velocity = Vector2.zero;
            gm.UpdateValueY();
            yield return new WaitForSeconds(0.1f);
            gm.movementSpeed = originalSpeed;
        }
        else if (attack == TypeAttack.AtaqueUp) 
        {
            rg.velocity = new Vector2(rg.velocity.x, upVelocity);
            yield return new WaitForSeconds(attackDuration);
        }
        else
        {
            yield return new WaitForSeconds(attackDuration);
        }

        gm.isOscilationActive = true;
    }

    //Instantiate Attacks
    public void LaunchAttack(int indexEnum)
    {
        GhostAttackType typeSelected = (GhostAttackType)indexEnum;
        ConfigAttack configFound = listOfAttacks.Find(a => a.typeAttack == typeSelected);

        if(configFound.prefabAttack != null)
        {
            GameObject attack = Instantiate(configFound.prefabAttack, configFound.startAttackPoint.position, Quaternion.identity);

            attack.transform.SetParent(this.transform);

            //Ajust the scale to face the ghost
            /*
            Vector3 scale = attack.transform.localScale;
            scale.x = transform.localScale.x;
            attack.transform.localScale = scale;*/

            int finalDamage = Mathf.RoundToInt(configFound.baseDamage * damageMultiplier);

            AttackDamage scripDamage = attack.GetComponent<AttackDamage>();
            if(scripDamage != null)
            {
                scripDamage.SetUp(finalDamage, configFound.finalAttackPoint);
            }            
        }      
    }

    public void CancelAllAttacks()
    {
        if(queueCoroutine != null)
        {
            StopCoroutine(queueCoroutine);
            queueCoroutine = null;
        }

        attackQueue.Clear();

        isAttacking = false;
        gm.isOscilationActive = true;
        gm.movementSpeed = 10f;

        animator.ResetTrigger("AtaqueP");
        animator.ResetTrigger("AtaqueS");
        animator.ResetTrigger("AtaqueA");
        animator.ResetTrigger("AtaqueUp");
    }
}
