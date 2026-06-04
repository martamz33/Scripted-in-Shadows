using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactuable : MonoBehaviour
{
    public GameObject iconInteractuable;
    protected bool playerIsNear = false;

    protected bool hasInteracted = false;
    protected string savedText = "";
    protected string savedName = "";
    protected string savedIcon = "";

    protected virtual void Update()
    {
        if(playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("HA entrado");
            playerIsNear = true;
            if(iconInteractuable != null) iconInteractuable.SetActive(true);
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
            if(iconInteractuable != null) iconInteractuable.SetActive(false);
        }
    }

    public virtual void SaveLastDialogue()
    {
        hasInteracted = true;
        savedText = DialogueManager.instance.lastDisplayedText;
        savedName = DialogueManager.instance.lastDisplayedName;
        savedIcon = DialogueManager.instance.lastDisplayedIcon;
    }
}
