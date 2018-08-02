using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity {

	public float maxHP = 1f;
	public float normalizedHP { get { return hp / maxHP; } }
	
	public override void TakeDamage (int damage =1, string effectName = null) {
        if (!invulnerable) {
            Debug.Log ("TakeDamage!");
            hp -= damage;
            //GetComponent<Animator> ().SetTrigger ("TakeDamage");
			if (hp <= 0) {
                //Anadir logica de ganado o perdido de juego
			}
			
            //invulnerable = true;

            if (effectName != null) {
                Debug.Log ("Will try to apply " + effectName);
                effect = EffectManager.instance.Search (effectName).Apply (this);
            }
        }
    }
}
