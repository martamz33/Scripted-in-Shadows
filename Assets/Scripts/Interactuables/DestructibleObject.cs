using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DestructibleObject : MonoBehaviour
{
    public enum TypeOfTreasure { Ink, Life}

    [Header("Aparition settings")]
    [Tooltip("Force when they jump when it appear")]
    public float forceJumpY = 5f;
    [Tooltip("Little lateral random force to not fall straight")]
    public float lateralRandomForce = 1f;
    public GameObject vfxSpawn;

    [Header("Break Settings")]
    public int maxBlows = 3;
    private int actualBlows;
    public GameObject vfxBreak;
    [SerializeField] private bool isGrounded = false;

    [Header("Sprites")]
    public SpriteRenderer crackOverlay;
    public Sprite withOneBlow;
    public Sprite withTowBlows;
    public Sprite withThreeBlows;

    [Header("Treasure")]
    public TypeOfTreasure typeOfTreasure;
    [Tooltip("Prefab del objeto físico de TINTA que caerá al suelo")]
    public GameObject inkPickupPrefab; 
    [Tooltip("Prefab del objeto físico de VIDA que caerá al suelo")]
    public GameObject lifePickupPrefab;

    [Header("Audio Settings")]
    [Tooltip("Sonido al recibir un golpe")]
    public AudioClip hitSound;
    [Tooltip("Sonido al destruirse (opcional)")]
    public AudioClip breakSound;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        actualBlows = maxBlows;
        if (crackOverlay != null) crackOverlay.sprite = null;

        if (vfxSpawn != null)
        {
            GameObject spawnEffect = Instantiate(vfxSpawn, transform.position, Quaternion.identity);
            Destroy(spawnEffect, 2f); 
        }

        float empujeX = Random.Range(-lateralRandomForce, lateralRandomForce);
        Vector2 initialForce = new Vector2(empujeX, forceJumpY);

        rb.AddForce(initialForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void RecibeBlow()
    {
        actualBlows --;
        ActualiseAspect();

        if (hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);

        if(actualBlows <= 0)
        {
            Break();
        }
    }

    private void ActualiseAspect()
    {
        if (crackOverlay != null)
        {
            if (actualBlows == 3 && withOneBlow != null)
            {
                crackOverlay.sprite = withOneBlow;
            }
            else if (actualBlows == 2 && withTowBlows != null)
            {
                crackOverlay.sprite = withTowBlows;
            }

            else if (actualBlows == 1 && withThreeBlows != null)
            {
                crackOverlay.sprite = withThreeBlows;
            }
        }
    }

    private void Break()
    {
        DeliverTreasure();

        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }

        if (vfxBreak != null)
        {
            GameObject breakEffect = Instantiate(vfxBreak, transform.position, Quaternion.identity);
            Destroy(breakEffect, 2f); 
        }

        Destroy(gameObject);
    }

    private void DeliverTreasure()
    {
        GameObject prefabToSpawn = null;

        if(typeOfTreasure == TypeOfTreasure.Ink)
        {
            prefabToSpawn = inkPickupPrefab;
        }
        if(typeOfTreasure == TypeOfTreasure.Life)
        {
            prefabToSpawn = lifePickupPrefab;
        }

        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
    }
}
