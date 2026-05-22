using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menus;

    [Header("Extra Panels")]
    public GameObject optionsPanel; 
    public string initialMenuSceneName = "InitialMenu";

    private bool isActivated;

    void Start()
    {
        isActivated = false;

        if(optionsPanel != null) optionsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ShopManager.Instance.ShopIsOpen)
        {
            if (!isActivated)
            {
                openMenu();
            }
            else
            {
                if (optionsPanel != null && optionsPanel.activeSelf)
                {
                    CloseOptions();
                }
                else // Si estamos en la pausa normal, quitamos la pausa
                {
                    closeMenu();
                }
            }
        }
    }

    void openMenu()
    {
        int index = -1;

        switch (GameManager.genre)
        {
            case Genre.hall:     index = 0; break;
            case Genre.fantasy:  index = 1; break;
            case Genre.mistery:  index = 2; break;
            case Genre.terror:   index = 3; break;
            case Genre.forget:   index = 4; break;
            case Genre.depth:    index = 5; break;
        }

        if (index >= 0 && index < menus.Length)
        {
            menus[index].SetActive(true);
            isActivated = true;

            //Pause the time
            Time.timeScale = 0f;
        }
    }

    public void closeMenu()
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        if (optionsPanel != null) optionsPanel.SetActive(false);
        isActivated = false;

        Time.timeScale = 1f;
    }

    public void OpenOptions()
    {
        if(optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        if(optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        
        SceneManager.LoadScene(initialMenuSceneName);
    }
}
