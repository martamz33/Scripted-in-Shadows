using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPowerUp : MonoBehaviour
{
    [Header("Arrastra aquí el poder que quieras probar")]
    public PowerUpScriptable poderAProbar;

    void Update()
    {
        // Al pulsar la tecla T (de Test), aplicamos el poder a este objeto
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (poderAProbar != null)
            {
                poderAProbar.ApplyEffect(this.gameObject);
                Debug.Log("¡Poder de prueba aplicado!");
            }
            else
            {
                Debug.LogWarning("Oye, ¡no has asignado ningún poder en el Inspector!");
            }
        }
    }
}
