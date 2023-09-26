using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;

    private void Start()
    {
        //if(Screen.height / Screen.width == (20f / 9f))
        //{
        //    gameUI.transform.localScale = new Vector3(Screen.width / 1620f, Screen.height / 3600f, 1f);
        //}
    }
    void Update()
    {
        gameUI.transform.localScale = new Vector3(Screen.width / 720f, Screen.height / 1440f, 1f);
    }
}
