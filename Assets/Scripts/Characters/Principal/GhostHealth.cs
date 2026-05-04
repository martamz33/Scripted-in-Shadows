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

    private Animator animator;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);
        actualHealth = totalHealth;

        healthSlider.maxValue = totalHealth;
        healthSlider.value = totalHealth;

        healthText.text = actualHealth.ToString() + "/" + totalHealth.ToString();

        fillImage.color = healthGradient.Evaluate(1f);
    }

    public void Heal(int amount)
    {
        if(!canHeal) return;

        int finalHealth = Mathf.RoundToInt(amount * multiplier);

        actualHealth += finalHealth;
        actualHealth = Mathf.Clamp(actualHealth, 0, totalHealth);
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        if(isInvulnerable) return;

        actualHealth -= damage;
        actualHealth = Mathf.Clamp(actualHealth, 0, totalHealth);
        UpdateUI();

        if(actualHealth <= 0)
        {
            Death();
        }
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
    }

    private void GoToHall()
    {
        //SceneManager.LoadScene("Hall");
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

            actualHealth = Mathf.Clamp(actualHealth, 0, totalHealth);
        }

        UpdateMaxHealthUI();
        UpdateUI();
    }

    public void AddFlatMaxHealth(int amount)
    {
        totalHealth +=amount;
        actualHealth +=amount;
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
