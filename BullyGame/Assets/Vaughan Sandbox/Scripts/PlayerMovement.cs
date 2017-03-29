using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public enum PlayerState
    {
        Grounded,
        Jumping
    }

    public float speed;
    public float jumpSpeed = 8.0F;
    public float gravity = 15.0F;
    public float floatingmultiplier = 0.1f;
    float verticalgrav;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 jumpDirection;
    public PlayerState currentState;
    private CharacterController control;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        control = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        control.detectCollisions = false;
        if (control.isGrounded)
        {
            currentState = PlayerState.Grounded;
        }
        else
        {
            currentState = PlayerState.Jumping;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime != 0)
        {

            if(Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetInteger("Walking", 1);
            }


            switch (currentState)
            {
                case PlayerState.Grounded:
                    grounded();
                    break;
                case PlayerState.Jumping:
                    jumping();
                    break;


            }
        }
    }

    void jumping()
    {

        // Falling Gravity
        verticalgrav -= gravity * Time.deltaTime;
        moveDirection.y = verticalgrav;

        // "Float"
        if (Input.GetKey(KeyCode.Space) && moveDirection.y < 0) moveDirection.y *= floatingmultiplier;

        // Transition from Jumping to Landing
        if (control.isGrounded)
        {
            currentState = PlayerState.Grounded;
        }
    }

    void grounded()
    {

        // Grounding Force
        verticalgrav = -gravity * Time.deltaTime;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalgrav = jumpSpeed;
            currentState = PlayerState.Jumping;
        }

        // Falling
        if (!control.isGrounded)

            currentState = PlayerState.Jumping;

        moveDirection.y = verticalgrav;

    }

}
