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
    protected void SetRenderColor()
    {
        int materialLength = enemyRenderer.materials.Length;
        for (int i = 0; i < materialLength; i++)
        {
            enemyRenderer.materials[i].color = damage.Evaluate(colorIndex);
        }
    }

}
