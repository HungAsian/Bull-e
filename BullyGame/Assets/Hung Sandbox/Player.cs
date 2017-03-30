using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Public Variables
    public float horizontalMoveSpeed = 10;
    public float verticalMoveSpeed = 15;
    public float gravity = 50;

    // Private Variables
    CharacterController player;
    float verticalGravity;
    MovementState currentState;

    //debug
    public GameObject target;

    // FSM for animations maybe
    public enum MovementState
    {
        movable,
        swinging
    }
	// Use this for initialization
	void Start () {
        player = gameObject.GetComponent<CharacterController>();
        if (!player)
        {
            player = gameObject.AddComponent<CharacterController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState)
        {
            case MovementState.movable:
                Movement();
                break;
            case MovementState.swinging:
                Swing();
                break;
        }
	}

    void Movement()
    {
        // Movement Basics
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement = transform.TransformDirection(movement);
        movement.x *= horizontalMoveSpeed;
        movement.z *= verticalMoveSpeed;
        verticalGravity = -gravity * Time.deltaTime;
        movement.y = verticalGravity;
        player.Move(movement * Time.deltaTime);


        if(Input.GetButtonDown("Fire1"))
        {
            Transform child = transform.GetChild(0);
            Rigidbody childrb = child.GetComponent<Rigidbody>();
            childrb.AddForce(Vector3.Normalize(childrb.position - transform.position) * 300);
            Debug.Log(Vector3.Normalize(childrb.position - transform.position));
        }
    }

    void Swing()
    {
    }
}
