using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincipalMenu : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject statsPanel;
    public GameObject optionsPanel; 

    // Function to New Game
    public void NewGame()
    {
       if (SceneLoader.Instance != null) {
            SceneLoader.Instance.LoadSceneWithFade(sceneToLoad);
        } else {
            // Fallback por si el loader no está en la escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Function to button Close
    public void CloseGame()
    {
        Debug.Log("Going out the game");

        Application.Quit();
    }

    public void OpenStatsPanel()
    {
        statsPanel.SetActive(true);
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
        if(statsPanel.activeSelf)
        {
            statsPanel.SetActive(false);
        }
        
        if(optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
        }
    }
}
