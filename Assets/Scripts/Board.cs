using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {
	wait,
	move,
	end,
	pause
}

public enum TileKind {
	Breakable,
	Blank,
	Normal
}

[System.Serializable]
public class TileType {
	public int x;
	public int y;
	public TileKind tileKind;
}

public class Board : MonoBehaviour {
	public GameState currentState = GameState.move;
	public int width;
	public int height;
	public int offSet;
	public GameObject[] scoreDisplay;
	public GameObject tilePrefab;
	public GameObject[] dots;
	public GameObject bonus;
	public GameObject destroyEffect;
	public TileType[] boardLayout;
	bool[,] blankSpaces;
	public GameObject[,] allDots;
	public Dot currentDot;
	FindMatches findMatches;
	ScoreManager scoreManager;
	SoundManager soundManager;
	public int multiplier;
	public int baseValue;
	public int[] scoreGoals;
	public bool goalReached;
	public bool scoreGoalReached;
	public bool scoreGoalPowerUp;
	public int scoreGoalAmt;
	PowerUpBar thePowerupBar;
	public int powerUpAmt;
	public string thisLevel;
	public string levelHighscore; 
	public float highscore;
	public bool newHighscore;

	void Start() {
		soundManager = FindObjectOfType<SoundManager>();
		findMatches = FindObjectOfType<FindMatches>();
		blankSpaces = new bool[width, height];
		allDots = new GameObject[width, height];
		scoreManager = FindObjectOfType<ScoreManager>();
		thePowerupBar = FindObjectOfType<PowerUpBar>();

		SetUp();

		KeepData.currentScene = SceneManager.GetActiveScene().name;
		highscore = PlayerPrefs.GetInt(levelHighscore);
	}

	void Update() {
		ScoreGoals();
		Goals();
		//Debug.Log(thePowerupBar.score); 
	}

	public void GenerateBlankSpaces() {
		for(int i = 0; i < boardLayout.Length; i ++) {
			if(boardLayout[i].tileKind == TileKind.Blank) {
				blankSpaces[boardLayout[i].x, boardLayout[i].y] = true;
			}
		}
	}

	void SetUp() {
		GenerateBlankSpaces();

		for(int i = 0; i < width; i ++) {
			for(int j = 0; j < height; j ++) {
				if(!blankSpaces[i, j]) {
					Vector2 tempPosition = new Vector2(i, j + offSet);
					GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity);
					backgroundTile.transform.parent = this.transform;
					backgroundTile.name = "(" + i + "," + j + ")";
					int dotToUse = Random.Range(0, dots.Length);

					int maxIterations = 0;
					while(MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100) {
						dotToUse = Random.Range(0, dots.Length);
						maxIterations++;
					}

					maxIterations = 0;

					GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
					dot.GetComponent<Dot>().row = j;
					dot.GetComponent<Dot>().column = i;

					dot.transform.parent = this.transform;
					dot.name = "(" + i + "," + j + ")";
					allDots[i, j] = dot;
				}
			}
		}
	}

	bool MatchesAt(int column, int row, GameObject piece) {
		if(column > 1 && row > 1) {
			if(allDots[column - 1, row] != null && allDots[column - 2, row] != null) {
				if(allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag) {
					return true;
				}
			}

			if(allDots[column, row - 1] != null && allDots[column, row - 2] != null) {
				if(allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag) {
					return true;
				}
			}
		} else if(column <= 1 || row <= 1) {
			if(row > 1) {
				if(allDots[column, row - 1] != null && allDots[column, row - 2] != null) {
					if(allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag) {
						return true;
					}
				}
			}

			if(column > 1) {
				if(allDots[column - 1, row] != null && allDots[column - 2, row] != null) {
					if(allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag) {
						return true;
					}
				}
			}
		}

		return false;
	}

	void DestroyMatchesAt(int column, int row) {
		if(allDots[column, row].GetComponent<Dot>().isMatched) {
			// Make a row or column bomb
			if(findMatches.currentMatches.Count >= 4) {
				findMatches.CheckBombs();
			}

			if(KeepData.currentScene == "Level 0") {
				if(allDots[column, row].tag == "Red Dot" || allDots[column, row].tag == "Green Dot" || allDots[column, row].tag == "Blue Dot") {
					thePowerupBar.IncreaseScore(powerUpAmt);
				}
			}

			if(KeepData.currentScene == "Level 1") {
				if(allDots[column, row].tag == "Green Dot" || allDots[column, row].tag == "Blue Dot" || allDots[column, row].tag == "Yellow Dot") {
					thePowerupBar.IncreaseScore(powerUpAmt);
				}
			}

			if(KeepData.currentScene == "Level 2") {
				if(allDots[column, row].tag == "Red Dot" || allDots[column, row].tag == "Green Dot" || allDots[column, row].tag == "Blue Dot" || allDots[column, row].tag == "Pink Dot") {
					thePowerupBar.IncreaseScore(powerUpAmt);
				}
			}

			findMatches.currentMatches.Remove(allDots[column, row]);
			GameObject particle = Instantiate(destroyEffect, allDots[column, row].transform.position, Quaternion.identity);
			Destroy(particle, 3f);
			Destroy(allDots[column, row]);

			if(allDots[column, row].tag == "Yellow Dot") {
				GameObject yellowscore = Instantiate(scoreDisplay[0], allDots[column, row].transform.position, Quaternion.identity);
				Destroy(yellowscore, 1f);
			}

			if(allDots[column, row].tag == "Red Dot") {
				GameObject redscore = Instantiate(scoreDisplay[1], allDots[column, row].transform.position, Quaternion.identity);
				Destroy(redscore, 1f);
			}

			if(allDots[column, row].tag == "Green Dot") {
				GameObject greenscore = Instantiate(scoreDisplay[2], allDots[column, row].transform.position, Quaternion.identity);
				Destroy(greenscore, 1f);
			}

			if(allDots[column, row].tag == "Blue Dot") {
				GameObject bluescore = Instantiate(scoreDisplay[3], allDots[column, row].transform.position, Quaternion.identity);
				Destroy(bluescore, 1f);
			}

			if(allDots[column, row].tag == "Pink Dot") {
				GameObject pinkscore = Instantiate(scoreDisplay[4], allDots[column, row].transform.position, Quaternion.identity);
				Destroy(pinkscore, 1f);
			}

			scoreManager.IncreaseScore(baseValue * multiplier);
			allDots[column, row] = null;
		}
	}

	public void DestroyMatches() {
		for(int i = 0; i < width; i ++) {
			for(int j = 0; j < height; j ++) {
				if(allDots[i, j] != null) {
					DestroyMatchesAt(i, j);
					if(soundManager != null) {
						soundManager.PlayDestroyNoise();
					}
				}
			}
		}

		findMatches.currentMatches.Clear();
		StartCoroutine(DecreaseRowCo2());
	}

	IEnumerator DecreaseRowCo2() {
		for(int i = 0; i < width; i ++) {
			for(int j = 0; j < height; j ++) {
				// If currrent spot is not blank and is empty
				if(!blankSpaces[i,j] && allDots[i,j] == null) {
					// Loop from the space above to the top of the column
					for(int k = j + 1; k < height; k ++) {
						// If a dot is found
						if(allDots[i, k] != null) {
							// Move that dot to this empty space
							allDots[i, k].GetComponent<Dot>().row = j;
							// Set that spot to be null
							allDots[i, k] = null;
							// Break out of the loop
							break;
						}
					}
				}
			}
		}

		yield return new WaitForSeconds(0.2f);
		StartCoroutine(FillBoardCo());
	}

	IEnumerator DecreaseRowCo() {
		int nullCount = 0;

		for(int i = 0; i < width; i ++) {
			for(int j = 0; j < height; j ++) {
				if(allDots[i, j] == null) {
					nullCount++;
				} else if(nullCount > 0) {
					allDots[i, j].GetComponent<Dot>().row -= nullCount;
					allDots[i, j] = null;
				}
			}

			nullCount = 0;
		}

		yield return new WaitForSeconds(.2f);
		StartCoroutine(FillBoardCo());
	}

	void RefillBoard() {
		for(int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++) {
				if(allDots[i, j] == null && !blankSpaces[i, j]) {
					Vector2 tempPosition = new Vector2(i, j + offSet);
					int dotToUse = Random.Range(0, dots.Length);
					GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
					allDots[i, j] = piece;
					piece.GetComponent<Dot>().row = j;
					piece.GetComponent<Dot>().column = i;

					if(soundManager != null) {
						soundManager.TileLandSound();
					}
				}
			}
		}
	}

	bool MatchesOnBoard() {
		for(int i = 0; i < width; i ++) {
			for(int j = 0; j < height; j ++) {
				if(allDots[i, j] != null) {
					if(allDots[i, j].GetComponent<Dot>().isMatched) {
						return true;
					}
				}
			}
		}

		return false;
	}

	IEnumerator FillBoardCo() {
		RefillBoard();
		yield return new WaitForSeconds(.25f);

		while(MatchesOnBoard()) {
			yield return new WaitForSeconds(.2f);
			//multiplier++; 
			DestroyMatches();
		
		}

		findMatches.currentMatches.Clear();
		currentDot = null;
		yield return new WaitForSeconds(.2f);

		currentState = GameState.move;
		//multiplier = 1; 
	}


	public void Goals() {
		if(scoreManager.score >= scoreGoals[scoreGoals.Length - 1]) {
			//Debug.Log("Tutorial goal reached");
			goalReached = true;
			int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
			//SendSerialSignal();

			if(currentLevel > PlayerPrefs.GetInt("levelReached")) {
				PlayerPrefs.SetInt("levelReached", currentLevel);
			}
		}
	}

	public void ScoreGoals() {
		if(thePowerupBar.score >= scoreGoalAmt && !scoreGoalPowerUp) {
			scoreGoalPowerUp = true;
			scoreGoalReached = true;
			thePowerupBar.score = 0;
		}
	}

	public IEnumerator ScoreGoalsCo() {
		scoreGoalPowerUp = true;
		yield return new WaitForSeconds(10); // Power up effective duration
		scoreGoalPowerUp = false;
	}

	public void EndGame() {
		currentState = GameState.end;
		if(scoreManager.score > highscore) {
			PlayerPrefs.SetInt(levelHighscore, scoreManager.score);
			newHighscore = true;
		} else if(scoreManager.score < highscore) {
			newHighscore = false;
		}

		if(PlayerPrefs.GetInt(levelHighscore) >= scoreGoals[0] && PlayerPrefs.GetInt(levelHighscore) < scoreGoals[1]) {
			PlayerPrefs.SetInt(thisLevel, 1);
			//Debug.Log("1 Star");
		} else if(PlayerPrefs.GetInt(levelHighscore) >= scoreGoals[1] && PlayerPrefs.GetInt(levelHighscore) < scoreGoals[2]) {
			PlayerPrefs.SetInt(thisLevel, 2);
			//Debug.Log("2 Star");
		} else if(PlayerPrefs.GetInt(levelHighscore) >= scoreGoals[2]) {
			PlayerPrefs.SetInt(thisLevel, 3);
			//Debug.Log("3 Star");
		} else {
			PlayerPrefs.SetInt(thisLevel, 0);
			//Debug.Log("Fail");
		}
	}

	public void ResetMultiplier() {
		StartCoroutine(Bonus());
	}

	IEnumerator Bonus() {
		multiplier = 2;
		bonus.SetActive(true);
		yield return new WaitForSeconds(5);
		multiplier = 1;
		bonus.SetActive(false);
	}
}