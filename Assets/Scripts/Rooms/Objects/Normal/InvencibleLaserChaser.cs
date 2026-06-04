using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvencibleLaserChaser : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2.5f;

    [Header("Laser Settings")]
    public Transform pointToCreateLaser;
    public int damage = 1; 
    public float maxDistance = 15f; 
    
    [Tooltip("Tiempo de espera entre un disparo y el siguiente")]
    public float attackCooldown = 3f; 
    public float prepareTime = 0.5f;
    public float laserDuration = 0.3f;
    public float lineWidth = 0.05f;
    
    [Tooltip("Capas que detienen el láser (incluye la capa del jugador y del suelo)")]
    public LayerMask hitLayers;
    [Tooltip("Capa específica del suelo para detener el láser visualmente")]
    public LayerMask groundLayer;
    
    [Header("VFX & Audio")] 
    public GameObject impactEffects;
    public AudioClip laserSound;

    private LineRenderer lineRenderer;
    private Transform player;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        // If the player does not exists and if is dead, don't do anything
        if (player == null) return;

        // 1. Look always at the player
        Vector3 targetDirection = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 2. If is  not attacking, chase it
        if (!isAttacking)
        {
            // Go to the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Add time to the next attack
            attackTimer += Time.deltaTime;
            
            if (attackTimer >= attackCooldown)
            {
                StartCoroutine(FireLaserSequence());
            }
        }
    }

    IEnumerator FireLaserSequence()
    {
        // Detein the movement to attack
        isAttacking = true;
        attackTimer = 0f; 

        // Stop to attack
        yield return new WaitForSeconds(prepareTime);

        // Fire Laser
        FireRayCast();

        // Mantain the laser visual 
        yield return new WaitForSeconds(laserDuration);

        // Return to persecution and close the laser.
        lineRenderer.enabled = false;
        isAttacking = false; 
    }

    private void FireRayCast()
    {
        if (laserSound != null)
        {
            AudioSource.PlayClipAtPoint(laserSound, transform.position);
        }

        lineRenderer.enabled = true;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, pointToCreateLaser.position);

        RaycastHit2D[] hits = Physics2D.RaycastAll(pointToCreateLaser.position, transform.right, maxDistance, hitLayers);

        bool hitSomething = false;
        Vector3 endPoint = pointToCreateLaser.position + (transform.right * maxDistance);

        foreach (RaycastHit2D hit in hits)
        {

            GhostHealth health = hit.collider.GetComponentInParent<GhostHealth>();
            
            if (health != null && hit.collider.CompareTag("Player"))
            {
                health.TakeDamage(damage); 
                endPoint = hit.point;
                hitSomething = true;
                break; 
            }
            
            if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
            {
                endPoint = hit.point;
                hitSomething = true;
                break;
            }
        }

        lineRenderer.SetPosition(1, endPoint);

        if(hitSomething && impactEffects != null)
        {
            Instantiate(impactEffects, endPoint, Quaternion.identity);
        }
    }
}