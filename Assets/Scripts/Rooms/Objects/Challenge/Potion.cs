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

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
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

            //1.Stop the physics movement
            rg.velocity = Vector2.zero;
            rg.isKinematic = true;

            //2.Enable false the element
            if(sprite!=null) sprite.enabled = false;

            //3.Effect and damage
            if(ps!=null) ps.Play();
            if(damageArea!=null) damageArea.enabled =true;

            //4.Cleaning,  destroy the element
            Destroy(gameObject, 2f);
        }
    }
}
