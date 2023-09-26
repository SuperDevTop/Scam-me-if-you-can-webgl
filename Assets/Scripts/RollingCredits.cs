using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollingCredits : MonoBehaviour {
	Rigidbody2D rb;
	public float duration;
	public float moveSpeed;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		rb.velocity = new Vector3(0f, moveSpeed, 0f);
		duration -= Time.deltaTime;

		if(duration <= 0) {
			SceneManager.LoadScene("Level Select");
		}
	}
}