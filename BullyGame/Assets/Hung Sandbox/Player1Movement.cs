using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour {

    // Public Variables
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;
    public float jumpStrength;
    public float gravity;

    // Private Variables
    CharacterController player;

    // FSM for animations maybe
    public enum MovementState {
        idle
    }

	// Use this for initialization
	void Start () {
        // Get Character Controller if it exists, if not create one in the object.
        player = gameObject.GetComponent<CharacterController>();
        if (!player)
        {
            player = gameObject.AddComponent<CharacterController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Movement Basics
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement = transform.TransformDirection(movement);
        movement.x *= horizontalMoveSpeed;
        movement.z *= verticalMoveSpeed;
        if (Input.GetButtonDown("Jump")) movement.y = jumpStrength;
        else movement.y -= gravity * Time.deltaTime;
        player.Move(movement * Time.deltaTime);
	}
}
