using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Rigidbody rb;
    float multiplier;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(1, 0, 1) * 100 * Time.deltaTime;
        multiplier = 1.00f;
        StartCoroutine("SpeedUp");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision collider)
    {
        rb.velocity *= multiplier;
    }

    IEnumerator SpeedUp()
    {
        float counter = 1f;
        while (true)
        {
            yield return new WaitForSeconds(5);
            multiplier += .01f;
            Debug.Log(multiplier);
        }
    }

}
