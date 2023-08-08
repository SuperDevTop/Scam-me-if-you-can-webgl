using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour {

    //[SerializeField] private VideoPlayer videoPlayer;
    
    private void Start()
    {
        //videoPlayer.url = Application.streamingAssetsPath + "/ScreenSaver.mp4";
        //videoPlayer.Play();
    }
    public void Exit() {
		SceneManager.LoadScene("Main Menu");
	}
}