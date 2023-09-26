using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
	public GameObject loadingScreen;
	public Slider slider;
	public GameObject highlightEffect;

	public void LoadLevel (int sceneIndex) {
		StartCoroutine(LoadAsynchronously(sceneIndex));
	}

	IEnumerator LoadAsynchronously (int sceneIndex) {
		highlightEffect.SetActive(true);
		loadingScreen.SetActive(true);

		yield return new WaitForSeconds(2f);
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		while(!operation.isDone) {
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			slider.value = progress;
			//Debug.Log(operation.progress);
			yield return null;
		}
	}
}