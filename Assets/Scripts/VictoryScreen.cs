using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour {
	Board board;
	ScoreManager scoreManager;
	Image darkScreen;
	public GameObject victoryScreen;
	public GameObject loseScreen;
	public GameObject starHolder;
	public int playerScore;
	public int starGoal;
	bool restart;
	bool updatedScore;

	TimerCountdown timer;

	public GameObject star1;
	public GameObject star2;
	public GameObject star3;

	[Header ("Loading Screen")]
	public GameObject loadingScreen;
	public Slider slider;
	public bool backToMainMenu;

	void Start() {
		board = FindObjectOfType<Board>();
		timer = FindObjectOfType<TimerCountdown>();
		scoreManager = FindObjectOfType<ScoreManager>();
		KeepData.keepLevelName = SceneManager.GetActiveScene().name;

		restart = false;
		updatedScore = false;
	}

	void Update() {
		if(board.goalReached == true && !updatedScore) {
			UpdateStarGoal();
			StartCoroutine(VictoryDelay());
		}

		if(timer.timeRemaining == 0 && board.goalReached == false && !updatedScore) {
			UpdateStarGoal();
			//UpdateStars();
			StartCoroutine(LoseDelay());
		}
	}

	public void UpdateStarGoal() {
		if(scoreManager.score < board.scoreGoals[0]) {
			starGoal = 0;
		}

		if(scoreManager.score >= board.scoreGoals[0] && playerScore <= board.scoreGoals[1]) {
			star1.SetActive(true);
		}

		if(scoreManager.score >= board.scoreGoals[1] && playerScore <= board.scoreGoals[2]) {
			star2.SetActive(true);
		}

		if(scoreManager.score >= board.scoreGoals[2]) {
			star3.SetActive(true);
		}

		board.EndGame();
		updatedScore = true;
	}

	public void NextLevel(string nextLevel) {
		SceneManager.LoadScene(nextLevel);
		//Debug.Log("Unlocked level");
	}

	public void BackToLevelSelect() {
		SceneManager.LoadScene("Level Select");
	}

	public void MainMenu() {
		SceneManager.LoadScene("Main Menu");
	}

	public void Retry() {
		StartCoroutine("RestartLevel");
	}

	IEnumerator RestartLevel() {
		restart = true;
		victoryScreen.SetActive(false);
		loseScreen.SetActive(false);
		starHolder.SetActive(false);
		SceneManager.LoadScene(KeepData.keepLevelName, LoadSceneMode.Single);
		yield return new WaitForSeconds(2.5f);
		restart = false;
	}

	public IEnumerator VictoryDelay() {
		yield return new WaitForSeconds(1f);
		victoryScreen.SetActive(true);

		yield return new WaitForSeconds(2f);
		starHolder.SetActive(true);

		yield return new WaitForSeconds(3f);

		//LoadLevel(5);

		if(backToMainMenu) {
			LoadLevel(5);
		} else {
			LoadLevel(1);
		}
	}

	public IEnumerator LoseDelay()
	{
		yield return new WaitForSeconds(1f);
		loseScreen.SetActive(true);

		yield return new WaitForSeconds(2f);
		starHolder.SetActive(true);
	}

	public void LoadLevel(int sceneIndex) {
		StartCoroutine(LoadAsynchronously(sceneIndex));
	}

	IEnumerator LoadAsynchronously(int sceneIndex) {
		loadingScreen.SetActive(true);

		//yield return new WaitForSeconds(2f);
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		while(!operation.isDone) {
			float progress = Mathf.Clamp01(operation.progress / 0.75f);
			slider.value = progress;
			//Debug.Log(operation.progress);
			yield return null;
		}
	}
}