using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public delegate void EnemyDeathAction(Vector3 deathPosition);
    public static event EnemyDeathAction OnAnyEnemyDeath;

    public static event System.Action OnEnemyDamaged;

    [Header("Health Stats")]
    public int baseHealth = 100;
    public int actualHealth;
    public float distanceDetection = 15f;
    public int healhMax;

    [Header("Damage Stats")]
    public int damage;
    public int damageBase = 4;

    [Header("Effects to death")]
    public GameObject PrefabParticules;
    public GameObject PrefabInk;
    public int quantityOfInk;
    public float forceOfExplosion = 5f;

    [Header("State")]
    public EnemyState currentState;
    public bool isFlying;

    [Header("References")]
    protected Animator animator;
    protected Transform player;
    protected Rigidbody2D rg;    

    protected virtual void Start()
    {
        if(currentState == EnemyState.Death) return;

        healhMax = Mathf.RoundToInt(baseHealth * GameManager.Instance.enemyHealthMultiplier);
        damage = Mathf.RoundToInt(damageBase * GameManager.Instance.enemyDamageMultiplier);

        actualHealth = healhMax;
        animator = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj!=null) player = playerObj.transform;
    }

    protected virtual void Update()
    {
        if(player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        HandleStateMachine(distance);
    }

    private void HandleStateMachine(float distance)
    {
        switch(currentState)
        {
            case EnemyState.Idle:       OnIdle(distance); break;
            case EnemyState.Walk:       OnWalk(distance); break;
            case EnemyState.Attack:     OnAttack(distance); break;
            case EnemyState.Fly:        OnFly(distance); break;
            case EnemyState.Teleport:   OnTeleport(distance); break;
        }
    }
    protected virtual void OnIdle(float distance) {}
    protected virtual void OnWalk(float distance) {}
    protected abstract void OnAttack(float distance);
    protected virtual void OnFly(float distance) {}
    protected virtual void OnTeleport(float distance) {}
    
    public virtual void TakeDamage(int damage)
    {
        actualHealth -= damage;
        Debug.Log(gameObject.name + "take damage. Actual Health: " + actualHealth);

        OnEnemyDamaged?.Invoke();

        if(actualHealth <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        currentState = EnemyState.Death;

        if(OnAnyEnemyDeath != null) OnAnyEnemyDeath(transform.position);

        if(GetComponent<Collider2D>()) GetComponent<Collider2D>().enabled = false;
        foreach(SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>()) r.enabled = false;

        InstantiateEffectAndInk();

        Destroy(gameObject);
    }

    protected virtual void InstantiateEffectAndInk()
    {
        if(PrefabParticules != null)
        {
            GameObject particules = Instantiate(PrefabParticules, transform.position, Quaternion.identity);
            Destroy(particules, 3f);
        }

        if(PrefabInk != null)
        {
            for(int i = 0; i<quantityOfInk; i++)
            {
                GameObject Ink = Instantiate(PrefabInk, transform.position + Vector3.up, Quaternion.identity);
                
                Rigidbody2D rbInk = Ink.GetComponent<Rigidbody2D>();
                if(rbInk != null)
                {
                    Vector3 flyDirection = new Vector2 (
                        Random.Range(-1f, 1f),
                        Random.Range(0.5f, 1.5f)).normalized;
                    
                    rbInk.AddForce(flyDirection * forceOfExplosion, ForceMode2D.Impulse);
                }
            }
        }

        return;
    }
}
