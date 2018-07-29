using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FileSelectManager : MonoBehaviour {
	public Text fileText;
	public GameObject fileSelectObject;
	public GameObject nameInputObject;
	
	public InputField inputText;
	
	public string startSceneName;
	
	public Button fileSelectButton;
	
	// Use this for initialization
	void Start () {
		fileSelectObject.SetActive(true);
		nameInputObject.SetActive(false);
		updateFileTextButton();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void updateFileTextButton(){
		if (DataManager.instance.currentData == null || string.IsNullOrEmpty (DataManager.instance.currentData.playerName)){
			fileSelectButton.interactable =false;
			fileText.text ="No file";
			
		}else{
			fileSelectButton.interactable =true;
			fileText.text=DataManager.instance.currentData.playerName;
		}
	}
	
	public void NewGame(){
		fileSelectObject.SetActive(false);
		nameInputObject.SetActive(true);
	}
	public void CreateFile(){
		if (!string.IsNullOrEmpty (inputText.text)) {
			DataManager.instance.currentData= new GameData(inputText.text,0);
			DataManager.instance.SaveGameData();
			updateFileTextButton();
			fileSelectObject.SetActive(true);
			nameInputObject.SetActive(false);
		}
	}
	
	public void StartGame(){
		SceneManager.LoadScene(startSceneName);
	}
	
	
}
