using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {
	public Button[] levelButtons;

	[Header("Auto Level Loader")]
	public int levelToLoad;
	public float timeToLoad;
	float timer;
	bool stopTimer;
	public LevelLoader[] levelLoader;
	public GameObject[] amyObjects;

	void Start() {
		// Level unlock
		timer = timeToLoad;
		int levelReached = PlayerPrefs.GetInt("levelReached", 2);
		levelToLoad = levelReached;
		for(int i = 0; i < levelButtons.Length; i++) {
			if (i + 2 == levelReached)
			{
				levelButtons[i].interactable = true;
			}
			else
			{
				levelButtons[i].interactable = false;
			}
		}

		amyObjects[levelToLoad - 2].SetActive(true);
	}

	void Update() {
		if(!stopTimer) { // Load the latest level when the timer is up
			timer -= Time.deltaTime;

			if(timer <= 0) {
				stopTimer = true;
				levelLoader[levelToLoad - 2].LoadLevel(levelToLoad);
			}
		}
	}
}