using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float elevatorSpeed = 4.5f;
    private bool isUsingElevator = false;
    private float dir = 0f;

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Animator animator = other.gameObject.GetComponent<Animator>();
            GhostMovement gm = other.gameObject.GetComponent<GhostMovement>();
            Rigidbody2D rg = other.gameObject.GetComponent<Rigidbody2D>();

            if(animator != null && gm != null && rg!=null)
            {
                if(!isUsingElevator)
                {
                    float yVel = Input.GetAxis("Vertical");
                    if(Mathf.Abs(yVel) > 0.1f)
                    {
                        isUsingElevator = true;
                        dir = yVel > 0 ? 1f : -1f;
                    }
                }

                if(isUsingElevator)
                {
                    // Solo activamos lo necesario para el movimiento
                    gm.isMovementActive = false;
                    gm.isOscilationActive = false;
                    rg.velocity = new Vector2(0, dir * elevatorSpeed);

                    // Solo activamos el bool una vez al entrar o cambiar
                    if(!animator.GetBool("onElevator")) 
                    {
                        animator.SetBool("onElevator", true);
                    }
                    
                    // Control de dirección
                    animator.SetInteger("ElevatorDir", dir > 0 ? 1 : 2);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isUsingElevator = false;
            dir = 0f;

            Animator animator = other.gameObject.GetComponent<Animator>();
            GhostMovement gm = other.gameObject.GetComponent<GhostMovement>();
            Rigidbody2D rg = other.gameObject.GetComponent<Rigidbody2D>();

            if(animator != null && gm != null && rg!=null)
            {
                gm.isMovementActive = true;
                animator.SetBool("onElevator", false);
                animator.SetInteger("ElevatorDir", 0);
                gm.isOscilationActive = true;

                rg.velocity = new Vector2(rg.velocity.x, 0);
            }
        }
    }
}
