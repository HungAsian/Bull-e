using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Rigidbody rb;
    float multiplier;
    GameController gc;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(1, 0, 1) * 200 * Time.deltaTime;
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        multiplier = 1.00f;
        StartCoroutine("SpeedUp");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision collider)
    {
        rb.velocity *= multiplier;
        if (collider.gameObject.name == "Cylinder") gc.P1_score += 1;
        if (collider.gameObject.tag == "Player") gc.EndGame();
    }

    IEnumerator SpeedUp()
    {
        float multiplier = 1f;
        while (true)
        {
            yield return new WaitForSeconds(5);
            multiplier += .01f;
            Debug.Log(multiplier);
        }
    }

}
