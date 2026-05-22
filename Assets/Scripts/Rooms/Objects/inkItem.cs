using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inkItem : MonoBehaviour
{
    public int inkValue = 2;
    private Rigidbody2D rg;
    private Collider2D col;
    private bool isCollected = false; // Evita que se procese dos veces

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Usamos Collision para el suelo (físico)
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            rg.isKinematic = true; // Se queda quieto
            col.isTrigger = true;  // Ahora es atravesable para recogerlo
        }
    }

    // Usamos Trigger para el jugador
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            InkManager.instance.AddInk(inkValue);
            Destroy(gameObject);
        }
    }
}