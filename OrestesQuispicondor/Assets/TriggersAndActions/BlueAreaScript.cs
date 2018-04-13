using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAreaScript : MonoBehaviour {
    public Light targetLight;
    public float maxIllumination=10;
    public float lightenSpeed = 2.0f;

    void OnTriggerEnter (Collider other)
    {
        //Debug.Log("Enter");
        if (other.CompareTag("Player"))
        {
            targetLight.enabled = true;
            //StartCoroutine(prenderLuz()); ;
        }
    }
    //public void prenderLuz()
    //{
    //    while (targetLight.intensity <= maxIllumination)
    //        targetLight.intensity += 0.2f * Time.deltaTime;
    //    targetLight.intensity = maxIllumination;
    //}
    IEnumerator prenderLuz()
    {
        targetLight.enabled = true;
        while (targetLight.intensity <= maxIllumination)
        {
            targetLight.intensity += 3f * Time.deltaTime;
            yield return new WaitForSeconds(0.0000001f / lightenSpeed);
        }
        targetLight.intensity = 0;
        

    }
}
