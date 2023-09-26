using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public bool die;
	private Board theBoard;
	private TimerCountdown theTimer;
	private Animator myAnim;

	void Start() {
		theBoard = FindObjectOfType<Board>();
		theTimer = FindObjectOfType<TimerCountdown>();
		myAnim = GetComponent<Animator>(); 
	}

	// Update is called once per frame
	//void Update()
	//{
	//    if (theTimer.timeRemaining <= 0)
	//    {
	//        myAnim.SetBool("Die", die); 

	//        if (theBoard.goalReached)
	//        {
	//            die = true;
	//        }
	//    }
	//}
}