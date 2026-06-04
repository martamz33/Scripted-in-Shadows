using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 100;
    public float lifeTime = 1.2f;
    public float speed = 5f;

    [Header("Anchor")]
    public  LayerMask groundLayer;
    public float rayDistance = 5f;
    public float yOffset = 0f;

    [Header("Audio Settings")]
    [Tooltip("Sonido continuo del agua arrasando")]
    public AudioClip tsunamiSound;
    [Tooltip("Sonido al impactar contra un enemigo (Opcional)")]
    public AudioClip hitSound; 
    
    private AudioSource audioSource;

    private float fixedY;
    private bool heightSet = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0f; // Aseguramos que sea 2D para que se escuche bien
        }

        if (tsunamiSound != null)
        {
            audioSource.clip = tsunamiSound;
            audioSource.loop = true; // Lo ponemos en bucle por si el audio es corto
            audioSource.Play();
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.down, rayDistance, groundLayer);

        if (hit.collider != null)
        {
            fixedY = hit.point.y + yOffset;
            heightSet = true;
            transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
        }

        Destroy(gameObject, lifeTime);
    }
    
    void Update()
    {
        float direction = transform.localScale.x > 0 ? 1 : -1;
        float newX = transform.position.x + (direction * speed * Time.deltaTime);

        if(heightSet)
        {
            transform.position = new Vector3(newX, fixedY, transform.position.z);
        }
        else
        {
            transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        }        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);

                if (hitSound != null)
                {
                    // Usamos PlayClipAtPoint para que el golpe suene entero aunque la ola siga adelante
                    AudioSource.PlayClipAtPoint(hitSound, transform.position);
                }
            }
        }
    }
}
