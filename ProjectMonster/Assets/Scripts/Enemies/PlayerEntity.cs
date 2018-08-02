using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity {

	public float maxHP = 1f;
	public float normalizedHP { get { return hp / maxHP; } }
	public Animator animator2d;
	
	protected override void Update () {
        base.Update();
		SetRenderColor (colorIndex);
       
    }
	
	
	public override void TakeDamage (int damage =1, string effectName = null) {
        if (!invulnerable) {
            Debug.Log ("TakeDamage!");
            hp -= damage;
			invulnerable = true;
			StartCoroutine(InterpolateColor(2f));
			if (hp <= 0) {
				
				UIManager.state=UIManager.GameState.GAMEOVER;
                //Anadir logica de ganado o perdido de juego
			}
			
            //invulnerable = true;

            if (effectName != null) {
                Debug.Log ("Will try to apply " + effectName);
                effect = EffectManager.instance.Search (effectName).Apply (this);
            }
        }
    }
	public void ResetInvulnerable () {
        invulnerable = false;
        animator2d.SetTrigger ("BackToNormal");
    }
	IEnumerator InterpolateColor(float completeTime){
		float time=0;
		while(time<completeTime){
			time+=Time.deltaTime;
			colorIndex=time/completeTime;
			Debug.Log("Color index: "+colorIndex);
			yield return null;
		}
		colorIndex=0;
		invulnerable = false;
		yield return null;
	}
	
	
	void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<EnemyEntity>()){
			Debug.Log("Enemy Collision!!");
			TakeDamage(1);
		}
    }
	
}
