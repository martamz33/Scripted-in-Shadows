using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    [Header("Reference To Manager")]
    [Tooltip("Arrastra aquí el objeto que tiene el ArenaManager")]
    public ArenaManager arenaManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (arenaManager != null)
            {
                arenaManager.StartArena();
            }
            gameObject.SetActive(false);
        }
    }
}
