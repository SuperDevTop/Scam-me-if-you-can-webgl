using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsCredits: MonoBehaviour {
	public void Instructions() {
		SceneManager.LoadScene("Instructions");
	}

	public void Credits() {
		SceneManager.LoadScene("Credits");
	}
}