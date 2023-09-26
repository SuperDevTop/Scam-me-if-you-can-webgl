using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	private Board board;
	public Text scoreText;
	public int score;
	public Image scoreBar;

	public GameObject star1;
	public GameObject star2;
	public GameObject star3;

	void Start() {
		board = FindObjectOfType<Board>();
		UpdateBar();
	}

	void Update() {
		scoreText.text = "" + score;
	}

	public void IncreaseScore(int amountToIncrease) {
		score += amountToIncrease;
		UpdateBar();
	}

	private void UpdateBar() {
		if(board != null && scoreBar != null) {
			int length = board.scoreGoals.Length;
			scoreBar.fillAmount = (float)score / (float)board.scoreGoals[length - 1];
		}

		if(score >= board.scoreGoals[0]) {
			star1.SetActive(true);
		}

		if(score >= board.scoreGoals[1]) {
			star2.SetActive(true);
		}

		if(score >= board.scoreGoals[2]) {
			star3.SetActive(true);
		}
	}
}