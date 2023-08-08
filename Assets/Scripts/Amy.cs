using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amy : MonoBehaviour {
	public bool die;
	Board theBoard;
	TimerCountdown theTimer;
	Animator myAnim;

	public bool attack;
	QuestionManager qnManager;

	void Start() {
		theBoard = FindObjectOfType<Board>();
		theTimer = FindObjectOfType<TimerCountdown>();
		myAnim = GetComponent<Animator>();
		qnManager = FindObjectOfType<QuestionManager>();
	}

	void Update() {
		if(theTimer.timeRemaining <= 0) {
			myAnim.SetBool("Die", die);

			if(!theBoard.goalReached) {
				die = true;
			}
		}
	}

	public void PlayAttackAnim() {
		myAnim.Play("amy attack");
	}

	public void PlayDeadAnim() {
		myAnim.Play("amy cry");
	}
}