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
    public LayerMask hitLayers;

    [Header("VFX")]
    public GameObject impactEffects;

    private LineRenderer lineRenderer;
    private Vector3 targetDirection;
    private bool hasFired = false;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            targetDirection = (player.transform.position - transform.position).normalized;
            
            // Rotamos el orbe para que mire al jugador (estético)
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

        lineRenderer.enabled = true;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, pointToCreateLaser.position);

        // USAR transform.right (o .up dependiendo de hacia dónde mire tu sprite)
        // Esto disparará en la dirección real a la que apunta el orbe
        RaycastHit2D hit = Physics2D.Raycast(pointToCreateLaser.position, transform.right, maxDistance, hitLayers);

        if(hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point);
            if(hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<GhostHealth>()?.TakeDamage(damage);
            }
            if(impactEffects != null)
            {
                Instantiate(impactEffects, hit.point, Quaternion.identity);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, pointToCreateLaser.position + (transform.right * maxDistance));
        }
    }
}
