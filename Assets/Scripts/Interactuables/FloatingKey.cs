using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingKey : MonoBehaviour
{
    public float amplitude = 0.1f;
    public float speed = 2f;

    Vector3 initialPos;

    void Awake() => initialPos = transform.localPosition;

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = initialPos + new Vector3(0, newY, 0);
    }
}
