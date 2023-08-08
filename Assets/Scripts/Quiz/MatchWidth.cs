using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchWidth : MonoBehaviour {
	public float cameraSize = 7.1f;
	float newCamSize;
	public CanvasScaler canvas;

	void Start() {
		//Debug.Log(Camera.main.aspect);
		
		if(Camera.main.aspect < (0.5625f)) { // Rescale camera if width will exceed the camera
			CalcNewSize();
		}
		else if(Camera.main.aspect > (0.5625f)) {
			canvas.matchWidthOrHeight = 1f;
		}
	}

	void CalcNewSize() {
		newCamSize = Camera.main.aspect * 16 / 9;
		newCamSize = cameraSize / newCamSize;
		Camera.main.orthographicSize = newCamSize;
	}
}