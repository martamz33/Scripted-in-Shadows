using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PrincipalMenu : MonoBehaviour
{
    [Header("Scenes")]
    public string sceneToLoad;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject statsPanel;
    public GameObject optionsPanel; 

    [Header("Buttons")]
    public Button continueButton;
    public Slider volumeSlider;            
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    void Start()
    {
        if (continueButton != null)
        {
            if (PlayerPrefs.HasKey("SavedScene"))
            {
                continueButton.interactable = true; // ACtive button
            }
            else
            {
                continueButton.interactable = false; // Turn off if is not available game
            }
        }

        LoadVolumeSettings();
        LoadResolutionSettings();
    }

    private void LoadVolumeSettings()
    {
        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("GlobalVolume", 1f);
            volumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume;

            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; 
        PlayerPrefs.SetFloat("GlobalVolume", volume); 
    }

    private void LoadResolutionSettings()
    {
        if (resolutionDropdown != null)
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();
            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);

            int savedResIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
            resolutionDropdown.value = savedResIndex;
            resolutionDropdown.RefreshShownValue();

            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, true); 
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void ContinueGame()
    {
        // Choose the save scene
        string sceneToContinue = PlayerPrefs.GetString("SavedScene", sceneToLoad);
        
        LoadScene(sceneToContinue);
    }

    // Function to New Game
    public void NewGame()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.ClearRunState();
        }

        LoadScene(sceneToLoad);
    }

    private void LoadScene(string sceneName)
    {
        if (SceneLoader.Instance != null) 
        {
            SceneLoader.Instance.LoadSceneWithFade(sceneName);
        } 
        else 
        {
            SceneManager.LoadScene(sceneName);
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
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false); // Apagamos el menú principal
        if (statsPanel != null) statsPanel.SetActive(true);
    }

    public void OpenOptions()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false); // Apagamos el menú principal
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }


    public void ReturnToMainMenu()
    {
        if (statsPanel != null) statsPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);

        // Volvemos a encender el menú principal
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }
}
