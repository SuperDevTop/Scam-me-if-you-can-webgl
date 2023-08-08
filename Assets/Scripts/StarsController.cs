using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsController : MonoBehaviour {
	public string thisLevel;
	public Image starImage;

	void Start() {
		if(PlayerPrefs.GetInt(thisLevel) == 3) {
			starImage.fillAmount = 1;
		} else if(PlayerPrefs.GetInt(thisLevel) == 2) {
			starImage.fillAmount = 0.67f;
		} else if(PlayerPrefs.GetInt(thisLevel) == 1) {
			starImage.fillAmount = 0.33f;
		} else {
			starImage.fillAmount = 0;
		}
	}
}