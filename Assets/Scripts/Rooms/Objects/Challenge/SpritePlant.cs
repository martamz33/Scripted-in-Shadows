using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePlant : MonoBehaviour
{
    [Header("Time Settings")]
    public float minTime = 2f;
    public float maxTime = 5f;
    public float minTimeBig = 3f;
    public float maxTimeBig = 7f;

    [Header("Movement Settings")]
    [Tooltip("Distancia que subirá la planta en el eje Y")]
    public float upwardDistance = 1f;
    [Tooltip("Tiempo que tarda en subir/bajar. Ajústalo para que coincida con tu animación")]
    public float moveDuration = 0.5f;

    private Animator animator;
    private Vector3 initialPosition;
    private Coroutine currentMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        // Guardamos la posición exacta donde colocaste la planta en el nivel
        initialPosition = transform.localPosition; 
        
        StartCoroutine(PlantBeahviour());
    }

    IEnumerator PlantBeahviour()
    {
        while(true)
        {
            // Tiempo que pasa siendo pequeña
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            // ¡CRECE!
            animator.SetBool("isBig", true);
            
            // Detenemos cualquier movimiento anterior por seguridad y empezamos a subir
            if (currentMovement != null) StopCoroutine(currentMovement);
            Vector3 targetUp = initialPosition + new Vector3(0, upwardDistance, 0);
            currentMovement = StartCoroutine(MovePlant(targetUp));

            // Tiempo que pasa siendo grande
            yield return new WaitForSeconds(Random.Range(minTimeBig, maxTimeBig));

            // ¡DECRECE!
            animator.SetBool("isBig", false);
            
            // Empezamos a bajar a la posición original
            if (currentMovement != null) StopCoroutine(currentMovement);
            currentMovement = StartCoroutine(MovePlant(initialPosition));
        }
    }

    // Corrutina encargada exclusivamente de mover el objeto suavemente
    IEnumerator MovePlant(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;

            // Movemos la planta progresivamente
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, percent);
            
            yield return null; // Esperamos al siguiente frame
        }

        // Al terminar, la fijamos exactamente en la posición final para evitar errores milimétricos
        transform.localPosition = targetPosition;
    }
}