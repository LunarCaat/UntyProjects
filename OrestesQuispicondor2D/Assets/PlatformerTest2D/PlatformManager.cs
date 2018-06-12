using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour {
    public enum GameState { NOT_FINISHED, GAMEOVER,SUCCESS};
    public static GameState state;
    public GameObject gameOverUI;
    public GameObject successUI;
    private bool finished=false;
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
                gameOverResult();
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
        showGameOverScreen();
        finished = true;
    }
    void showSuccessScreen()
    {
        successUI.SetActive(true);
    }
    void resetState()
    {
        state = GameState.NOT_FINISHED;
    }
}
