using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    float timeLeft;
    public float timeStart;
    public Text timerText;

	// Use this for initialization
	void Start () {
        timeLeft = timeStart;
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time:"+timeLeft.ToString("0.00");
	}
}
