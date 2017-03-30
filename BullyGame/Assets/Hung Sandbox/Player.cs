using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Public Variables
    public float horizontalMoveSpeed = 10;
    public float verticalMoveSpeed = 15;
    public float gravity = 50;
    public GameObject bat;
    public float swingSpeed = 15;

    // Private Variables
    CharacterController player;
    float verticalGravity;
    MovementState currentState;
    GameObject ball;
    GameObject swingingBat;
    float targetAngle;
    public int counter;

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
        ball = GameObject.FindGameObjectWithTag("Dodgeball");
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
            counter = 0;
            swingingBat = Instantiate(bat, transform, false);
            
            targetAngle = 110;
            if (Vector3.Dot(ball.transform.position - transform.position, Vector3.back) > 0)
            {
                Vector3 rotationAngle = swingingBat.transform.GetChild(0).eulerAngles + 180f * Vector3.up;
                swingingBat.transform.GetChild(0).eulerAngles = rotationAngle;
                rotationAngle = swingingBat.transform.GetChild(1).eulerAngles + Vector3.right * 180f;
                swingingBat.transform.GetChild(1).eulerAngles = rotationAngle;
                targetAngle = 60;
            }
            swingingBat.SetActive(true);
        }
        if (Input.GetButtonUp("Fire1") && swingingBat)
        {
            currentState = MovementState.swinging;
        }
    }

    void Swing()
    {
        Transform hbcog = swingingBat.transform.GetChild(1).transform;
        if (counter == 0)
        {
            if (hbcog.GetChild(0).gameObject.activeSelf)
            {
                hbcog.GetChild(0).gameObject.SetActive(false);
                hbcog.GetChild(1).gameObject.SetActive(true);
                counter = 2;
            }
            else if (hbcog.GetChild(1).gameObject.activeSelf)
            {
                hbcog.GetChild(1).gameObject.SetActive(false);
                hbcog.GetChild(2).gameObject.SetActive(true);
                counter = 1;
            }
            else if (hbcog.GetChild(2).gameObject.activeSelf)
            {
                hbcog.GetChild(2).gameObject.SetActive(false);
                hbcog.GetChild(3).gameObject.SetActive(true);
                counter = 1;
            }
            else if (hbcog.GetChild(3).gameObject.activeSelf)
            {
                hbcog.GetChild(3).gameObject.SetActive(false);
            }
        }
        if (targetAngle == 60)
        {
            swingingBat.transform.GetChild(0).transform.Rotate(Vector3.up, -swingSpeed);
        }
        else swingingBat.transform.GetChild(0).transform.Rotate(Vector3.up, swingSpeed);

        counter--;

        if (Mathf.Abs(swingingBat.transform.GetChild(0).transform.rotation.eulerAngles.y - targetAngle) < 10)
        {
            Destroy(swingingBat);
            currentState = MovementState.movable;
        }
    }

}
