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

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(PlantBeahviour());
    }

    IEnumerator PlantBeahviour()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            animator.SetBool("isBig", true);

            yield return new WaitForSeconds(Random.Range(minTimeBig, maxTimeBig));

            animator.SetBool("isBig", false);
        }
    }
}
