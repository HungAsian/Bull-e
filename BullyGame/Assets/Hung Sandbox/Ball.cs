using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(1, 0, 1) * 1000 * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision");
        Debug.Log(collider.transform.parent.name);
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Transform bat = collider.transform.parent.transform.parent.transform.parent.transform.GetChild(0).GetChild(0).transform;
        Vector3 side1 = transform.position - player.position;
        Vector3 side2 = bat.position - player.position;
        Vector3.Reflect(rb.velocity, -Vector3.Cross(Vector3.Cross(side1, side2), side2));
        Debug.DrawLine(transform.position, -Vector3.Cross(Vector3.Cross(side1, side2), side2).normalized * 100);
        Debug.Break();
    }
}
