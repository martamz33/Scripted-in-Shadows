using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [System.Serializable]
    public struct Portrait // Guardará el nombre del icono y su imagen
    {
        public string iconName;
        public Sprite iconSprite;
    }

    [Header("Configuration")]
    public TextAsset inkJSONAsset;
    private Story story;

    [Header("Canvas per level")]
    public List<CanvasGroup> levelCanvases;

    [Header("UI elements")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject dialoguePanel;
    public Image portraitImage;
    public List<Portrait> availablePortraits;

    //dictionary to know which dialogues are alredy been used
    private HashSet<string> completedDialogues = new HashSet<string>();

    private bool isWaitingForChoice;

    private System.Action onDialogueFinishedCallback;

    void Awake()
    {
        instance = this;
        if(inkJSONAsset != null)
        {
            story = new Story(inkJSONAsset.text);
        }
    }
    
    public void TriggerDialogue(string knotName)
    {
        if(completedDialogues.Contains(knotName))
        {
            Debug.Log("Este dialogo ya no esta disponible");
            return;
        }

        if(GameManager.Instance != null) 
        {
            GameManager.Instance.FreezePlayer(true);
        }

        onDialogueFinishedCallback = null;

        story.ChoosePathString(knotName);

        dialoguePanel.SetActive(true);

        StartCoroutine(DisplayDialogue());
    }

    public void StartExternalDialogue(TextAsset externalInkJSON, System.Action onFinish = null)
    {
        if(externalInkJSON == null)
        {
            Debug.LogWarning("Se intentó iniciar un diálogo pero el archivo Ink está vacío.");
            return;
        }

        if(GameManager.Instance != null) 
        {
            GameManager.Instance.FreezePlayer(true);
        }

        onDialogueFinishedCallback = onFinish;

        story = new Story(externalInkJSON.text);

        dialoguePanel.SetActive(true);

        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator  DisplayDialogue()
    {
        while(story.canContinue)
        {
            Debug.Log("HA entrado en displayDialogue");
            string text = story.Continue();

            text = text.Trim();

            ParseTags(story.currentTags);

            if (story.currentTags.Contains("OpenShop"))
            {
                break; 
            }

            if (string.IsNullOrWhiteSpace(text)) 
            {
                continue; 
            }
            
            dialogueText.text = text;

            if(isWaitingForChoice)
            {
                dialoguePanel.SetActive(false);
                UIManager.instance.OpenMenuOfPowerUps();

                yield return new WaitUntil(() => !isWaitingForChoice);

                dialoguePanel.SetActive(true);
            }
            else
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }

            yield return new WaitForEndOfFrame();
        }

        dialoguePanel.SetActive(false);
        if (!isWaitingForChoice && GameManager.Instance != null) 
        {
            GameManager.Instance.FreezePlayer(false);
        }

        onDialogueFinishedCallback?.Invoke();
    }

    private void ParseTags(List<string> tags)
    {
        foreach(string tag in tags)
        {
            if (tag.Trim() == "OPEN_UI")
            {
                Debug.Log("Tag de abrir UI encontrado correctamente");
                isWaitingForChoice = true;
                continue;
            }

            string[] splitTag = tag.Split(":");
            if(splitTag.Length < 2) continue;

            string key = splitTag[0].Trim();
            string value = splitTag[1].Trim();

            switch(key)
            {
                case "canvas":
                    ActivateCanvas(value);
                    break;
                case "repeat":
                    if(value == "false")
                    {
                        completedDialogues.Add(story.currentChoices[0].sourcePath);
                    }
                    break;
                case "speaker":
                    if(nameText!= null) nameText.text = value;
                    break;
                case "icon":
                    foreach (Portrait p in availablePortraits)
                    {
                        if (p.iconName == value)
                        {
                            portraitImage.sprite = p.iconSprite;
                            portraitImage.gameObject.SetActive(true); 
                            break;
                        }
                    }
                    break;
            }
        }
    }

    public void ContinueAfterPowerUpChoice(string powerInkID)
    {
        story.variablesState["poder_elegido"] = powerInkID;

        isWaitingForChoice = false;
    }

    private void ActivateCanvas(string levelName)
    {
        foreach(var canvas in levelCanvases)
        {
            canvas.alpha = (canvas.name == levelName) ? 1 : 0;
            canvas.blocksRaycasts = (canvas.name == levelName);
        }
    }
}
