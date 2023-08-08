using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Highscore : MonoBehaviour {
	public Text highscore;
	public GameObject newHighscoreText;
	public string levelHighscore;

	Animation myAnim;
	Board theBoard;

	void Start() {
		theBoard = FindObjectOfType<Board>();
		myAnim = GetComponent<Animation>();

		StartCoroutine(EndCo());

		highscore.text = PlayerPrefs.GetInt(levelHighscore).ToString();

		if(theBoard.newHighscore) {
			newHighscoreText.SetActive(true);
		} else {
			newHighscoreText.SetActive(false);
		}
	}

	public IEnumerator EndCo() {
		yield return new WaitForSeconds(2f);
		myAnim.Play("Highscore");

		yield return new WaitForSeconds(.3f);

		this.gameObject.SetActive(false);
	}
}