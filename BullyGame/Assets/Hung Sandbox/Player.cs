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
    Transform CoG;
    Transform Paddle;
    Rigidbody PaddleRB;

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
        CoG = transform.GetChild(0);
        Paddle = CoG.transform.GetChild(0);
        PaddleRB = Paddle.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState)
        {
            case MovementState.movable:
                Movement();
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

        if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
            {
                Debug.Log(Vector3.Normalize(Paddle.position - transform.position));
                PaddleRB.AddForce(Vector3.Normalize(Paddle.position - transform.position) * 500);
            }
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                CoG.Rotate(Vector3.up, -300f * Time.deltaTime);
            }
            if (Input.GetButton("Fire2"))
            {
                CoG.Rotate(Vector3.up, 300f * Time.deltaTime);
            }
        }
    }

    void Swing()
    {
    }
}
