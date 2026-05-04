using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationPopup : MonoBehaviour
{
    public static NotificationPopup Instance;

    [Header("UI elements")]
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI notificationText;

    [Header("Settings")]
    public float displayTime = 2f;
    public float fadeTime = 0.5f;

    private Coroutine currentCoroutine;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Call this function when we buy a marker
    public void ShowNotification(string message)
    {
        notificationText.text = message;

        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // 1. Fade In
        float elapsedTime = 0f;
        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime/fadeTime);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // 2. wait time
        yield return new WaitForSecondsRealtime(displayTime);

        // 3. Fade out
        elapsedTime = 0f;
        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeTime));
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
