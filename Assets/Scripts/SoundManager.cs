using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public AudioSource answerCorrect;
	public AudioSource buttonClick;
	public AudioSource backButtonClick;
	public AudioSource destroyNoise;
	public AudioSource tileLanding;

	public void PlayDestroyNoise() {
		destroyNoise.Play();
	}

	public void ClickSound() {
		buttonClick.Play();
	}

	public void backClickSound() {
		backButtonClick.Play();
	}

	public void TileLandSound() {
		tileLanding.Play();
	}

	public void AnsCorrect() {
		answerCorrect.Play();
	}
}