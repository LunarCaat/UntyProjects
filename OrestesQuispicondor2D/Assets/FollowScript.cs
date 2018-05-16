using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {
    private Transform target;
    public float speed = 1;
    public TriggerArea trigger;
    // Use this for initialization
    void Start () {
		//PorHacer Inicializar
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(trigger==null||(trigger!=null&&trigger.hasActivated))
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}
}
