using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveElement : MonoBehaviour
{
    [Header("Settings")]
    public float timeBetweenExplosions = 3f;
    public float warningDuration = 1f; // Tiempo que parpadea antes de explotar

    [Header("References")]
    public SpriteRenderer spriteRenderer; // Arrastra aquí el Sprite de la mina/bomba
    public GameObject explosionVFX; // Prefab de la explosión con el collider

    void Start()
    {
        StartCoroutine(ExplosionRoutine());
    }

    IEnumerator ExplosionRoutine()
    {
        while (true)
        {
            // 1. Espera el tiempo de espera normal
            yield return new WaitForSeconds(timeBetweenExplosions - warningDuration);

            // 2. Efecto de aviso (Parpadeo)
            yield return StartCoroutine(WarningFlash());

            // 3. ¡Explosión!
            if (explosionVFX != null)
            {
                Instantiate(explosionVFX, transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator WarningFlash()
    {
        float timer = 0f;
        bool toggle = false;

        while (timer < warningDuration)
        {
            spriteRenderer.color = toggle ? Color.red : Color.white;
            toggle = !toggle;
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.color = Color.white; // Restaurar color
    }
}