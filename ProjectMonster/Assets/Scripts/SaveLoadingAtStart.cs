using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadingAtStart : MonoBehaviour {
	
	public string sceneName;
	// Use this for initialization
	void Start () {
		DataManager.instance.LoadGameData();
		SceneManager.LoadScene(sceneName);
	}
	
	
}
