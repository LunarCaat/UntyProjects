using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManagerScript : MonoBehaviour {

    public static SpriteManagerScript current;
    public Sprite playerBulllet;
    public Sprite enemyBulllet;
    void Awake()
    {
        current = this;
    }
    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
