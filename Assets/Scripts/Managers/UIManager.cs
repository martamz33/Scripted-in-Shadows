using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct ConfigAbility
{
    public AbilityType abilityType;
    public string nameAbility;
    [TextArea] public string description;
    public Sprite icon;
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Elements")]
    public GameObject selectionPower;
    public ButtonPowerUpFantasy[] buttonsFantasy;

    [Header("Power-Up Pools")]
    [Tooltip("Los poderes específicos que da el Escritor/Guía al inicio")]
    public PowerUpScriptable[] writerPowerSelection;

    [Tooltip("Todos los poderes generales que pueden salir en las salas Equipment")]
    public List<PowerUpScriptable> generalPowerUpsPool;

    [Header("Configuración Habilidades Iniciales (La Guía)")]
    public GameObject selectionAbility;
    public ConfigAbility[] initialAbilities;
    public ButtonAbility[] abilityButtons;

    [Header("Tooltip Settings")] 
    public GameObject tooltipPanel; 
    public TextMeshProUGUI tooltipTitle;
    public TextMeshProUGUI tooltipDescription;

    private GhostMovement ghostMovement;

    private void Awake()
    {
        instance = this;

        ghostMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<GhostMovement>();
    } 

    public void OpenMenuOfPowerUps()
    {
        DisplayPowerUpMenu(writerPowerSelection);
    }

    public void OpenMenuOfGeneralPowerUps()
    {
        PowerUpScriptable[] randomPowers = GetRandomPowers(buttonsFantasy.Length);
        
        DisplayPowerUpMenu(randomPowers);
    }

    public void DisplayPowerUpMenu(PowerUpScriptable[] powersToShow)
    {
        selectionPower.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        if(ghostMovement != null) ghostMovement.isMovementActive = false;

        for(int i = 0; i < buttonsFantasy.Length; i++)
        {
            if(i < powersToShow.Length && powersToShow[i] != null)
            {
                buttonsFantasy[i].gameObject.SetActive(true);
                buttonsFantasy[i].Setup(powersToShow[i], this);
            }
            else
            {
                buttonsFantasy[i].gameObject.SetActive(false); 
            }
        }
    }

    public void OpenMenuOfAbilities()
    {
        selectionAbility.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if(ghostMovement != null) ghostMovement.isMovementActive = false;

        for(int i = 0; i< abilityButtons.Length; i++)
        {
            if(i < initialAbilities.Length)
            {
                abilityButtons[i].gameObject.SetActive(true);

                abilityButtons[i].SetupAbility(initialAbilities[i], this);
            }
            else
            {
                abilityButtons[i].gameObject.SetActive(false); 
            }
        }
    }

    public void closeMenu()
    {
        selectionPower.SetActive(false);
        selectionAbility.SetActive(false);
        HideTooltip();
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if(ghostMovement != null) ghostMovement.isMovementActive = true;
    }

    private PowerUpScriptable[] GetRandomPowers(int amount)
    {
        List<PowerUpScriptable> poolTemp = new List<PowerUpScriptable>(generalPowerUpsPool);
        
        //1. Filter
        if(PlayerPrefs.GetInt("TimeDoesNotForgetUnlocked", 0) == 1)
        {
            poolTemp.RemoveAll(p => p.powerUpName == FantasyPowerUp.TimeDoesNotForget);
        }

        // 2. Iron and Paper Skin are exclusive
        if(GameManager.Instance.hasIronSkin || GameManager.Instance.hasPaperSkin)
        {
            poolTemp.RemoveAll(p => p.powerUpName == FantasyPowerUp.IronSkin || p.powerUpName == FantasyPowerUp.PaperSkin); 
        }
        
        // 3. Random Selection
        List<PowerUpScriptable> selected = new List<PowerUpScriptable>();

        while(selected.Count < amount && poolTemp.Count > 0)
        {
            int randomIndex = Random.Range(0, poolTemp.Count);
            PowerUpScriptable obtainPowerUp = poolTemp[randomIndex];

            selected.Add(obtainPowerUp);
            poolTemp.RemoveAt(randomIndex);

            if(obtainPowerUp.powerUpName == FantasyPowerUp.IronSkin || obtainPowerUp.powerUpName == FantasyPowerUp.PaperSkin)
            {
                FantasyPowerUp counterpartType = (obtainPowerUp.powerUpName == FantasyPowerUp.IronSkin)
                                            ? FantasyPowerUp.PaperSkin
                                            : FantasyPowerUp.IronSkin;
                
                PowerUpScriptable counterpart = poolTemp.Find(p => p.powerUpName == counterpartType);

                if (counterpart != null && selected.Count < amount)
                {
                    selected.Add(counterpart);
                    poolTemp.Remove(counterpart);
                }
            }
        }

        return selected.ToArray();
    }

    // Function of Tooltip
    public void ShowTooltip(string title, string description)
    {
        if(tooltipPanel == null)  return;

        tooltipTitle.text = title;
        tooltipDescription.text = description;
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        if(tooltipPanel != null) tooltipPanel.SetActive(false);
    }
}
