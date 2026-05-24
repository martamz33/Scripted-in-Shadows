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
            StartCoroutine(CinematicSequence(other.gameObject));
        }
    }

    private IEnumerator CinematicSequence(GameObject playerObject)
    {
        Transform playerTransform = playerObject.transform;
        toTouchTheTrigger.Invoke();

        GameManager.Instance.FreezePlayer(true);

        Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
        if(rb != null) rb.velocity = Vector2.zero;

        GhostAttack attackScript = playerObject.GetComponent<GhostAttack>();
        if(attackScript != null) attackScript.CancelAllAttacks();

        Animator animator = playerObject.GetComponent<Animator>();
        if(animator != null)
        {
            animator.SetFloat("Speed", 0f);
            
            animator.Play("Idle"); 
        }

        while (Vector2.Distance(playerTransform.position, pointPlayer.position) > 0.05f)
        {
            playerTransform.position = Vector2.MoveTowards(
                playerTransform.position, 
                pointPlayer.position, 
                velocityMovement * Time.deltaTime
            );

            if(animator != null) animator.SetFloat("Speed", 1f);
            yield return null; 
        }

        playerTransform.position = pointPlayer.position;
        if(animator != null) animator.SetFloat("Speed", 0f);

        yield return new WaitForSeconds(3f);

        GameManager.Instance.FreezePlayer(false);

        toFinishDialogue.Invoke();

        gameObject.SetActive(false);
    }
}
