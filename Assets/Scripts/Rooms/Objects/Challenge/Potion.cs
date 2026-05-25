using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer sprite;
    public ParticleSystem ps;
    public Collider2D damageArea;

    [Header("Swing Settings")] //balanceo
    public float amplitude = 20f;
    public float swingSpeed = 5f;

    private bool isOnTheFloor;
    private Rigidbody2D rg;
    private Collider2D mainCollider; // NUEVO: Para guardar el collider físico

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>(); // Buscamos el collider sólido de la poción
    }

    void Update()
    {
        if(!isOnTheFloor)
        {
            float rotationZ = Mathf.Sin(Time.time * swingSpeed) * amplitude;
            transform.localRotation = Quaternion.Euler(0, 0, rotationZ);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground") && !isOnTheFloor)
        {
            isOnTheFloor = true;

            // 1. Detener físicas
            rg.velocity = Vector2.zero;
            rg.isKinematic = true;
            transform.rotation = Quaternion.identity; 

            // 2. Apagar la imagen y el collider físico
            if(sprite != null) sprite.enabled = false;
            if(mainCollider != null) mainCollider.enabled = false;

            // 3. Activar VFX y Daño
            if(ps != null) ps.Play();
            if(damageArea != null) damageArea.enabled = true;

            // 4. CAMBIO AQUÍ: Desactivamos el objeto en lugar de destruirlo de golpe
            // O, si quieres destruir, espera a que la partícula termine:
           float destroyTime = (ps != null) ? ps.main.duration : 2f;
            
            // 2. Destruimos el objeto de la poción inmediatamente
            Destroy(gameObject, destroyTime);
        }
    }
}