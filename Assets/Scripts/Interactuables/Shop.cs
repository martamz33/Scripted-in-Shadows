using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class Shop : Interactuable
{
    [Header("Dialogue Settings")]
    public TextAsset inkJSONAsset;

    private bool isDialogueActive = false;

    protected override void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        if (isDialogueActive) return;

        if (hasInteracted)
        {
            isDialogueActive = true;
            DialogueManager.instance.ShowSingleLine(savedText, savedName, savedIcon, () => {
                isDialogueActive = false;
                OpenShopUI();
            });
            return;
        }

        isDialogueActive = true;
        
        DialogueManager.instance.StartExternalDialogue(inkJSONAsset, OnDialogueFinished);
    }

    public void OnDialogueFinished()
    {
        SaveLastDialogue();
        isDialogueActive = false;

        GameManager.Instance.FreezePlayer(false);

        OpenShopUI();
    }

    private void OpenShopUI()
    {
        ShopManager.Instance.shopCanvas.SetActive(true);
        if (ShopManager.Instance != null)
        {
            ShopManager.Instance.GenerateShopItems();
        }
    }
    
}
