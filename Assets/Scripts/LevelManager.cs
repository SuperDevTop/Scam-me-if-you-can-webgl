using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	FindMatches findMatches;
	QuestionManager qnManager;
	public GameObject[] powerUps;
	public bool powerUpActive;
	public Transform powerUpSpawn;
	Board board;
	GameObject powerUp;

	public GameObject powerUpActiveMode;
	public GameObject powerUpInactiveMode;
	//public GameObject powerUpHolder;

	void Start() {
		powerUpActive = false; 
		findMatches = FindObjectOfType<FindMatches>();
		qnManager = FindObjectOfType<QuestionManager>();
		board = FindObjectOfType<Board>();
	}

	void Update() {
		//if(powerUpActive)
		//{
		//    return;
		//}
		//Dot dot = FindObjectOfType<Dot>();
		//PowerUp pu = FindObjectOfType<PowerUp>();
		//int powerUpToUse = Random.Range(0, powerUps.Length);

		if(board.scoreGoalPowerUp) {
			powerUp = Instantiate(powerUps[0], powerUpSpawn.position, Quaternion.identity) as GameObject;
			board.scoreGoalPowerUp = false;

			//powerUpActive = true;
			//} else if(powerUpActive && !board.scoreGoalPowerUp) {
		} else {
			board.scoreGoalPowerUp = false;
			Destroy(powerUp, 10f);
			//Debug.Log("Destroy power up");
		}

		if (powerUp != null)
		{
			ActiveModeOn();
		}
		else 
		{
			InactiveModeOn();
		}

		//Debug.Log("PowerUpAppears"); 
		//powerUpActive = true;
	}

	public void InactiveModeOn() {
		if(gameObject != null) {
			powerUpInactiveMode.SetActive(true);
			powerUpActiveMode.SetActive(false);
		}
	}

	public void ActiveModeOn() {
		powerUpActiveMode.SetActive(true);
		powerUpInactiveMode.SetActive(false);
	}
}