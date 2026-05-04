using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            GameManager.Instance.OptionsOfDEstiny();

            col.GetComponent<GhostMovement>().enabled = false;
            col.GetComponent<GhostMovement>().isMovementActive = false;
        }
    }
}
