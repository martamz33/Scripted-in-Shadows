using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;

public class ButtonAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ConfigAbility myConfig;
    private UIManager myUIManager;

    [Header("UI Elements")]
    public Image iconImage;

    [Header("Audio Settings")]
    public AudioClip clickSound;

    public void SetupAbility(ConfigAbility config, UIManager manager)
    {
        myConfig = config;
        myUIManager = manager;

        if(iconImage != null && myConfig.icon != null)
        {
            iconImage.sprite = myConfig.icon;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if(myUIManager != null)
        {
            myUIManager.ShowTooltip(myConfig.nameAbility, myConfig.description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (myUIManager != null)
        {
            myUIManager.HideTooltip();
        }
    }

    public void OnClick()
    {
        if (clickSound != null)
        {
            GameObject audioTemp = new GameObject("TempUIClick");
            AudioSource aSource = audioTemp.AddComponent<AudioSource>();
            aSource.clip = clickSound;
            aSource.spatialBlend = 0f; // 0 = 2D puro, ideal para UI
            aSource.volume = 1f;
            
            aSource.Play();
            Destroy(audioTemp, clickSound.length);
        }
        
        if(GameManager.Instance != null)
        {
            GameManager.Instance.abilitySelected = myConfig.abilityType;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerAbilityManager abilityManager = player.GetComponent<PlayerAbilityManager>();
            if (abilityManager != null)
            {
                abilityManager.ActivateAbility(myConfig.abilityType);
                Debug.Log("Script de habilidad encendido en el fantasma.");
            }
        }

        myUIManager.closeMenu();

        if(DialogueManager.instance != null)
        {
            DialogueManager.instance.ContinueAfterPowerUpChoice();
        }
    }

    
}
