using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlatformManager : MonoBehaviour {
    public enum GameState { NOT_FINISHED, GAMEOVER,SUCCESS};
    public static GameState state;
    public GameObject gameOverUI;
    public GameObject successUI;
    public Text successText;
    public Timer timer;
    private bool finished=false;

    public Text timerText;
	// Use this for initialization
	void Start () {
        gameOverUI.SetActive(false);
        successUI.SetActive(false);
        state = GameState.NOT_FINISHED;
    }
	
	// Update is called once per frame
	void Update () {
        if (!finished) {
            if (state == GameState.GAMEOVER)
            {
                gameOverResult();
            }
            if (state == GameState.SUCCESS)
            {
                successResult();
            }
        }

        
	}

    void gameOverResult()
    {
        showGameOverScreen();
        finished = true;
    }

    void showGameOverScreen()
    {
        gameOverUI.SetActive(true);
    }

    public void resetGame()
    {
        SceneManager.LoadScene("MainMenu");
        resetState();
    }
    void successResult()
    {
        showSuccessScreen();
        finished = true;
    }
    void showSuccessScreen()
    {
        string minutes = Mathf.Floor(timer.timePassed / 60).ToString("00");
        string seconds = Mathf.Floor(timer.timePassed % 60).ToString("00");
        string timeClearedStr = minutes + ":" + seconds;
        successText.text = "Congratulations!\r\nClear time: " + timeClearedStr;
        timerText.enabled =false;
        successUI.SetActive(true);
    }
    void resetState()
    {
        state = GameState.NOT_FINISHED;
    }
}
