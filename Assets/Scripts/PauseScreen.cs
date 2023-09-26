using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {
	public GameObject thePauseScreen;
	LevelManager theLevelManager;

	void Start() {
		theLevelManager = FindObjectOfType<LevelManager>();
	}

	public void PauseGame() {
		Time.timeScale = 0; // Freeze the game
		thePauseScreen.SetActive(true);
	}

	public void ResumeGame() {
		Time.timeScale = 1.0f;
		thePauseScreen.SetActive(false);
	}

	public void BackToMainMenu() {
		Time.timeScale = 1.0f; // Avoid game stuck
		SceneManager.LoadScene("Level Select");
	}
}