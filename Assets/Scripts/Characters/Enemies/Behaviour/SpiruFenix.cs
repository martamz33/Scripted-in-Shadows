using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiruFenix : EnemyBase
{    
    [Header("Procedural Movement")]
    public float amplitude = 0.5f;
    public float velocity = 2f;
    private Vector3 initialPosition;

    [Header("Attack")]
    public GameObject prefabMagic;
    public Transform pointToCreateMagic;
    public float timeBetweenAttacks = 3f;
    private float cronoAttack;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip createSound;

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    protected override void Update()
    {
        base.Update();
        proceduralMove();
    }

    private void proceduralMove()
    {
        if(currentState == EnemyState.Death) return;

        float newY = initialPosition.y + Mathf.Sin(Time.time * velocity) *amplitude;
        transform.position = new Vector3 (initialPosition.x, newY, transform.position.z);
    }

    protected override void OnIdle(float distance)
    {
        if(distance < distanceDetection)
        {
            currentState = EnemyState.Attack;
        }
    }

    protected override void OnAttack(float distance)
    {
        if(distance > distanceDetection)
        {
            currentState = EnemyState.Idle;
            return;
        }

        cronoAttack += Time.deltaTime;

        if(cronoAttack >= timeBetweenAttacks)
        {
            LaunchMagic();
            cronoAttack = 0;
        }
    }

    private void LaunchMagic()
    {
        if(audioSource != null && createSound != null)
        {
            audioSource.pitch = 1f + Random.Range(-0.1f, 0.1f);
            audioSource.PlayOneShot(createSound);
        }
        
        if(prefabMagic != null && pointToCreateMagic !=null)
        {
            if(animator!=null) animator.SetTrigger("Attack");
            GameObject magic = Instantiate(prefabMagic, pointToCreateMagic.position, Quaternion.identity);

            SpiruAttack spiru = magic.GetComponent<SpiruAttack>();
            if(spiru != null)
            {
                spiru.damage = this.damage;
            }
        }
    }
}
