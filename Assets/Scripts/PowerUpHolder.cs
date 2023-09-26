using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHolder : MonoBehaviour {
	private PowerUp powerUp;
	private LevelManager theLevelManager;

	void Start() {
		powerUp = FindObjectOfType<PowerUp>();
		theLevelManager = FindObjectOfType<LevelManager>();
	}

	void Update() {
		if(powerUp.release) {
			StartCoroutine(DestroyCo());
		}
	}

	public IEnumerator DestroyCo() {
		yield return new WaitForSeconds(0.3f);
		Destroy(gameObject);
		powerUp.release = false;
		theLevelManager.powerUpActive = false;
	}
}