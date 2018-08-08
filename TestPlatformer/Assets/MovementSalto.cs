using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSalto : MonoBehaviour {
    public Rigidbody2D charRigidbody2D;
    public float characterSpeed;
    public float jumpSpeed=10;
    public float distance = -1f;
    public int attack = 1;
    public float maxJumpHorizontal = 5f;

    public bool isAttacking = false;

    public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

	}


    public void OnTriggerEnterCall()
	{
        Jump(distance);
        isAttacking = true;
	}

    public void Jump(float jumpDistance){
        Debug.Log("Jumping!");
        charRigidbody2D.AddForce(Vector2.up * jumpSpeed+Vector2.right*jumpDistance, ForceMode2D.Impulse);
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
        GameObject collisionObject = collision.gameObject;
        if(collisionObject.CompareTag("Player")){
            Movement movement = collisionObject.GetComponent<Movement>();
            if(movement){
                movement.TakeDamage(attack);

            }
        }
        if(isAttacking){
            JumpAttack();
        }
	}

    public void JumpAttack(){
        float distanceToPlayer = player.position.x-transform.position.x;
        Debug.Log(distanceToPlayer / 2);
        Jump(Mathf.Clamp(distanceToPlayer / 2,-maxJumpHorizontal,maxJumpHorizontal));
    }
}
