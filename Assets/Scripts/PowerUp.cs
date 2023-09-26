using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	[SerializeField]
	Transform powerUpHolder;
	Vector2 initialPosition;
	Vector2 mousePosition;
	float deltaX, deltaY;
	public bool release;
	//LevelManager lvlManager;
	public float timer = 10f;

	void Start() {
		initialPosition = transform.position;
		//lvlManager = FindObjectOfType<LevelManager>();
		//lvlManager.ActiveModeOn();
	}

	//void Update() {
	//	timer -= Time.deltaTime;
	//	if(timer <= 0) {
	//		lvlManager.InactiveModeOn();
	//		Destroy(this.gameObject);
	//	}

		////For Mobile 
		//if (Input.touchCount > 0) {
		//	Touch touch = Input.GetTouch(0);
		//	Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 

		//	switch (touch.phase)
		//	{
		//		case TouchPhase.Began:
		//			if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)) {
		//				deltaX = touchPos.x - transform.position.x;
		//				deltaY = touchPos.y - transform.position.y;
		//			}
		//			break;

		//		case TouchPhase.Moved:
		//			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)) {
		//				transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY); 
		//			}
		//			break;

		//		case TouchPhase.Ended:
		//			transform.position = new Vector2(transform.position.x, transform.position.y);
		//			release = true;

		//			break; 
		//	}
		//}
	//}

	// For PC platform use
	void OnMouseDown() {
		deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
		deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
		//Debug.Log("MouseDown"); 
	}

	void OnMouseDrag() {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
		//Debug.Log("MouseDrag");
	}

	void OnMouseUp() {
		//if(Mathf.Abs(transform.position.x - powerUpHolder.position.x) <= 0.05f && Mathf.Abs(transform.position.y - powerUpHolder.position.y) <= 0.05f) {
		//	transform.position = new Vector2(powerUpHolder.position.x, powerUpHolder.position.y);
		//} else {
			transform.position = new Vector2(transform.position.x, transform.position.y);
			release = true;
			//lvlManager.InactiveModeOn();
		//}
	}

	//void OnDestroy() {
	//	lvlManager.InactiveModeOn();
	//}
}