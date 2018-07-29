using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClickToGoToAScene : MonoBehaviour {
	public string sceneName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1))
			SceneManager.LoadScene(sceneName);
	}
}
