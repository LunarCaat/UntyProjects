using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    
    private bool hasActivated = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player")&&!hasActivated)
        {
            hasActivated = true;
            MusicManager.current.playBossStage();

        }


    }
}
