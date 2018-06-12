using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float minutesLeft;
    public float secondsLeft;
    //public float timeStart;
    private float timeLeft;
    private bool stopped=false;
    public Text timerText;

    // Use this for initialization
    void Start() {
        timeLeft = minutesLeft*60 + secondsLeft;
    }

    // Update is called once per frame
    void Update() {
        
        if (!stopped)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < float.Epsilon)
            {
                StopClock();
                timerText.text = "Time:00:00";
                TimeRanOut();
            }
            else
            {
                string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
                string seconds = Mathf.Floor(timeLeft % 60).ToString("00");
                timerText.text = "Time:" + minutes + ":" + seconds;
            }
                
            
        }
        


        
	}
    private void StopClock()
    {
        
        stopped = true;
    }
    private void TimeRanOut()
    {
        PlatformManager.state = PlatformManager.GameState.GAMEOVER;
    }
}
