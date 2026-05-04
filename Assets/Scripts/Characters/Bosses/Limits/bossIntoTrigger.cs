using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class bossIntoTrigger : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("The exact point where the player needs to stop")]
    public Transform pointPlayer;
    public float velocityMovement = 5f;

    [Header("Arena Events")]
    [Tooltip("Actions that occur when the player touch the trigger")]
    public UnityEvent toTouchTheTrigger;
    public UnityEvent toFinishDialogue;

    private bool initialSequence = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !initialSequence)
        {
            initialSequence = true;
            StartCoroutine(CinematicSequence(other.transform));
        }
    }

    private IEnumerator CinematicSequence(Transform playerTransform)
    {
        toTouchTheTrigger.Invoke();

        GameManager.Instance.FreezePlayer(true);

        while (Vector2.Distance(playerTransform.position, pointPlayer.position) > 0.05f)
        {
            playerTransform.position = Vector2.MoveTowards(
                playerTransform.position, 
                pointPlayer.position, 
                velocityMovement * Time.deltaTime
            );
            yield return null; 
        }

        playerTransform.position = pointPlayer.position;

        yield return new WaitForSeconds(3f);

        GameManager.Instance.FreezePlayer(false);

        toFinishDialogue.Invoke();

        gameObject.SetActive(false);
    }
}
