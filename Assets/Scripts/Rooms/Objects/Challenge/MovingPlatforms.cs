using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [Header("Points of movement")]
    public Transform iniPoint;
    public Transform finalPoint;
    public float speed = 3f;

    [Header("Player")]
    public GhostMovement gm;

    [Header("System Particles")]
    public ParticleSystem effectOfMovement;

    private Rigidbody2D rg;
    private Vector3 nextPosition;
    private Vector3 IniPoint;
    private Vector3 FinalPoint;
    private Vector2 platformVelocity;
    private Vector3 lastPosition;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.bodyType = RigidbodyType2D.Kinematic;

        IniPoint = iniPoint.position;
        FinalPoint = finalPoint.position;

        transform.position = IniPoint;
        lastPosition = transform.position;
        nextPosition = FinalPoint;

        if(effectOfMovement!=null && !effectOfMovement.isPlaying)
            effectOfMovement.Play();
    }

    void FixedUpdate()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.fixedDeltaTime);
        rg.MovePosition(newPos);

        //calculate the real velocity
        platformVelocity = (newPos - lastPosition) / Time.fixedDeltaTime;
        lastPosition = newPos;

        UpdateParticlesDirection();

        if(Vector3.Distance(transform.position, nextPosition) <0.1f)
        {
            nextPosition = (nextPosition == IniPoint) ? FinalPoint : IniPoint;
        }
    }

    private void UpdateParticlesDirection()
    {
        if(effectOfMovement == null) return;

        Vector3 direction = (nextPosition - transform.position).normalized;

        effectOfMovement.transform.rotation = Quaternion.LookRotation(-direction);
    }

    public Vector2 GetVelocity() => platformVelocity;
}
