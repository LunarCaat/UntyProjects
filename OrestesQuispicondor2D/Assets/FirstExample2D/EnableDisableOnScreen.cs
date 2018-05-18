using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableOnScreen : MonoBehaviour {
    public MonoBehaviour target;
    // Use this for initialization
    void OnBecameVisible()
    {
        target.enabled = true;
        //Debug.Log("Hey I'm visible again!");
    }
    void OnBecameInvisible()
    {
        target.enabled = false;
        //Debug.Log("Hey I'm invisible!");
    }
}
