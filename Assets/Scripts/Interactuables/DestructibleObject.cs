using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DestructibleObject : MonoBehaviour
{
    public enum TypeOfTreasure { Ink, Life}

    [Header("Aparition settings")]
    [Tooltip("Force when they jump when it appear")]
    public float forceJumpY = 5f;
    [Tooltip("Little lateral random force to not fall straight")]
    public float lateralRandomForce = 1f;

    [Header("Break Settings")]
    public int maxBlows = 3;
    private int actualBlows;
    public GameObject vfxBreak;

    [Header("Sprites")]
    public Sprite intact;
    public Sprite withOneBlow;
    public Sprite withTowBlows;

    [Header("Treasure")]
    public TypeOfTreasure typeOfTreasure;
    public int quantity = 1;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        actualBlows = maxBlows;
        if(intact != null) spriteRenderer.sprite = intact;

        float empujeX = Random.Range(-lateralRandomForce, lateralRandomForce);
        Vector2 initialForce = new Vector2(empujeX, forceJumpY);

        rb.AddForce(initialForce, ForceMode2D.Impulse);
    }

    public void RecibeBlow()
    {
        actualBlows --;
        ActualiseAspect();

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);

        if(actualBlows == 0)
        {
            Break();
        }
    }

    private void ActualiseAspect()
    {
        if(actualBlows == 2 && withOneBlow != null)
        {
            spriteRenderer.sprite = withOneBlow;
        }
        if(actualBlows == 1 && withTowBlows != null)
        {
            spriteRenderer.sprite = withTowBlows;
        }
    }

    private void Break()
    {
        DeliverTreasure();

        GameObject vfx = Instantiate(vfxBreak, transform.position, Quaternion.identity);
        Destroy(vfx);

        Destroy(gameObject);
    }

    private void DeliverTreasure()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        if(typeOfTreasure == TypeOfTreasure.Ink)
        {
            if(InkManager.instance !=null) InkManager.instance.AddInk(quantity);
            Debug.Log("tinta");
        }
        if(typeOfTreasure == TypeOfTreasure.Life)
        {
            GhostHealth healthPlayer = player.GetComponent<GhostHealth>();
            if(healthPlayer != null) healthPlayer.Heal(quantity);
            Debug.Log("vida");
        }
    }
}
