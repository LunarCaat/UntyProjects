using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterHUD : MonoBehaviour {

    public TopDownMovementWithRigidBody player;
    Text debugText;
    Text gameOverText;
    public float spacing =75;
    Transform collection;
    public GameObject weaponPrefab;
    int lastColorIndex;

    int health;

	void Start () {
        debugText= transform.Find("DebugText").GetComponent<Text>();
        gameOverText = transform.Find("GameOverText").GetComponent<Text>();
        collection = transform.Find("WeaponCollection");
        for (int i =0; i<player.colors.Count;i++)
        {
            Instantiate(weaponPrefab,collection).GetComponent<Image>().color = player.colors[i];
            collection.GetChild(i).localPosition= new Vector3 (spacing*i,0,0);
        }
        lastColorIndex = player.ColorIndex;
        health = player.health;
        debugText.text= "Player health : "+ health;
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


        health = player.health;
        debugText.text= "Player health : "+ health;
        //Transform targetObject = transform.Find("WeaponCollection").GetChild(0);
        //targetObject.GetComponent<RectTransform>().position = new Vector3(25,25,0);
        //debugText.text = targetObject.name;

        //debugText.text = collection.GetChild(player.ColorIndex).GetComponent<RectTransform>().sizeDelta.ToString();

        if (TopDownMovementWithRigidBody.gameOver && !gameOverText.gameObject.activeInHierarchy)
        {
            gameOverText.text = "Try again!";
            gameOverText.gameObject.SetActive(true);
        }
        else if (TopDownMovementWithRigidBody.winCondition && !gameOverText.gameObject.activeInHierarchy)
        {
            gameOverText.text = "You did it!";
            gameOverText.gameObject.SetActive(true);
        }

    }
    //void LateUpdate()
    //{
    //    lastColorIndex = player.ColorIndex;
    //}
}
