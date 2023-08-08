using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml; //Basic XML attributes
using System.Xml.Serialization; // Access XML serialiser
using System.IO; // File management

public class QuestionsInstance: MonoBehaviour {
	public QuestionDatabase questionDB;
	public static QuestionsInstance instance { get; private set; }

	private void Awake() { // Singleton
		if(instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
			//LoadQuestion();
		} else {
			Destroy(gameObject);
		}
	}
	
	[System.Serializable]
	public class QuestionDatabase {
		public List<QuestionsClass> listOfQuestions = new List<QuestionsClass>();
	}
	
	public void LoadQuestion() {
		PlayerPrefs.DeleteAll();
		questionDB.listOfQuestions.Clear();

		QuestionsXMLManager questions = FindObjectOfType<QuestionsXMLManager>();

		for(int i = 0; i < questions.questionDB.listOfQuestions.Count; i++) {
			questionDB.listOfQuestions.Add(questions.questionDB.listOfQuestions[i]);
		}

		//Debug.Log("Questions loaded");
	}
}