using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStarted = false;

    private void Awake() {
        Singleton();
    }

    void Update()
    {
        if (isGameStarted)
        {
            RunClock();
        }
    }

    public void StartGame(){
        isGameStarted = true;
    }

    public bool IsGameStarted(){
        return isGameStarted;
    }

    //********************************************
    // Button Actions
    //********************************************
    public void Paused () { Time.timeScale = 0; }

    public void UnPause () {
        Time.timeScale = 1;
    }
    
    //***********************
    // Time Action
    //***********************
    public string Timer()
    {
        _elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_elapsedTime / 60F);
        seconds += Time.deltaTime;

        if (seconds >= 60)
        {
            seconds = 0;
        }
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RunClock()
    {
        _clockTxt.text = Timer();
    }

    void Singleton(){
        if(instance == null || instance != this){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }
}
