using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : DamageableObject {
    public int health;
    [SerializeField]
    bool invulnerable = false;
    public float colorIndex = 0f;
    public Gradient damage;
    public Renderer enemyRenderer;


    public delegate void EnemyBehaviour();
    public event EnemyBehaviour currentBehaviour;


    public FSM enemyStateMachine;

	protected virtual void Start()
	{
        
        enemyRenderer = transform.GetChild(1).GetComponent<Renderer>();
	}

    protected virtual void Update()
	{
        Debug.Log("Updating parent");
        if (currentBehaviour != null)
            currentBehaviour();
	}


	public override void TakeDamage()
    {
        if (!invulnerable)
        {
            Debug.Log("Take Damage!");
            health--;
            GetComponent<Animator>().SetTrigger("TakeDamage");
            invulnerable = true;
        }

    }
    public void ResetInvulnerable()
    {

        invulnerable = false;
        if (health < 0)
        {
            QuestManager.instance.Check("destroy", name);
            Destroy(gameObject);
        }
    }

    protected void SetCurrentBehaviour(EnemyBehaviour enemyBehaviour){
        currentBehaviour = enemyBehaviour;
    }
    protected virtual void SendEnemyEvent(int eventIndex){
        enemyStateMachine.SendEvent(eventIndex);
    }


    protected void SetRenderColor(float gradientPick)
    {
        int materialLength = enemyRenderer.materials.Length;
        for (int i = 0; i < materialLength; i++)
        {
            enemyRenderer.materials[i].color = damage.Evaluate(gradientPick);
        }
    }
    public virtual void TriggerEnterCall(GameObject objRef){
        
    }
    public virtual void TriggerExitCall(GameObject objRef)
    {

    }
}
