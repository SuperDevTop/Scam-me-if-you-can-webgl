using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour {
	public Text text;

	void Start() {
		StartBlinking();
	}

	void StartBlinking() {
		StartCoroutine("Blink");
	}

	IEnumerator Blink() {
		while(true) {
			switch(text.color.a.ToString()) {
				case "0":
					text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
					yield return new WaitForSeconds(0.5f);
					break;
				case "1":
					text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
					yield return new WaitForSeconds(0.5f);
					break;
			}
		}
	}
}