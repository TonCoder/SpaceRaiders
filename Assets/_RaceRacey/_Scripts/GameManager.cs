using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI _clockTxt;
    public TextMeshProUGUI _distanceTxt;
    public Transform _player;

    public bool isGameStarted = false;
    private float _seconds;
    private float _elapsedTime;

    // Distance info
    private Transform playerStartPos;

    private void Awake()
    {
        Singleton();
    }

    void Update()
    {
        if (isGameStarted)
        {
            var dist = (int)Vector3.Distance(new Vector3(0, 0, 0), _player.position);
            _distanceTxt.text = dist.ToString();
            RunClock();
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        playerStartPos = _player;
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    //********************************************
    // Button Actions
    //********************************************
    public void Paused() { Time.timeScale = 0; }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    //***********************
    // Time Action
    //***********************
    public string Timer()
    {
        _elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_elapsedTime / 60F);
        _seconds += Time.deltaTime;

        if (_seconds >= 60)
        {
            _seconds = 0;
        }
        return string.Format("{0:00}:{1:00}", minutes, _seconds);
    }

    public void RunClock()
    {
        _clockTxt.text = Timer();
    }

    void Singleton()
    {
        if (instance == null || instance != this)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
