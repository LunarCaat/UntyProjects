using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : Damageable {
	
	public float colorIndex = 0f;
    public Gradient damageGradient;
	protected Effect effect;
	protected bool invulnerable = false;
	public SpriteRenderer enemyRenderer;
	public Color currentBase;
	protected Color baseColor;
	public float baseSpeed = 4.5f;
	public float speed;
	public bool changedBaseColor { get { return currentBase != baseColor; } }
	
	protected virtual void Start () {
		speed=baseSpeed;
        //enemyRenderer = transform.GetChild (1).GetComponent<Renderer> ();
        currentBase = baseColor = enemyRenderer.material.color;
    }
	
	protected virtual void Update () {
        
        if (effect != null) {
            if (effect.Update (Time.deltaTime)) {
                currentBase = baseColor;
                //ApplyColor (currentBase);
				ApplySpriteColor(currentBase);
                speed = baseSpeed;
                effect = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            Debug.LogWarning (changedBaseColor);
        }
    }
	
	protected void SetRenderColor (float gradientPick) {
        if (changedBaseColor || gradientPick != 0f) {
            Color targetColor = damageGradient.Evaluate (gradientPick);
            if (changedBaseColor) { targetColor = (targetColor * 0.7f) + (currentBase * 0.3f); }
            //ApplyColor (targetColor);
			ApplySpriteColor(targetColor);
        }
    }
	public override void TakeDamage (int damage =1, string effectName = null) {
        if (!invulnerable) {
            Debug.Log ("TakeDamage!");
            hp -= damage;
            //GetComponent<Animator> ().SetTrigger ("TakeDamage");
			if (hp <= 0) {
                invulnerable = true;
				QuestManager.instance.Check ("destroy", gameObject.tag);
				Destroy (gameObject);
			}
			
            //invulnerable = true;

            if (effectName != null) {
                Debug.Log ("Will try to apply " + effectName);
                effect = EffectManager.instance.Search (effectName).Apply (this);
            }
        }
    }
	
	// protected void ApplyColor (Color targetColor) {
        // for (int i = 0; i < enemyRenderer.materials.Length; i++) {
            // enemyRenderer.materials[i].color = targetColor;
        // }
    // }
	
	protected void ApplySpriteColor (Color targetColor) {
        
            enemyRenderer.color = targetColor;
        
    }
	
	
}
