using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSaver : MonoBehaviour {
	float duration = 60f;

	void Update() {
		duration -= Time.deltaTime;

		if(Input.anyKey) {
			duration = 60f; // 1 minute
		}

		if(duration <= 0) {
			SceneManager.LoadScene("Screen Saver");
		}
	}
}