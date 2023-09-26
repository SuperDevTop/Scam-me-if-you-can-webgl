using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
	[DllImport("__Internal")]
	private static extern string GetURLFromPage();
	string frontUrl = "";

	public string ReadURL()
	{
		return GetURLFromPage();
	}

	FindMatches findMatches;
	Board board;
	public bool qnActive;

	[Header("Timer")]
	bool timerActive = false;
	public int qnTime = 20;
	float timeLeft;
	public Text timerText;
	public Image timerImage;

	[Header("Question Cooldown")]
	public bool coolingDown;
	public int cooldownTime;
	//public GameObject cooldown;
	SoundManager qnsSoundManager;

	[Header("Questions Panel")]
	public GameObject questionPanel;
	public Text questionText;
	public Transform answerHolder;
	public GameObject answerButtonPrefab;

	[Header("Explanations Panel")]
	public GameObject correctPanel;
	public GameObject wrongPanel;
	public GameObject explainScreen;
	public Text explainText;
	public float explainTime;
	public Text explainTimeText;
	bool explainActive;

	//[Header("For Testing")]
	//public int qnToTest;

	QuestionsInstance questionManager;
	QuestionsClass qnClass;
	Amy amy;
	LevelManager lvlManager;

	// Question correct/wrong pop ups
	public GameObject[] qnsAnswer;
	int i;

	void Awake() {
		timeLeft = 20;
		coolingDown = false;
	}

	void Start() {
		questionManager = FindObjectOfType<QuestionsInstance>();
		qnsSoundManager = FindObjectOfType<SoundManager>();
		findMatches = FindObjectOfType<FindMatches>();
		board = FindObjectOfType<Board>();
		amy = FindObjectOfType<Amy>();
		lvlManager = FindObjectOfType<LevelManager>();

		qnActive = false;
		//qnToUse = yesNoQns.Length;

		//frontUrl = ReadURL().Split("/")[0] + "//" + ReadURL().Split("/")[2];

		//PlayerPrefs.SetString("FRONT_URL", ReadURL().Split("/")[0] + "//" + ReadURL().Split("/")[2]);
	}

	void Update() { 
		if(findMatches.currentMatches.Count >= 4 && !qnActive && !coolingDown && !board.goalReached) {
			QuestionPopUp();
		}
		
		//if(coolingDown) {
		//	cooldown.SetActive(true);
		//} else {
		//	cooldown.SetActive(false); 
		//}

		//if (timerActive)
		//{
		//	timerLeft -= Time.deltaTime;
		//	ShowTime();
		//	if (timerLeft <= 0)
		//	{
		//		WrongAns();
		//	}
		//}

		if(qnActive) {
			timeLeft -= Time.unscaledDeltaTime;
			timerImage.fillAmount = (timeLeft - 1f) / qnTime;
			timerText.text = timeLeft.ToString("f0") + "s";

			if(timeLeft <= 0) {
				//this.gameObject.SetActive(false);
				WrongAns();
			}
		} else {
			//this.gameObject.SetActive(false);
			timeLeft = 20;
		}
		
		if(explainActive) {
			explainTime -= Time.unscaledDeltaTime;
			explainTimeText.text = explainTime.ToString("f0") + "s";

			if(explainTime <= 0) {
				Close();
			}
		} 
	}

	void QuestionPopUp() {
		//qnToUse = Random.Range(0, yesNoQns.Length);
		//yesNoQns[qnToUse].SetActive(true);
		//Debug.Log("POWER UP");
		int qnNo;

		// Get a question from given list before removing it
		questionPanel.SetActive(true);

		//if(qnToTest >= questionManager.questionDB.listOfQuestions.Count) {
		//	qnNo = Random.Range(0, questionManager.questionDB.listOfQuestions.Count);
		//} else {
		//	qnNo = qnToTest;
		//}

		qnNo = Random.Range(0, questionManager.questionDB.listOfQuestions.Count);
		qnClass = questionManager.questionDB.listOfQuestions[qnNo];
		Debug.Log("Question " + qnNo);

		questionText.text = qnClass.question;
		GetAnswers();
		questionManager.questionDB.listOfQuestions.RemoveAt(qnNo);

		qnActive = true;

		//timerLeft = qnTime;
		//ShowTime();
		//timerActive = true;

		//var newQnTimer = Instantiate(qnTimer, new Vector3(transform.position.x, transform.position.y - 850f, transform.position.z), Quaternion.identity);
		//newQnTimer.transform.SetParent(gameObject.transform);

		Time.timeScale = 0f;
	}

	//void ShowTime()
	//{
	//	int minutes;
	//	int seconds;
	//	string timeString;

	//	minutes = (int)timeLeft / 60;
	//	seconds = (int)timeLeft % 60;
	//	timeString = "0" + minutes.ToString() + ":" + seconds.ToString("d2");
	//	timerText.text = "0" + minutes.ToString() + ":" + seconds.ToString("d2");

	//	timerImage.fillAmount = (timeLeft - 1f) / qnTime;
	//}

	void GetAnswers() { // Put the answers into a list and randomize them which then serve as button options
		List<AnswersClass> questionAnswers = new List<AnswersClass>();

		for(int i = 0; i < qnClass.listOfAnswers.Count; i++) {
			questionAnswers.Add(qnClass.listOfAnswers[i]);
		}

		for(int i = 0; i < qnClass.listOfAnswers.Count; i++) {
			AnswersClass temp = questionAnswers[i];
			int randomIndex = Random.Range(i, questionAnswers.Count);
			questionAnswers[i] = questionAnswers[randomIndex];
			questionAnswers[randomIndex] = temp;
		}

		int fontSize = (int)qnClass.answerFont;

		foreach(AnswersClass answer in questionAnswers) {
			GameObject newButton = Instantiate(answerButtonPrefab) as GameObject;
			AnswerButtons button = newButton.GetComponent<AnswerButtons>();
			button.isAnswer = answer.isAnswer;

			Text buttonText = newButton.GetComponentInChildren<Text>();
			buttonText.text = answer.answer;
			buttonText.fontSize = fontSize;

			newButton.transform.SetParent(answerHolder, false);
		}
	}

	public void CorrectAns() {

		StartCoroutine(CorrectAnsCo());

		if(qnsSoundManager != null) {
			qnsSoundManager.AnsCorrect();
		}

		//Time.timeScale = 0;
		explainTime = 5f; // Reset timer
		correctPanel.SetActive(true);
		ShowExplanation();
		board.ResetMultiplier();
	}

	public void WrongAns() {
		questionPanel.SetActive(false);
		DestroyButtons();
		//Time.timeScale = 0;

		explainTime = 8f; // Reset timer
		wrongPanel.SetActive(true);
		ShowExplanation();

		qnActive = false;
		timerActive = false;
	}

	public void ShowExplanation() {
		explainActive = true;
		explainText.text = qnClass.explanation;
		explainScreen.SetActive(true);
		CoolingDown();
	}

	public IEnumerator CorrectAnsCo() {
		questionPanel.SetActive(false);
		DestroyButtons();
		//yesNoQns[qnToUse].SetActive(false);
		timerActive = false;
		amy.PlayAttackAnim();
		//CoolingDown();
		yield return new WaitForSeconds(0.5f);

		qnActive = false;
	}

	public void Close() {
		//CoolingDown();
		Time.timeScale = 1.0f;
		correctPanel.SetActive(false);
		wrongPanel.SetActive(false);
		explainScreen.SetActive(false);
		explainActive = false;
	}

	public void DestroyButtons() {
		foreach(Transform child in answerHolder) {
			Destroy(child.gameObject);
		}
	}

	void CoolingDown() {
		StartCoroutine(CoolDownCo());
	}

	public IEnumerator CoolDownCo() {
		coolingDown = true;
		yield return new WaitForSeconds(cooldownTime);
		coolingDown = false;
	}
}