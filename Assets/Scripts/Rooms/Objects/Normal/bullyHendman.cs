using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullyHendman : MonoBehaviour
{
    [Header("Configuration of Movement")]
    [Tooltip("Velocidad a la que persigue al jugador")]
    public float movementVelocity = 2f;
    [Tooltip("Distancia mínima a la que se queda del jugador para no ponerse encima")]
    public float minimunDistance = 3f;

    [Header("Configuration of Point")]
    public GameObject balaPrefab;
}
