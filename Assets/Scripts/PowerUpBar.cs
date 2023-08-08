using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpBar : MonoBehaviour {
	Board board;
	public int score;
	public Image powerupBar;

	void Start() {
		board = FindObjectOfType<Board>();
		UpdateBar();
	}

	public void IncreaseScore(int amountToIncrease) {
		//if (board.scoreGoalPowerUp)
		//{
		//	amountToIncrease *= board.scoreGoalAmt;
		//	score += amountToIncrease;
		//	UpdateBar();
		//}
		//else
		//{
			score += amountToIncrease;
			UpdateBar();
		//}
	}

	void UpdateBar() {
		if(board != null && powerupBar != null) {
			int length = board.scoreGoals.Length;
			powerupBar.fillAmount = (float)score / (float)board.scoreGoalAmt;
		}
	}
}