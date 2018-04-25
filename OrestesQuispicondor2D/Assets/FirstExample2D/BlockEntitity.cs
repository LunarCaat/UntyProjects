using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEntitity : MonoBehaviour {


    public int lifePoints;
	
    public void DecreaseLife(int ammount)
    {
        lifePoints -= ammount;
        if (lifePoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
