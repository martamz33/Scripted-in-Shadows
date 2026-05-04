using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomIdle : MonoBehaviour
{
    private Animator animator;

    [Header("Time Animations")]
    public float minTimeAnimation = 12f;
    public float maxTimeAnimation = 22f;

    private float timer;
    private float objectiveTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        calculateNewTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= objectiveTime)
        {
            animator.SetTrigger("quitPipe");

            calculateNewTime();
        }
    }

    private void calculateNewTime()
    {
        timer = 0f;

        objectiveTime = Random.Range(minTimeAnimation, maxTimeAnimation);
    }
}
