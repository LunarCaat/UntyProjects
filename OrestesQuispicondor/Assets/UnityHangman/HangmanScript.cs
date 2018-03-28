using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangmanScript : MonoBehaviour {
    public string palabra;
    string palabraEscondida;
    public Text output; 

    // Use this for initialization
    void Start() { 
        
        foreach (char c in palabra)
        {
            palabraEscondida += "*";
        }
    }
    
	// Update is called once per frame
	void Update () {
        output.text = palabraEscondida;
        //Debug.Log(palabra);

    }
}
