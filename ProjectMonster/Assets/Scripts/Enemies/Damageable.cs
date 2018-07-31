using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour {
    public int hp;
    public int Hp { get { return hp; } }
    public abstract void TakeDamage(int damage =1,string effectName = null);
}
