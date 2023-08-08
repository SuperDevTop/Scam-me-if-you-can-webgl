using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour {
	[SerializeField]
	float startTime;
	public float timeRemaining;
	public Text countDownTimeText;
	Animation myAnim;
	Board theBoard;
	bool startCounter;

	public float levelTagTimer = 1.5f;

	void Start() {
		myAnim = GetComponent<Animation>();
		theBoard = FindObjectOfType<Board>();
		
		timeRemaining = startTime;
	}

	void Update() {
		if(startCounter) {
			CountDown();
		} else {
			if(Time.timeSinceLevelLoad >= levelTagTimer) {
				startCounter = true;
			}
		}
	}

	void CountDown() {
		timeRemaining = startTime - (Time.timeSinceLevelLoad - levelTagTimer);

		if(timeRemaining <= 0) {
			theBoard.EndGame();
			timeRemaining = 0;
			myAnim.Stop("Timer blink");
		}

		if(timeRemaining <= 10) {
			myAnim.Play("Timer blink");
		} else if(timeRemaining <= 30) {
			countDownTimeText.color = Color.red;
		}

		ShowTime();
	}

	void ShowTime() {
		int minutes;
		int seconds;
		string timeString;

		minutes = (int)timeRemaining / 60;
		seconds = (int)timeRemaining % 60;
		timeString = "0" + minutes.ToString() + ":" + seconds.ToString("d2");
		countDownTimeText.text = "0" + minutes.ToString() + ":" + seconds.ToString("d2");
	}
}