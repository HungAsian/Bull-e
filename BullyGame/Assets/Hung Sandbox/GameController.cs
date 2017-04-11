using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    float timer = 60;
    GameObject Player1;
    public int P1_score;
    Text UItimer;
    Text UIscore;

	// Use this for initialization
	void Start ()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player");
        P1_score = 0;
        UItimer = transform.GetChild(0).gameObject.GetComponent<Text>();
        UIscore = transform.GetChild(1).gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		//Timer
        timer -= Time.deltaTime;
        if (UItimer.text != "Game Over") UItimer.text = timer.ToString("F0");
        UIscore.text = P1_score.ToString();
        if (timer == 0) EndGame();
        if (Input.GetKeyDown(KeyCode.T)) timer = 60;
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
	}

    public void EndGame()
    {
        UItimer.text = "Game Over";
        Time.timeScale = 0;
    }
}
