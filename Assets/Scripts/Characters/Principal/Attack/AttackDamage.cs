using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    [Header("Movement Settings")]
    public MovementType currentMovement;
    public float arcHeight = 1.5f;
    
    private Vector3 targetLocalPos;
    private int currentDamage;
    private float timer = 0f;
    private float lifeTime;
    private Vector3 startLocalPos;

    public void SetUp(int amount, Transform finalPoint)
    {
        currentDamage = amount;
        startLocalPos = transform.localPosition;
        targetLocalPos = finalPoint.localPosition;

        AutoDestroyEffect destroy = GetComponent<AutoDestroyEffect>();
        if(destroy!=null)
        {
            lifeTime = destroy.delay;
        }
    }

    void Update()
    {
        if(currentMovement == MovementType.Static) return;

        timer += Time.deltaTime;

        float percent = timer / lifeTime;

        percent = Mathf.Clamp01(percent);

        switch(currentMovement)
        {
            case MovementType.Linear:
                transform.localPosition = Vector3.Lerp(startLocalPos, targetLocalPos, percent);
                break;
            case MovementType.Parabolic:
                MoveParabolic(percent);
                break;
        }
    }

    private void MoveParabolic(float percent)
    {
        Vector3 basePosition = Vector3.Lerp(startLocalPos, targetLocalPos, percent);

        float y = Mathf.Sin(percent * Mathf.PI) * arcHeight;

        transform.localPosition = basePosition + new Vector3(0, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if(damagable != null)
                damagable.TakeDamage(currentDamage);
        }

        if(other.gameObject.CompareTag("DestructibleObject"))
        {
            DestructibleObject objectDestructible = other.gameObject.GetComponent<DestructibleObject>();
            if(objectDestructible != null)
                objectDestructible.RecibeBlow();
        }
    }
}
