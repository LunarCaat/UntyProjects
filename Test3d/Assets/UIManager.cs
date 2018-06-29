using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Image hpBar;
    public Gradient barColors;
    float hp;
    public PlayerScript playerScript;

	// Use this for initialization
	void Start () {
        hpBar.fillAmount = playerScript.normalizedHP;
	}
	
	// Update is called once per frame
	void Update () {
        if(hpBar.fillAmount!=playerScript.normalizedHP){
            float delta = Mathf.Abs(hpBar.fillAmount - playerScript.normalizedHP);
            if (delta < 0.2f) { delta = 0.2f; }
            hpBar.fillAmount = Mathf.MoveTowards(hpBar.fillAmount,playerScript.normalizedHP,delta * Time.deltaTime);
            hpBar.color = barColors.Evaluate(hpBar.fillAmount);
        }
	}
}
