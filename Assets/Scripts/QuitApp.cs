using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitApp : MonoBehaviour {
	public void OnApplicationQuit() {	
		StartCoroutine("DelayQuit");
	}

	IEnumerator DelayQuit() {
		yield return new WaitForSeconds(2.0f);
		Application.Quit();
	}
}