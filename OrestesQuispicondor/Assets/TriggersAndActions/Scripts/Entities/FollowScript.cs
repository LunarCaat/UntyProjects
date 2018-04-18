using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {
    public float speed = 1;
    public Transform target;
    Light followerLight;

	// Use this for initialization
	void Start () {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        followerLight=SpawnArea.lightIndicator;
	}
	
	// Update is called once per frame
	void Update () {
		if(followerLight.enabled)
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}
}
