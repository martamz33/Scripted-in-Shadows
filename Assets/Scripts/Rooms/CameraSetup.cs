using UnityEngine;
using Cinemachine;

public class CameraSetup : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private CinemachineConfiner2D confiner;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner2D>();

        AssignPlayer();
        AssignCameraLimits();
    }

    private void AssignPlayer()
    {
        // Busca al jugador por su Tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            vcam.Follow = player.transform;
        }
        else
        {
            Debug.LogWarning("La cámara no encontró al Player en esta escena.");
        }
    }

    private void AssignCameraLimits()
    {
        if (confiner != null)
        {
            // Busca el objeto que tenga los límites (asegúrate de ponerle este Tag a tu collider de límites)
            GameObject limits = GameObject.FindGameObjectWithTag("CameraLimits"); 
            
            if (limits != null)
            {
                confiner.m_BoundingShape2D = limits.GetComponent<Collider2D>();
                
                // Le decimos a Cinemachine que recalcule los límites con la función actualizada
                confiner.InvalidateCache(); 
            }
            else
            {
                // Si la escena no tiene límites, apagamos el confiner para que no de error NaN
                confiner.enabled = false; 
            }
        }
    }
}