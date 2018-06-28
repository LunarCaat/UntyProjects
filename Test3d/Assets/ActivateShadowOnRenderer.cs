using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShadowOnRenderer : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        GetComponent<Renderer>().receiveShadows = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
