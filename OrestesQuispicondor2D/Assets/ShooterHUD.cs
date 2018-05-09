using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterHUD : MonoBehaviour {

    public TopDownMovementWithRigidBody player;
    Text debugText;
    public float spacing =75;
    Transform collection;
    public GameObject weaponPrefab;
    int lastColorIndex;

	void Start () {
        debugText= transform.Find("DebugText").GetComponent<Text>();
        collection = transform.Find("WeaponCollection");
        for (int i =0; i<player.colors.Count;i++)
        {
            Instantiate(weaponPrefab,collection).GetComponent<Image>().color = player.colors[i];
            collection.GetChild(i).localPosition= new Vector3 (spacing*i,0,0);
        }
        lastColorIndex = player.ColorIndex;
    }
	
	// Update is called once per frame
	void Update () {
        //debugText.text = "Weapon Index = "+player.ColorIndex;
        
        if (lastColorIndex!= player.ColorIndex)
        {
            for (int i =0;i<collection.childCount;i++)
            {
                float targetSize = (i == player.ColorIndex) ? 50 : 30;
                collection.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(targetSize, targetSize);
            }
            Debug.Log("Weapon Changed!");
        }
        lastColorIndex = player.ColorIndex;
        //Transform targetObject = transform.Find("WeaponCollection").GetChild(0);
        //targetObject.GetComponent<RectTransform>().position = new Vector3(25,25,0);
        //debugText.text = targetObject.name;

        //debugText.text = collection.GetChild(player.ColorIndex).GetComponent<RectTransform>().sizeDelta.ToString();

    }
    //void LateUpdate()
    //{
    //    lastColorIndex = player.ColorIndex;
    //}
}
