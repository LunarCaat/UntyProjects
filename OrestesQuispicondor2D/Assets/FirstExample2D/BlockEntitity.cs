using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEntitity : MonoBehaviour {


    public int lifePoints;
    public bool isBoss;

 

	
    public void DecreaseLife(int ammount)
    {
        lifePoints -= ammount;
        if (lifePoints <= 0)
        {
            if (isBoss) TopDownMovementWithRigidBody.winCondition = true;
            Destroy(gameObject);
        }
    }
}
