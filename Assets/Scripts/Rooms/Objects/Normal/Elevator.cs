using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float elevatorSpeed = 4.5f;
    private bool isUsingElevator = false;
    private float dir = 0f;
    private bool elevatorStarted = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;

        Animator animator = other.gameObject.GetComponent<Animator>();
        GhostMovement gm = other.gameObject.GetComponent<GhostMovement>();
        Rigidbody2D rg = other.gameObject.GetComponent<Rigidbody2D>();

        if(animator == null || gm == null || rg == null) return;

        float yInput = Input.GetAxisRaw("Vertical");
        bool pressingDirection = Mathf.Abs(yInput) > 0.1f;

        if(pressingDirection)
        {
            float newDir = yInput > 0 ? 1f : -1f;

            if(!isUsingElevator)
            {
                isUsingElevator = true;
                elevatorStarted = false;
                dir = newDir;
                gm.isMovementActive = false;
                gm.isOscilationActive = false;

                animator.SetFloat("Speed", 0f);
                animator.SetBool("isGrounded", false);
                animator.SetInteger("ElevatorDir", dir > 0 ? 1 : 2);
                animator.SetBool("onElevator", true);
                animator.ResetTrigger("StartElevator");
                animator.SetTrigger("StartElevator");
            }
            else if(newDir != dir)
            {
                dir = newDir;
                animator.SetInteger("ElevatorDir", dir > 0 ? 1 : 2);
            }

            if(elevatorStarted && Mathf.Abs(rg.velocity.y) < 0.2f)
            {
                ExitElevatorMode(gm, animator, rg);
                return;
            }

            elevatorStarted = true;
            rg.velocity = new Vector2(0, dir * elevatorSpeed);
        }
        else if(isUsingElevator)
        {
            ExitElevatorMode(gm, animator, rg);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;

        Animator animator = other.gameObject.GetComponent<Animator>();
        GhostMovement gm = other.gameObject.GetComponent<GhostMovement>();
        Rigidbody2D rg = other.gameObject.GetComponent<Rigidbody2D>();

        if(animator == null || gm == null || rg == null) return;

        ExitElevatorMode(gm, animator, rg);
    }

    private void ExitElevatorMode(GhostMovement gm, Animator animator, Rigidbody2D rg)
    {
        isUsingElevator = false;
        elevatorStarted = false;
        dir = 0f;
        gm.isMovementActive = true;
        gm.isOscilationActive = true;
        animator.SetBool("onElevator", false);
        animator.SetInteger("ElevatorDir", 0);
        rg.velocity = new Vector2(rg.velocity.x, 0);
    }
}
