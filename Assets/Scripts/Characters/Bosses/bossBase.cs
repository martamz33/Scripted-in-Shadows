using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class bossBase : MonoBehaviour, IDamagable
{
    public bossStates currentState = bossStates.Waiting;

    [Header("Base Stadistics")]
    public int baseHealth = 600;
    public int actualHealth;
    public int totalHealth;
    public bool isAttacking = false;

    protected Animator animator;
    protected Rigidbody2D rb;
    protected Transform player;

    [Header("stock market system")]
    protected List<int> attackStock = new List<int>();
    private List<int> attacksAvailable = new List<int>();

    [Header("Canvas Health")]
    public Slider healthSlider;

    protected virtual void Start()
    {
        float healthMult = GameManager.Instance != null ? GameManager.Instance.enemyHealthMultiplier : 1f;
        totalHealth = Mathf.RoundToInt(baseHealth * healthMult); 
        actualHealth = totalHealth;

        healthSlider.maxValue = totalHealth;
        healthSlider.value = totalHealth;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        switch(currentState)
        {
            case bossStates.Phase1:
                if(!isAttacking) StartCoroutine(PatternsPhase1());
                break;
            case bossStates.Phase2:
                if(!isAttacking) StartCoroutine(PatternsPhase2());
                break;
        }
    }
    
    private void UpdateUI()
    {
        healthSlider.value = actualHealth;
    }

    // --- VIRTUAL FUNCTION TO FILL IN BY CHILDS
    protected virtual IEnumerator PatternsPhase1() { yield return null;}
    protected virtual IEnumerator PatternsPhase2() { yield return null;}

    // --- STOCK SYSTEM ---

    // Call at the beggining to fill in the cards in the stock
    protected void ConfigureStock(int[] attacks)
    {
        attackStock.Clear();
        attackStock.AddRange(attacks);
        RefreshStock();
    }

    private void RefreshStock()
    {
        attacksAvailable = new List<int>(attackStock);

        for(int i = 0; i < attacksAvailable.Count; i++)
        {
            int temp = attacksAvailable[i];
            int randomIndex = Random.Range(i, attacksAvailable.Count);
            attacksAvailable[i] = attacksAvailable[randomIndex];
            attacksAvailable[randomIndex] = temp;
        }
    }

    protected int ObtainNextAttack()
    {
        if(attacksAvailable.Count == 0)
        {
            RefreshStock();
        }

        int attackTaken = attacksAvailable[0];
        attacksAvailable.RemoveAt(0);
        return attackTaken;
    }

    // --- DAMAGE AND PHASE SYSTEM ---
    public virtual void TakeDamage(int damage)
    {
        if(currentState == bossStates.Waiting || currentState == bossStates.Transcition || currentState == bossStates.Dead) return;

        actualHealth -= damage;

        UpdateUI();
    }

    protected void Dead()
    {
        currentState = bossStates.Dead;
        StopAllCoroutines();
        rb.velocity = Vector2.zero;
        animator.SetTrigger("dead");
    }

    // --- VISUAL FUNCITON ---
    protected void SeeThePlayer()
    {
        if (player == null) return;

        if (player.position.x > transform.position.x)
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    else
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
