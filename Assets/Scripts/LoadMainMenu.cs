using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour {
	void Start() {
	}

	void Update() {
		
	}

	public void ExitBtnClick()
    {
		SceneManager.LoadScene(0);
	}
}