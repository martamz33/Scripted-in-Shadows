using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject selectionPower;
    public ButtonPowerUpFantasy[] buttonsFantasy;
    public PowerUpScriptable[] myActualPowerSelection;

    private GhostMovement ghostMovement;

    private void Awake()
    {
        instance = this;

        ghostMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<GhostMovement>();
    } 

    public void OpenMenuOfPowerUps()
    {
        selectionPower.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ghostMovement.isMovementActive = false;

        for(int i = 0; i < buttonsFantasy.Length; i++)
        {
            buttonsFantasy[i].Setup(myActualPowerSelection[i], this);
        }
    }

    public void closeMenu()
    {
        selectionPower.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //ghostMovement.isMovementActive = true;
    }
}
