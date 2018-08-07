using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour {
	public float blinkDelay=1.5f;
	public Text blinkingText;
	private Color textColor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		textColor=blinkingText.color;
		textColor.a=  colorAlphaBlinkEvaluate(Time.time,blinkDelay);
		blinkingText.color=textColor;
	}
	
	//timeNow should be in range of 0 to 2
	float colorAlphaBlinkEvaluate(float timeNow,float delay){
		float speededDownTime=timeNow/delay;
		int factorFlooredToInt=Mathf.FloorToInt(speededDownTime);
		Debug.Log(factorFlooredToInt);
		if(factorFlooredToInt%2==0){
			return speededDownTime-factorFlooredToInt;
		}else {
			return 1-(speededDownTime-factorFlooredToInt);
		}
			
			
	}
}
