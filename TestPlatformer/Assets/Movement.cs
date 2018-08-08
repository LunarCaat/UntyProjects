using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Rigidbody2D charRigidbody2D;
    public float characterSpeed;
    public float jumpSpeed=10;
    public float health = 10;
    public float maxHealth = 10;
    public float normalizedHP { get { return health / maxHealth; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        float horizontalSpeed = Input.GetAxis("Horizontal");
        if(horizontalSpeed!=0){
            charRigidbody2D.velocity= new Vector2(horizontalSpeed* characterSpeed,charRigidbody2D.velocity.y);
        }


	}

	private void FixedUpdate()
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charRigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

        }
	}

    public void TakeDamage(int damage){
        health -= damage;
    }
}
