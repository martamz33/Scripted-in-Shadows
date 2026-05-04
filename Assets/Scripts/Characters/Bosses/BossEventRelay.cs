using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventRelay : MonoBehaviour
{
    private Judge scriptPadre;

    void Start()
    {
        scriptPadre = GetComponentInParent<Judge>();
    }

    // --- FUNCTIONS THHAT WILL CALL THE ANIMATIONS ---

    public void Llamar_MartillosCielo()
    {
        if (scriptPadre != null) scriptPadre.Event_InstantiateHammerSky();
    }

    public void Llamar_Ondas()
    {
        if (scriptPadre != null) scriptPadre.Event_InstantiateWavesHammer();
    }

    public void Llamar_LanzarPequeno()
    {
        if (scriptPadre != null) scriptPadre.Event_InstantiateThoughHammerL();
    }

    public void Llamar_LanzarGrande()
    {
        if (scriptPadre != null) scriptPadre.Event_InstantiateThoughHammerB();
    }

    public void Call_InvokeBigBalance()
    {
        if (scriptPadre != null) scriptPadre.Event_InvokeBigBalances();
    }

    public void Call_Event_ThroughBalance(string enumNameString)
    {
        if (scriptPadre != null) scriptPadre.Event_ThoughBalance(enumNameString);
    }

    public void Call_EnergyWave(string enumNameString)
    {
        if (scriptPadre != null) scriptPadre.Event_EnergyWave(enumNameString);
    }

}
