using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmingEnemy : MonoBehaviour {
	public int attack=1;
	private Collider2D thisCollider;
	private Transform player;
	private Collider2D playerCollider;
	// Use this for initialization
	void Start(){
        player = GameObject.FindWithTag("Player").transform;
        thisCollider = GetComponent<Collider2D>();
		playerCollider=player.GetComponent<Collider2D>();
		//Physics2D.IgnoreCollision(thisCollider, playerCollider);
	}
	/*void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj=collision.gameObject;
        TopDownMovementWithRigidBody mov =obj.GetComponent<TopDownMovementWithRigidBody>();
        if (obj.CompareTag("Player")&&mov!=null){
        	 
            mov.damagePlayer(attack);
        }
    }*/

    void OnTriggerStay2D(Collider2D other)
    {
        TopDownMovementWithRigidBody mov =other.GetComponent<TopDownMovementWithRigidBody>();
       if (other.CompareTag("Player")&&mov!=null){
        	 
            mov.damagePlayer(attack);
        }
    }
}
