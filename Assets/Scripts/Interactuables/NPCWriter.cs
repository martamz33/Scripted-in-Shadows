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
    
    public override void Interact()
    {
        UIManager.instance.myActualPowerSelection = misPowerUps;

        DialogueManager.instance.TriggerDialogue("conversacion_principal");
    }
}
