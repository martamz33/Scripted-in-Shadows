using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class NPCWriter : Interactuable
{
    [Header("Configuración del Escritor")]
    public PowerUpScriptable[] misPowerUps;

    [Header("Dialogue Settings")]
    public TextAsset inkJSONAsset;

    private bool isDialogueActive = false;
    
    public override void Interact()
    {
        if (isDialogueActive) return;

        // NUEVO: Repetimos solo la frase final, no volvemos a mostrar la UI de poderes
        if (hasInteracted)
        {
            isDialogueActive = true;
            DialogueManager.instance.ShowSingleLine(savedText, savedName, savedIcon, () => {
                isDialogueActive = false;
            });
            return;
        }

        isDialogueActive = true;
        UIManager.instance.writerPowerSelection = misPowerUps;

        // Llamamos al trigger y pasamos el evento al terminar para guardar
        DialogueManager.instance.TriggerDialogue("conversacion_principal", () => {
            SaveLastDialogue();
            isDialogueActive = false;
            GameManager.Instance.FreezePlayer(false);
        });
    }
}
