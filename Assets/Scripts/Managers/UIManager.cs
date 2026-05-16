using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void closeMenu()
    {
        selectionPower.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if(ghostMovement != null) ghostMovement.isMovementActive = true;
    }

    private PowerUpScriptable[] GetRandomPowers(int amount)
    {
        List<PowerUpScriptable> poolTemp = new List<PowerUpScriptable>(generalPowerUpsPool);
        PowerUpScriptable[] selected = new PowerUpScriptable[amount];

        for (int i = 0; i < amount; i++)
        {
            if (poolTemp.Count == 0) break; 
            
            int randomIndex = Random.Range(0, poolTemp.Count);
            selected[i] = poolTemp[randomIndex];
            
            poolTemp.RemoveAt(randomIndex); 
        }

        return selected;
    }
}
