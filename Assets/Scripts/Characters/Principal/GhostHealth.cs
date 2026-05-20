using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GhostHealth : MonoBehaviour, IDamagable
{
    [Header("Settings")]
    public int totalHealth = 50;
    public int actualHealth;
    public bool isInvulnerable = false;

    //powerup variables
    public bool canHeal = true;
    public float multiplier = 1f;

    [Header("CanvasSettings")]
    public Slider healthSlider;
    public Image fillImage;
    public Gradient healthGradient;
    public TextMeshProUGUI healthText;

    [Header("Image Healh Bar")]
    public Image healthPortrait;
    public Sprite ghostImage100;
    public Sprite ghostImage75;
    public Sprite ghostImage50;
    public Sprite ghostImage25;

    [Header("Impact Feedback")]
    //public GameObject hitParticlesPrefab;
    public float knockbackForceX = 3f;
    public float knockbackForceY = 3f;
    public float knockbackDuration = 0.2f;

    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rg;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rg = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);
        actualHealth = totalHealth;
        if (SceneManager.GetActiveScene().name == "Hall") 
        {
            actualHealth = totalHealth;
            GameManager.Instance.playerCurrentHealth = actualHealth;
        }
        else 
        {
            actualHealth = GameManager.Instance.playerCurrentHealth;
        }

        if(healthSlider != null)
        {
            healthSlider.maxValue = totalHealth;
            healthSlider.value = totalHealth;
        }

        if(healthText != null)
        {
            healthText.text = actualHealth.ToString() + "/" + totalHealth.ToString();
        }
        
        if(fillImage != null)
        {
            fillImage.color = healthGradient.Evaluate(1f);
        }
    }

    public void Heal(int amount)
    {
        if(!canHeal) return;

        int finalHealth = Mathf.RoundToInt(amount * multiplier);

        actualHealth += finalHealth;
        actualHealth = Mathf.Clamp(actualHealth, 0, totalHealth);
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerCurrentHealth = actualHealth;
        }
        
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        if(isInvulnerable) return;

        actualHealth -= damage;
        actualHealth = Mathf.Clamp(actualHealth, 0, totalHealth);
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerCurrentHealth = actualHealth;
        }
        
        UpdateUI();

        if(actualHealth <= 0)
        {
            Death();
        }
        else
        {
            StartCoroutine(ApplyKnockback());
        }
    }

    private IEnumerator ApplyKnockback()
    {
        isInvulnerable = true;

        for (int i = 0; i < 3; i++) 
        {
            sprite.color = new Color(1f, 0.5f, 0.5f, 0.7f); // Rojo semitransparente
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white; // Vuelve a la normalidad
            yield return new WaitForSeconds(0.1f);
        }

        isInvulnerable = false;
    }

    private void UpdateMaxHealthUI()
    {
        healthSlider.maxValue = totalHealth;
    }

    private void UpdateUI()
    {
        healthSlider.value =actualHealth;

        healthText.text = actualHealth.ToString() + "/" + totalHealth.ToString();

        float percentageHealh = (float)actualHealth / totalHealth;

        fillImage.color = healthGradient.Evaluate(percentageHealh);
        
        if(percentageHealh  > 0.75f)
        {
            healthPortrait.sprite = ghostImage100;
        }
        else if(percentageHealh  > 0.5f)
        {
            healthPortrait.sprite = ghostImage75;
        }
        else if(percentageHealh  > 0.25f)
        {
            healthPortrait.sprite = ghostImage50;
        }
        else
        {
            healthPortrait.sprite = ghostImage25;
        }
    }

    private void Death()
    {
        animator.SetBool("isAlive", false);

        if(InkManager.instance != null)
        {
            InkManager.instance.ResetInk();
        }

        if(GameManager.Instance != null)
        {
            GameManager.Instance.RecordPlayerDeath();
        }

        GoToHall();
    }

    private void GoToHall()
    {
        SceneManager.LoadScene("Hall");
    }

    //functions for rooms or power ups
    public void ModifyMaxHealthPercentage(float percentage)
    {
        int damageTaken = totalHealth - actualHealth;

        int amountToChange = Mathf.RoundToInt(totalHealth * percentage);
        totalHealth += amountToChange;

        if(percentage > 0)
        {
            actualHealth += amountToChange;
        }
        else
        {
            actualHealth = totalHealth - damageTaken;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.playerCurrentHealth = actualHealth;
            }

            actualHealth = Mathf.Clamp(actualHealth, 0, totalHealth);
        }

        UpdateMaxHealthUI();
        UpdateUI();
    }

    public void AddFlatMaxHealth(int amount)
    {
        totalHealth +=amount;
        actualHealth +=amount;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerCurrentHealth = actualHealth;
        }
        UpdateMaxHealthUI();
        UpdateUI();
    }

    //Dissapareance Of Salvation
    public void DissapearanceOfSalvationInvulneraty(float minTime, float maxTime, float duration)
    {
        StartCoroutine(Invulneravility(minTime, maxTime, duration));
    }

    private IEnumerator Invulneravility(float minTime, float maxTime, float duration)
    {
        while(actualHealth > 0)
        {
            float waitTime = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(waitTime);

            // ---Invulnerable and transparent ---
            isInvulnerable = true;

            if(sprite != null)
            {
                Color c = sprite.color;
                c.a = 0.5f;
                sprite.color = c;
            }

            yield return new WaitForSeconds(duration);

            isInvulnerable = false;

            if(sprite != null)
            {
                Color c = sprite.color;
                c.a = 1f;
                sprite.color = c;
            }
        }
    }
}
