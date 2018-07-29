using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData{

	public string playerName;
	public int highScore;
	
	
	public GameData(string _playerName, int _highScore)
   {
      playerName = _playerName;
      highScore = _highScore;
   }
}
