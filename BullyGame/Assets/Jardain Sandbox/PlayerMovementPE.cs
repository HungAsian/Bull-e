using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPE : MonoBehaviour {

    // Public Variables
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;
    public float jumpStrength;
    public float gravity;

    // Private Variables
    CharacterController player;

    // FSM for animations maybe
    public enum MovementState
    {
        grounded,
        jumping,
        falling,
        running
    }
    public enum FightingState
    {
        attack,
        moving
    }
    public MovementState current; 
	// Use this for initialization
	void Start () {
        // Get Character Controller if it exists, if not create one in the object.
        player = gameObject.GetComponent<CharacterController>();
        if (!player)
        {
            player = gameObject.AddComponent<CharacterController>();
        }
        if (player.isGrounded)
        {
            current = MovementState.grounded;
        }
        else
        {
            current = MovementState.jumping;
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Movement Basics
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement = transform.TransformDirection(movement);
        movement.x *= horizontalMoveSpeed;
        movement.z = 0 ;
        if (player.isGrounded)
        {
            current = MovementState.grounded; 
        }
        else verticalMoveSpeed -= gravity * Time.deltaTime;
        movement.y = verticalMoveSpeed;
        player.Move(movement * Time.deltaTime);

        switch (current)
        {
            case MovementState.grounded:
                grounded();
                break;
            case MovementState.jumping:
                jumping();
                break;
            case MovementState.running:
                running();
                break;
        }
	}
    void grounded() {
        verticalMoveSpeed -= gravity * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            current = MovementState.jumping;
            verticalMoveSpeed = jumpStrength;
        }

    }
    void jumping() {
        verticalMoveSpeed -= gravity * Time.deltaTime;
        if (player.isGrounded)
        {
            current = MovementState.grounded; 
        }
    }
    void running() { }

}
