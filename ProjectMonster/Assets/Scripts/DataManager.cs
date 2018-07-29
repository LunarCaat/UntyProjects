using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour {
	
	public GameData currentData;
	static public DataManager instance;
	string gameFileName = "save.json";
	
	
	void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            DestroyImmediate (gameObject);
        }
        
    }
	
	// Use this for initialization
	void Start () {
		//LoadGameData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void LoadGameData(){
		string filePath =Path.Combine(Application.persistentDataPath,gameFileName);
		
		if (File.Exists(filePath)){
			string dataAsJson = File.ReadAllText(filePath);
			currentData=JsonUtility.FromJson<GameData>(dataAsJson);
		}
	}
	
	public void SaveGameData(){
		string dataAsJson = JsonUtility.ToJson (currentData);
		string filePath =Path.Combine(Application.persistentDataPath,gameFileName);
		
		File.WriteAllText (filePath, dataAsJson);
	}
}
