using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangmanScript : MonoBehaviour {
    public string palabra;
    string palabraEscondida;
    public Text outputText;
    public InputField inputText;
    public int maximoFallas = 4;

    public AudioClip successSound;
    public AudioClip failSound;
    public AudioClip winSound;
    public AudioSource camSource;

    private bool endHangman=false;

    // Use this for initialization
    void Start() {
        inputText.Select();
        foreach (char c in palabra)
        {
            palabraEscondida += "*";
        }
    }
    void Update() {
        if (!endHangman)
        {
            RunHangman();
        }

    }
	// Update is called once per frame
	void RunHangman () {
        if (outputText.text != palabraEscondida)
        {
            outputText.text = palabraEscondida;
        }
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(inputText.text))
        {
             Debug.Log("Enter pressed!");
            //char letra = inputText.text[0];
            string letra = inputText.text.Substring(0, 1);
            if (palabra.Contains(letra))
            {
                //Acerto
                string palabraTemporal = "";
                for (int i = 0; i < palabra.Length; i++)
                {
                    //if (palabra[i] == letra)
                    if (palabra[i] == letra[0])
                    {
                        palabraTemporal += letra;
                    }
                    else
                    {
                        palabraTemporal += palabraEscondida[i];
                    }
                }
                palabraEscondida = palabraTemporal;
                camSource.PlayOneShot(successSound);
            }
            else
            {
                //Fallo
                camSource.PlayOneShot(failSound);
            }
            inputText.text = "";
            inputText.Select();
            if (palabraEscondida == palabra)
            {
                outputText.text = "FELICIDADES, GANASTE!";
                outputText.fontStyle = FontStyle.Bold;
                inputText.gameObject.SetActive(false);
                endHangman = true;
                Debug.Log("Felicidades");
                camSource.PlayOneShot(winSound);
            }

        }
        inputText.ActivateInputField();

        //Debug.Log(palabra);
       
    }
}
