using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAreaScript : MonoBehaviour {
    public Light targetLight;
    public float fadeOutSpeed=2.0f;
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");
        if (other.CompareTag("Player"))
        {
            targetLight.enabled = false;

            //StartCoroutine(apagarLuz());
            
            
        }
    }
    IEnumerator apagarLuz()
    {
        while (targetLight.intensity >= 0)
        {
            targetLight.intensity -= 3f * Time.deltaTime;
            yield return new WaitForSeconds(0.0000001f / fadeOutSpeed);
        }
        targetLight.intensity = 0;
        targetLight.enabled = false;

    }
}
