using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour {
    public EnemyEntity enemyEntity;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyEntity.TriggerEnterCall(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyEntity.TriggerExitCall(other.gameObject);
        }
    }
}
