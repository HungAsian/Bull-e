using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    float timer;
    GameObject Player1;
    GameObject Player2;
    int P1_score;
    int P2_score;

	// Use this for initialization
	void Start ()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player");
        P1_score = 0;
        P2_score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Timer
        timer -= Time.deltaTime;
	}
}
