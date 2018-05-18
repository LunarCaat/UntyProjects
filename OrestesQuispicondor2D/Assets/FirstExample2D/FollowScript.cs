using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {
    private Transform target;
    public float speed = 1;
    public TriggerArea trigger;
    private Rigidbody2D rb2D;
    // Use this for initialization
    void Start () {
		//PorHacer Inicializar
		target = GameObject.FindGameObjectWithTag("Player").transform;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	//void Update () {
	//	if(trigger==null||(trigger!=null&&trigger.hasActivated))
	//	transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	//}
    void FixedUpdate()
    {
        if (trigger == null || (trigger != null && trigger.hasActivated))
        {
            Vector3 movePosition = transform.position;
            movePosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            rb2D.MovePosition(movePosition);
        }
            
    }

}
