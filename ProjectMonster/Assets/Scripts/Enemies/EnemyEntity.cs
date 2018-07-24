using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Damageable {

	public override void TakeDamage(int damage =1)
    {
        hp -= damage;
		if(hp==0) Destroy(this.gameObject);
    }
}
