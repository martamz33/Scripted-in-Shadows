using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("El panel de fondo/imágenes del tutorial que se apagará")]
    public GameObject panelTutorial;

    private bool isActive = false;

    void Start()
    {
        if(MapManager.Instance != null && MapManager.Instance.roomsCleared <= 1)
        {
            ActivateTutorial();
        }
        else
        {
            if(panelTutorial != null) panelTutorial.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(isActive)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                CloseTutorial();
            }
        }
    }

    private void ActivateTutorial()
    {
        isActive = true;
        if(panelTutorial != null) panelTutorial.SetActive(true);

        if(GameManager.Instance != null)
        {
            GameManager.Instance.FreezePlayer(true);
        }
    }

    private void CloseTutorial()
    {
        isActive = false;
        if(panelTutorial != null) panelTutorial.SetActive(false);

        if(GameManager.Instance != null)
        {
            GameManager.Instance.FreezePlayer(false);
        }

        gameObject.SetActive(false);
    }
}
