using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyEntity {
    
    public PlatformMovement3DA51 target;
    public Vector3 planarTargetDistance{ get { return new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z); }}


	private void Start()
	{
        enemyRenderer = transform.GetChild(1).GetComponent<Renderer>();
	}

	void Update()
	{
        if(target!=null){
            transform.forward = (planarTargetDistance - transform.position).normalized;
        }


        SetRenderColor();

    }



	



}
