using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {
	[Header("Board Variables")]
	public int column; // X position
	public int row; // Y position
	public int previousColumn;
	public int previousRow;
	public int targetX;
	public int targetY;
	public bool isMatched = false;

	FindMatches findMatches;
	Board board;
	public GameObject otherDot;
	Vector2 firstTouchPosition;
	Vector2 finalTouchPosition;
	Vector2 tempPosition;
	Animator anim;

	[Header("Swipe Detection")]
	public float swipeAngle = 0;
	public float swipeResist = 0.5f;

	[Header("Power Up")]
	public bool isColumnBomb;
	public bool isRowBomb;
	public GameObject rowArrow;
	public GameObject columnArrow;
	public bool powerUpDot;
	LevelManager levelManager;
	//PowerUpBar thePowerupBar;

	void Start() {
		isColumnBomb = false;
		isRowBomb = false;

		board = FindObjectOfType<Board>();
		findMatches = FindObjectOfType<FindMatches>();
		levelManager = FindObjectOfType<LevelManager>();
		anim = gameObject.GetComponent<Animator>();
		//thePowerupBar = FindObjectOfType<PowerUpBar>(); 

		//targetX = (int)transform.position.x;
		//targetY = (int)transform.position.y;
		//row = targetY;
		//column = targetX;
		//previousRow = row;
		//previousColumn = column;
	}

	//For testing and debuging 
	/*private void OnMouseOver() {

		// MAKES BOMB SPAWN ON RIGHT CLICK
		if(Input.GetMouseButtonDown(1))
		{
			isRowBomb = true;
			GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
			arrow.transform.parent = this.transform;
		}
	}*/

	void Update() {
		//FindMatches();
		
		//if(isMatched) {
		//	SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
		//	mySprite.color = new Color(1f, 1f, 1f, .2f);
		//}

		targetX = column;
		targetY = row;

		if(Mathf.Abs(targetX - transform.position.x) > .1) {
			tempPosition = new Vector2(targetX, transform.position.y);
			transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);
			if(board.allDots[column, row] != this.gameObject) {
				board.allDots[column, row] = this.gameObject;
			}

			findMatches.FindAllMatches();
		} else {
			tempPosition = new Vector2(targetX, transform.position.y);
			transform.position = tempPosition;
		}

		if(Mathf.Abs(targetY - transform.position.y) > 0.1f) {
			tempPosition = new Vector2(transform.position.x, targetY);
			transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);

			if(board.allDots[column, row] != this.gameObject) {
				board.allDots[column, row] = this.gameObject;
			}

			findMatches.FindAllMatches();
		} else {
			tempPosition = new Vector2(transform.position.x, targetY);
			transform.position = tempPosition;
		}
	}

	public IEnumerator CheckMoveCo() {
		yield return new WaitForSeconds(0.5f);

		if(otherDot != null) {
			if(!isMatched && !otherDot.GetComponent<Dot>().isMatched) {
				otherDot.GetComponent<Dot>().row = row;
				otherDot.GetComponent<Dot>().column = column;
				row = previousRow;
				column = previousColumn;
				yield return new WaitForSeconds(0.5f);
				board.currentDot = null;
				board.currentState = GameState.move;
			} else {
				board.DestroyMatches();
			}

			//otherDot = null;
		}
	}

	void OnMouseDown() {
		if(board.currentState == GameState.move) {
			anim.SetTrigger("MouseDown");
			firstTouchPosition = new Vector2(column, row);
			//Debug.Log("anim on");
		}
	}

	void OnMouseUp() {
		if(board.currentState == GameState.move) {
			finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			CalculateAngle();
			anim.ResetTrigger("MouseDown");
			anim.SetTrigger("Idle");
			//Debug.Log("anim off");
		}
	}

	void CalculateAngle() {
		if(Mathf.Abs(finalTouchPosition.y-firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist) {
			swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
			board.currentState = GameState.wait;
			MovePieces();
			board.currentDot = this;
		} else {
			board.currentState = GameState.move;
		}
	}

	void MovePiecesActual(Vector2 direction) {
		otherDot = board.allDots[column + (int)direction.x, row + (int)direction.y];
		previousRow = row;
		previousColumn = column;
		if(otherDot != null) {
			otherDot.GetComponent<Dot>().column += -1 * (int)direction.x;
			otherDot.GetComponent<Dot>().row += -1 * (int)direction.y;
			column += (int)direction.x;
			row += (int)direction.y;
			StartCoroutine(CheckMoveCo());
		} else {
			board.currentState = GameState.move;
		}
	}

	void MovePieces() {
		if(swipeAngle > -38 && swipeAngle <= 38 && column < board.width-1) { // Swipe right
			/*
			otherDot = board.allDots[column + 1, row];
			previousRow = row;
			previousColumn = column;
			otherDot.GetComponent<Dot>().column -= 1;
			column += 1;
			*/
			MovePiecesActual(Vector2.right);
		} else if(swipeAngle > 52 && swipeAngle <= 128 && row < board.height-1) { // Swipe up
			/*
			otherDot = board.allDots[column, row + 1];
			previousRow = row;
			previousColumn = column;
			otherDot.GetComponent<Dot>().row -= 1;
			row += 1;
			*/ 
			MovePiecesActual(Vector2.up);
		} else if((swipeAngle > 142 || swipeAngle <= -142) && column > 0) { // Swipe left
			/*
			otherDot = board.allDots[column - 1, row];
			previousRow = row;
			previousColumn = column;
			otherDot.GetComponent<Dot>().column += 1;
			column -= 1;
			*/
			MovePiecesActual(Vector2.left);
		} else if(swipeAngle < -52 && swipeAngle >= -128 && row > 0) { // Swipe down
			/*
			otherDot = board.allDots[column, row - 1];
			previousRow = row;
			previousColumn = column;
			otherDot.GetComponent<Dot>().row +=1;
			row -= 1;
			*/
			MovePiecesActual(Vector2.down);
		} else {
			board.currentState = GameState.move;
		}
	}

	void FindMatches() {
		if(column > 0 && column < board.width -1) {
			GameObject leftDot1 = board.allDots[column - 1, row];
			GameObject rightDot1 = board.allDots[column + 1, row];

			if(leftDot1 != null && rightDot1 != null) {
				if(leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag) {
					leftDot1.GetComponent<Dot>().isMatched = true;
					rightDot1.GetComponent<Dot>().isMatched = true;
					isMatched = true;
				}
			}
			
		}

		if(row > 0 && row < board.height - 1) {
			GameObject upDot1 = board.allDots[column, row + 1];
			GameObject downDot1 = board.allDots[column, row - 1];
			if(upDot1 != null && downDot1 != null) {
				if(upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag) {
					upDot1.GetComponent<Dot>().isMatched = true;
					downDot1.GetComponent<Dot>().isMatched = true;
					isMatched = true;
				}
			}
		}
	}

	public void MakeRowBomb() {
		isRowBomb = true;
		GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
		arrow.transform.parent = this.transform;
	}

	public void MakeColumBomb() {
		isColumnBomb = true;
		GameObject arrow = Instantiate(columnArrow, transform.position, Quaternion.identity);
		arrow.transform.parent = this.transform;
	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.tag == "PowerUp") {
			PowerUp powerUp = FindObjectOfType<PowerUp>();

			if(powerUp.release) {
				MakeRowBomb();
				powerUpDot = true;
				levelManager.powerUpActive = false;
				powerUp.release = false;
			}
		}
	}

	//void CheckTag() {
	//	if (this.tag == "Teal Dot")
	//	{
	//		thePowerupBar.IncreaseScore(10);
	//		Debug.Log(thePowerupBar.score); 
	//	}
	//}
}