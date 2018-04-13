using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAreaScript : MonoBehaviour {
    public Light targetLight;
    private float timer;
    private bool runUpdate;
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Stay");
        //if (Input.GetKeyDown(KeyCode.Space) && other.CompareTag("Player"))
        //{
        //    targetLight.enabled = !targetLight.enabled;
        //}
        if(other.CompareTag("Player")&&!runUpdate)
        {
            runUpdate = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Stay");
        //if (Input.GetKeyDown(KeyCode.Space) && other.CompareTag("Player"))
        //{
        //    targetLight.enabled = !targetLight.enabled;
        //}
        if (other.CompareTag("Player"))
        {
            runUpdate = false;
        }
    }

    void Update()
    {
        if (runUpdate)
        {

            //if (((int)Time.time) % 2 == 0)
            //{
            //    targetLight.enabled = !targetLight.enabled;
            //}
            //else
            //{
            //    targetLight.enabled = false;
            //}
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                targetLight.enabled = !targetLight.enabled;
                timer = 0;
            }
        }
    }
}
