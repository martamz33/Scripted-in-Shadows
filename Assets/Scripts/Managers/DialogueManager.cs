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
    public struct Portrait 
    {
        public string iconName;
        public Sprite iconSprite;
        public AudioClip voiceSound;
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

    [Header("Voice & Typewriter Settings")]
    public AudioSource audioSource;
    public float typingSpeed = 0.04f; 
    [Range(0f, 0.5f)]
    public float pitchVariation = 0.05f; 
    private AudioClip currentVoiceClip;

    //dictionary to know which dialogues are alredy been used
    private HashSet<string> completedDialogues = new HashSet<string>();
    private bool isWaitingForChoice;
    private System.Action onDialogueFinishedCallback;

    // To know if the variable has to start 
    private bool willStartRun = false;
    private string menuToOpen;

    public string lastDisplayedText { get; private set; } = "";
    public string lastDisplayedName { get; private set; } = "";
    public string lastDisplayedIcon { get; private set; } = "";
    private string currentIconName = "";


    void Awake()
    {
        instance = this;
        if(inkJSONAsset != null)
        {
            story = new Story(inkJSONAsset.text);
        }
    }
    
    public void TriggerDialogue(string knotName, System.Action onFinish = null)
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

        onDialogueFinishedCallback = onFinish;

        if (GameManager.Instance != null && story.variablesState.Contains("guide_talk_count"))
        {
            story.variablesState["guide_talk_count"] = GameManager.Instance.guideTalkCount;
        }

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

        if (GameManager.Instance != null && story.variablesState.Contains("guide_talk_count"))
        {
            story.variablesState["guide_talk_count"] = GameManager.Instance.guideTalkCount;
        }

        dialoguePanel.SetActive(true);

        willStartRun = false;

        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        while(story.canContinue)
        {
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

            lastDisplayedText = text;
            lastDisplayedName = nameText != null ? nameText.text : "";
            lastDisplayedIcon = currentIconName;

            if(isWaitingForChoice)
            {
                dialoguePanel.SetActive(false);
                if (menuToOpen == "PowerUp")
                {
                    UIManager.instance.OpenMenuOfPowerUps();
                }
                else if (menuToOpen == "Ability")
                {
                    UIManager.instance.OpenMenuOfAbilities();
                }

                yield return new WaitUntil(() => !isWaitingForChoice);

                if (story.canContinue) {
                    dialoguePanel.SetActive(true);
                    string nextText = story.Continue();
                    ParseTags(story.currentTags);
                    dialogueText.text = nextText.Trim();

                    yield return StartCoroutine(TypeLine(nextText.Trim()));

                    if(!string.IsNullOrWhiteSpace(nextText))
                    {
                        lastDisplayedText = dialogueText.text;
                        lastDisplayedName = nameText != null ? nameText.text : "";
                        lastDisplayedIcon = currentIconName;
                    }
                } else {
                    break;
                }
            }
            else
            {
                yield return StartCoroutine(TypeLine(text));

                yield return null;
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }

        }

        dialoguePanel.SetActive(false);
        if (!isWaitingForChoice && GameManager.Instance != null) 
        {
            GameManager.Instance.FreezePlayer(false);
        }
        
        if (GameManager.Instance != null && story.variablesState.Contains("guide_talk_count"))
        {
            GameManager.Instance.guideTalkCount = (int)story.variablesState["guide_talk_count"];

            PlayerPrefs.SetInt("GuideTalkCount", GameManager.Instance.guideTalkCount);
            PlayerPrefs.Save();
        }

        onDialogueFinishedCallback?.Invoke();

        if(willStartRun)
        {
            willStartRun = false;
            MapManager.Instance.StartFirstRandomRoom();
        }
    }

    private IEnumerator TypeLine(string text)
    {
        yield return null; // Esperamos un frame inicial

        if (string.IsNullOrEmpty(text)) text = "...";
        if (dialogueText != null) dialogueText.text = "";
        
        bool isAddingRichTextTag = false;
        bool isSkipping = false;

        foreach(char c in text.ToCharArray())
        {
            if (isSkipping) break;

            if (c == '<' || isAddingRichTextTag) 
            {
                isAddingRichTextTag = true;
                if (dialogueText != null) dialogueText.text += c;
                if (c == '>') isAddingRichTextTag = false;
            }
            else 
            {
                if (dialogueText != null) dialogueText.text += c;

                if (currentVoiceClip != null && audioSource != null && c != ' ') 
                {
                    audioSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
                    audioSource.PlayOneShot(currentVoiceClip);
                }

                // ARREGLO DEL ESPACIO: Esperamos leyendo el teclado cada frame
                float timer = 0f;
                while(timer < typingSpeed)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isSkipping = true;
                        break;
                    }
                    timer += Time.unscaledDeltaTime;
                    yield return null;
                }
            }
        }

        // Si salta, mostramos todo y cortamos el audio
        if (isSkipping)
        {
            if (dialogueText != null) dialogueText.text = text;
            if (audioSource != null) audioSource.Stop();
            yield return null;
        }
    }

    public void ShowSingleLine(string text, string speakerName, string iconName, System.Action onFinish = null)
    {
        if (string.IsNullOrEmpty(text)) 
        {
            text = "...";
        }

        if (GameManager.Instance != null) GameManager.Instance.FreezePlayer(true);
        
        dialoguePanel.SetActive(true);
        if (nameText != null) nameText.text = speakerName;
        if (dialogueText != null) dialogueText.text = text;

        bool iconFound = false;
        foreach (Portrait p in availablePortraits)
        {
            if (p.iconName == iconName)
            {
                portraitImage.sprite = p.iconSprite;
                portraitImage.gameObject.SetActive(true);
                currentVoiceClip = p.voiceSound;
                iconFound = true;
                break;
            }
        }
        if (!iconFound) 
        {
            portraitImage.gameObject.SetActive(false);
            currentVoiceClip = null;
        }
        StartCoroutine(WaitToCloseSingleLine(text, onFinish));
    }

    private IEnumerator WaitToCloseSingleLine(string text, System.Action onFinish)
    {
        yield return StartCoroutine(TypeLine(text));

        yield return null;
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        dialoguePanel.SetActive(false);
        if (GameManager.Instance != null) GameManager.Instance.FreezePlayer(false);
        
        onFinish?.Invoke();
    }

    public void TriggerFantasyBossDeathDialogue(string knotName)
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.FreezePlayer(true);

            int wins = 0;

            if (knotName == "dead_fallenHero") 
            {
                wins = GameManager.Instance.victoriesAgainstBossFantasy;
            }
            else if (knotName == "dead_Judge") 
            {
                wins = GameManager.Instance.victoriesAgainstBossFinal;
            }
            story.variablesState["boss_wins"] = wins;
        }

        onDialogueFinishedCallback = null;
        story.ChoosePathString(knotName);
        dialoguePanel.SetActive(true);
        StartCoroutine(DisplayDialogue());
    }

    private void ParseTags(List<string> tags)
    {
        foreach(string tag in tags)
        {
            if (tag.Trim() == "OPEN_UI")
            {
                menuToOpen = "PowerUp";
                isWaitingForChoice = true;
                continue;
            }

            if (tag.Trim() == "OPEN_ABILITY")
            {
                menuToOpen = "Ability";
                isWaitingForChoice = true;
                continue;
            }

            if(tag.Trim() == "START_RUN")
            {
                willStartRun = true;
                continue;
            }

            if (tag.Trim() == "START_BOSS")
            {
                bossBase boss = FindObjectOfType<bossBase>();
                if (boss != null)
                {
                    boss.SendMessage("StartCombat", SendMessageOptions.DontRequireReceiver);
                }
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
                currentIconName = value;
                    foreach (Portrait p in availablePortraits)
                    {
                        if (p.iconName == value)
                        {
                            portraitImage.sprite = p.iconSprite;
                            portraitImage.gameObject.SetActive(true); 
                            currentVoiceClip = p.voiceSound;
                            break;
                        }
                    }
                    break;
            }
        }
    }

    public void ContinueAfterPowerUpChoice(string powerInkID = null)
    {
        if (story != null && isWaitingForChoice)
        {
            if(powerInkID != null) 
            {
                story.variablesState["poder_elegido"] = powerInkID;
            }
        }

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