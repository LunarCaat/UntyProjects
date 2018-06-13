using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour {
	public bool hasActivated=false;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            hasActivated=true;
        }

        
    }
}
