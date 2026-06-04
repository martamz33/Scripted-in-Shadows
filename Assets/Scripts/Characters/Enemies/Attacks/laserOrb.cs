using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserOrb : MonoBehaviour
{
    [Header("Laser Settings")]
    public Transform pointToCreateLaser;
    public int damage = 3;
    public float maxDistance = 8f;
    public float prepareTime = 0.5f;
    public float laserDuration = 0.3f;
    public float lineWidth = 0.05f;
    
    [Tooltip("Capas que detienen el láser (incluye la capa del jugador y del suelo)")]
    public LayerMask hitLayers;
    [Tooltip("Capa específica del suelo para detener el láser visualmente")]
    public LayerMask groundLayer;

    [Header("VFX & Audio")] // Actualizado el header
    public GameObject impactEffects;
    public AudioClip laserSound;

    private LineRenderer lineRenderer;
    private bool hasFired = false;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Apuntar al jugador inicialmente
            Vector3 targetDirection = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        StartCoroutine(FireLaserSequence());
    }

    IEnumerator FireLaserSequence()
    {
        yield return new WaitForSeconds(prepareTime);

        FireRayCast();

        yield return new WaitForSeconds(laserDuration);

        // Limpieza tras el disparo
        if(impactEffects != null)
        {
            GameObject dead = Instantiate(impactEffects, transform.position, Quaternion.identity);
            Destroy(dead, 1.5f);
        }
        
        Destroy(gameObject);
    }

    private void FireRayCast()
    {
        if(hasFired) return;
        hasFired = true;

        if (laserSound != null)
        {
            AudioSource.PlayClipAtPoint(laserSound, transform.position);
        }

        lineRenderer.enabled = true;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, pointToCreateLaser.position);

        // Lanzamos un rayo que atraviesa todos los colliders en el camino
        RaycastHit2D[] hits = Physics2D.RaycastAll(pointToCreateLaser.position, transform.right, maxDistance, hitLayers);

        bool hitSomething = false;
        Vector3 endPoint = pointToCreateLaser.position + (transform.right * maxDistance);

        foreach (RaycastHit2D hit in hits)
        {
            // 1. Intentar encontrar al jugador (buscando el componente en el objeto o sus padres)
            GhostHealth health = hit.collider.GetComponentInParent<GhostHealth>();
            
            if (health != null && hit.collider.CompareTag("Player"))
            {
                health.TakeDamage(damage);
                endPoint = hit.point;
                hitSomething = true;
                break; // Jugador golpeado, dejamos de atravesar
            }
            
            // 2. Si golpeamos suelo/pared, detener el láser visualmente
            if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
            {
                endPoint = hit.point;
                hitSomething = true;
                break;
            }
        }

        // Finalizar el dibujado
        lineRenderer.SetPosition(1, endPoint);

        if(hitSomething && impactEffects != null)
        {
            Instantiate(impactEffects, endPoint, Quaternion.identity);
        }
    }
}