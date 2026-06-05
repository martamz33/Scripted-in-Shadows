using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menus;

    [Header("Extra Panels")]
    public GameObject optionsPanel; 
    public string initialMenuSceneName = "InitialMenu";

    [Header("UI References")]
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private bool isActivated;

    void Start()
    {
        isActivated = false;

        if(optionsPanel != null) optionsPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        LoadVolumeSettings();
        LoadResolutionSettings();
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

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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

    private void LoadVolumeSettings()
    {
        if (volumeSlider != null)
        {
            // Load the save volumne or per default at max (1)
            float savedVolume = PlayerPrefs.GetFloat("GlobalVolume", 1f);
            
            volumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume;

            // Add a Listener
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

                // Check if it is the real resolution
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);

            // Load the save resolution or the default one
            int savedResIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
            resolutionDropdown.value = savedResIndex;
            resolutionDropdown.RefreshShownValue();

            // Authomatic event when change the dropdown
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, true); 
        
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void ReturnToMainMenu()
    {

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveRunState();
        }
        
        Time.timeScale = 1f; 
        
        if (SceneLoader.Instance != null) {
            SceneLoader.Instance.LoadSceneWithFade(initialMenuSceneName);
        } else {
            // Fallback por si el loader no está en la escena
            SceneManager.LoadScene(initialMenuSceneName);
        }
    }
}
