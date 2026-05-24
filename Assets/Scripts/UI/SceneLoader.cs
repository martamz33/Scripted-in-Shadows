using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public CanvasGroup fadeCanvasGroup; 
    public float fadeSpeed = 1f;

    void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (fadeCanvasGroup != null)
        {
            StartCoroutine(FadeIn());
        }
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

    private IEnumerator FadeIn()
    {
        // Fundido desde negro (Alpha 1 -> 0)
        while (fadeCanvasGroup.alpha > 0)
        {
            fadeCanvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // Permitimos hacer clic de nuevo al terminar
        fadeCanvasGroup.blocksRaycasts = false;
    }
}