using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GhostAbility : MonoBehaviour
{
    public delegate void AbilityUsedAction();
    public event AbilityUsedAction OnAbilitySuccessfullyUsed;

    [Header("Settings")]
    public float totalChargeAbility = 80f;
    public float actualChargeAbility = 0f;
    public float totalTime = 50f;

    [Header("Canvas")]
    public Slider abilitySlider;
    public Image fillImage;

    private bool isCharge = true;
    private AbilityBase currentAbility;
    private bool selectedAbility = false;

    void Start()
    {
        actualChargeAbility = totalChargeAbility;

        if(abilitySlider != null)
        {
            abilitySlider.maxValue = totalChargeAbility;
            abilitySlider.value = totalChargeAbility;
        }

        GetComponent<MachasAbility>().enabled = false;
        GetComponent<PlumaAbility>().enabled = false;
        GetComponent<MonsterAbility>().enabled = false;
    }

    void Update()
    {
        if(!isCharge && actualChargeAbility < totalChargeAbility)
        {
            actualChargeAbility += (totalChargeAbility/totalTime) * Time.deltaTime;

            actualChargeAbility = Mathf.Clamp(actualChargeAbility, 0, totalChargeAbility);

            UpdateUI();

            if(actualChargeAbility == totalChargeAbility)
            {
                isCharge = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.F) && isCharge)
        {
            UseAbility();
        }
        
        if(GameManager.Instance.abilitySelected != AbilityType.None && !selectedAbility)
        {
            InitializeAbility();
        }
    }

    private void InitializeAbility()
    {
        selectedAbility = true;

        AbilityType ability = GameManager.Instance.abilitySelected;

        switch(ability)
        {
            case AbilityType.Manchas:
                currentAbility = GetComponent<MachasAbility>();
                GetComponent<MachasAbility>().enabled = true;
                break;
            case AbilityType.Pluma:
                currentAbility = GetComponent<PlumaAbility>();
                GetComponent<PlumaAbility>().enabled = true;
                break;
            case AbilityType.Monstruo:
                currentAbility = GetComponent<MonsterAbility>();
                GetComponent<MonsterAbility>().enabled = true;
                break;
            default:
                break;
        }
    }

    private void UpdateUI()
    {
        abilitySlider.value = actualChargeAbility;
    }

    private void UseAbility()
    {
        if(currentAbility != null)
        {
            currentAbility.PerformAbility();

            OnAbilitySuccessfullyUsed?.Invoke();

            actualChargeAbility = 0;
            isCharge = false;
            UpdateUI();
        }
    }

    //toEchoesOfThePast
    public void RepeatAbility(float delay)
    {
        if(currentAbility != null)
        {
            StartCoroutine(DelayRepetitionAttack(delay));
        }
    }

    private IEnumerator DelayRepetitionAttack(float delay)
    {   
        yield return new WaitForSeconds(delay);

        if(currentAbility != null)
        {
            currentAbility.PerformAbility();
        }
    }
}
