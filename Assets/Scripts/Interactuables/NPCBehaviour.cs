using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : Interactuable
{
    [Header("Dialog Setting")]
    public TextAsset inkJSONAsset;

    private bool isDialogueActive = false;
    protected override void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        if (isDialogueActive) return;

        isDialogueActive = true;
        
        DialogueManager.instance.StartExternalDialogue(inkJSONAsset, OnDialogueFinished);
    }

    public void OnDialogueFinished()
    {
        isDialogueActive = false;

        GameManager.Instance.FreezePlayer(false);
    }
}
