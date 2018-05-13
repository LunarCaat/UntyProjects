using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerScript : MonoBehaviour {

    public static ObjectPoolerScript current;
    public GameObject pooledObject;
    public int pooledAmmount = 20;
    public bool willGrow = true;

    List<GameObject> pooledObjects;


    void Awake()
    {
        current = this;
    }
    // Use this for initialization
    void Start() {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmmount; i++)
        {
            GameObject newObj = (GameObject)Instantiate(pooledObject);
            newObj.SetActive(false);
            pooledObjects.Add(newObj);
        }

	}
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy) return pooledObjects[i];
        }

        if (willGrow)
        {
            GameObject newObj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(newObj);
            return newObj;
        }
        return null;
    }



}
