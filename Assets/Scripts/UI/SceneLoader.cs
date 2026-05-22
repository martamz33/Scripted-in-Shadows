using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public CanvasGroup fadeCanvasGroup; // Arrastra aquí el panel negro
    public float fadeSpeed = 1f;

    void Awake() 
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        // 1. Fundido a negro (Alpha 0 -> 1)
        while (fadeCanvasGroup.alpha < 1)
        {
            fadeCanvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // 2. Cargar la escena
        SceneManager.LoadScene(sceneName);

        // 3. Fundido desde negro (Alpha 1 -> 0)
        while (fadeCanvasGroup.alpha > 0)
        {
            fadeCanvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
}