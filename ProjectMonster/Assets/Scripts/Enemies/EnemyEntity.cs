using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : CharacterEntity {

    public int health;
    

    

    public FSM enemyStateMachine;

    public delegate void EnemyBehaviour ();
    public event EnemyBehaviour currentBehaviour;


    protected override void Update () {
        base.Update();
		if (currentBehaviour != null) {
            currentBehaviour ();
        }
       
    }

    
    
    protected void SetCurrentBehaviour (EnemyBehaviour enemyBehaviour) {
        currentBehaviour = enemyBehaviour;
    }
    protected virtual void SendEnemyEvent (int eventIndex) {
        enemyStateMachine.SendEvent (eventIndex);
    }

    

    public void ResetInvulnerable () {
        invulnerable = false;
        if (health <= 0) {
            QuestManager.instance.Check ("destroy", name);
            Destroy (gameObject);
        }
    }

    //OnTriggerEvent Unity Functions
    public virtual void TriggerEnterCall (GameObject objRef) {
        //EMPTY (Call on children only)
    }
    public virtual void TriggerExitCall (GameObject objRef) {
        //EMPTY (Call on children only)
    }
}



