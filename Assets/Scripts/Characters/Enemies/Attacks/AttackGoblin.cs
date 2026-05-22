using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGoblin : MonoBehaviour
{
    private Goblin goblin;
    
    void Start()
    {
        goblin = GetComponentInParent<Goblin>();

        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Algo entró en el trigger del arma: " + other.name);
        
        if(other.gameObject.CompareTag("Player") && goblin.IsAttacking)
        {
            other.gameObject.GetComponent<GhostHealth>().TakeDamage(goblin.damage);
            GetComponent<Collider2D>().enabled = false;
        }    
    }
}
