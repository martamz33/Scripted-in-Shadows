using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform cam;
    public float parallaxFactor;

    private float length, startPos;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // how far we moved since the begginig
        float dist = (cam.position.x * parallaxFactor);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        
        //  how far we moved relatively
        float distanceToCam = cam.position.x - transform.position.x;

        // if the camera goes beyond the width of the image, we move the startPos
        if (distanceToCam > length * 1.5f) 
        {
            startPos += length * 3;
        }
        else if (distanceToCam < -length * 1.5f) 
        {
            startPos -= length * 3;
        }
    }
}
