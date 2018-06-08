using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscilatingMovement : MonoBehaviour {

    Vector3 startPosition;
    public float max=1;
    public float speed = 1;
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = startPosition + Mathf.Sin(Time.time*speed)*Vector3.up*max;
	}
}
