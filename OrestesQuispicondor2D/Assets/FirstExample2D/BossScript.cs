using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {
    public BlockEntitity block;
    private TopDownMovementWithRigidBody player;
    // Use this for initialization



    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<TopDownMovementWithRigidBody>();
    }
	
	// Update is called once per frame
	void Update () {
		if (block.lifePoints <= 0)
        {
            //player.winCondition = true;
        }
	}
}
