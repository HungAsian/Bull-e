using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeball : MonoBehaviour {

    // Public Variables
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;
    public float jumpStrength;
    public float gravity;
    public bool isArmed;
    public GameObject dodgeball;
    public float throwStrength;

    // Private Variables
    CharacterController player;
    float verticalGravity;

    //debug
    public GameObject target;

    // FSM for animations maybe
    public enum MovementState 
    {
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
        if (player.isGrounded)
        {
            if (Input.GetButtonDown("Jump")) verticalGravity = jumpStrength;
        }
        else verticalGravity -= gravity * Time.deltaTime;
        movement.y = verticalGravity;
        player.Move(movement * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F6)) isArmed = true;

        if (isArmed && Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(dodgeball,transform.position + new Vector3(.5f,0,0),Quaternion.identity);
            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
            Vector3 direction = Vector3.Normalize(target.transform.position - transform.position);
            projectileRB.AddForce(direction * throwStrength);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dodgeball" && !isArmed)
        {
            isArmed = true;
            Destroy(collision.gameObject);
        }
    }
}
