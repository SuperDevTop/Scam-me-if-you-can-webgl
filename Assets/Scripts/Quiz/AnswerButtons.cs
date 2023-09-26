using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButtons : MonoBehaviour {
	public bool isAnswer;
	QuestionManager qnManager;

	void Start() {
		qnManager = FindObjectOfType<QuestionManager>();

		if(isAnswer) {
			this.name = "Answer";
		}
	}

	public void SelectButton() {
		if(isAnswer) {
			qnManager.CorrectAns();
			//Debug.Log("Correct");
		} else if(!isAnswer) {
			qnManager.WrongAns();
			//Debug.Log("Wrong");
		}
	}
}