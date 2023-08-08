using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class InstructionController : MonoBehaviour {
	public GameObject pageOne;
	public GameObject pageTwo;
	public GameObject pageThree;

	public Text page; 

	public bool One;
	public bool Two;
	public bool Three;

	void Start() {
		pageOne.SetActive(true);
		One = true;
		Two = false;
		Three = false;
		page.text = "1"; 
	}

	public void NextPage() {
		if(One && !Two && !Three) {
			pageOne.SetActive(false);
			pageTwo.SetActive(true);
			Two = true;
			One = false;
			page.text = "2";
		} else if(Two && !One && !Three) {
			pageThree.SetActive(true);
			pageTwo.SetActive(false);
			Three = true;
			Two = false;
			page.text = "3";
		} else if(Three && !One && !Two) {
			pageOne.SetActive(true);
			pageThree.SetActive(false);
			One = true;
			Three = false;
			page.text = "1";
		}
	}

	public void PreviousPage() {
		if(Three && !One && !Two) {
			pageThree.SetActive(false);
			pageTwo.SetActive(true);
			Two = true;
			Three = false;
			page.text = "2";
		} else if(Two && !One && !Three) {
			pageTwo.SetActive(false);
			pageOne.SetActive(true);
			One = true;
			Two = false;
			page.text = "1";
		} else if(One && !Two && !Three) {
			pageOne.SetActive(false);
			pageThree.SetActive(true);
			Three = true;
			One = false;
			page.text = "3";
		}
	}

	public void BackToWorldMap() {
		SceneManager.LoadScene("Level Select");
	}
}