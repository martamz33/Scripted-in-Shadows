using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincipalMenu : MonoBehaviour
{
    // Function to New Game
    public void NewGame()
    {
        SceneManager.LoadScene("Hall");
    }

    // Function to button Close
    public void CloseGame()
    {
        Debug.Log("Going out the game");

        Application.Quit();
    }
}
