using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    static public UIManager instance;


    public Image hpBar;
    public Gradient barColors;
    float hp;
    public PlayerScript playerScript;
    public Image restartPanel;
    public float restartSpeed = 3.5f;
    bool isLoading = false;



	private void Awake()
	{
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            DestroyImmediate(gameObject);
        }
	}


	// Use this for initialization
	void Start () {
        hpBar.fillAmount = playerScript.normalizedHP;
        if(playerScript == null){
            playerScript = FindPlayerInstance();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(playerScript!=null && hpBar.fillAmount!=playerScript.normalizedHP){
            float delta = Mathf.Abs(hpBar.fillAmount - playerScript.normalizedHP);
            if (delta < 0.2f) { delta = 0.2f; }
            hpBar.fillAmount = Mathf.MoveTowards(hpBar.fillAmount,playerScript.normalizedHP,delta * Time.deltaTime);
            hpBar.color = barColors.Evaluate(hpBar.fillAmount);
        }
        if(!isLoading &&Input.GetKeyDown(KeyCode.T)){
            isLoading = true;
            StartCoroutine(RestartProcess());
        }
	}



    IEnumerator RestartProcess(){
        restartPanel.gameObject.SetActive(true);
        while(restartPanel.color.a != 1){
            restartPanel.color = FadeColorAlpha(restartPanel.color, 1, restartSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Here0");
        playerScript = null;
        Debug.Log("Here1");
        AsyncOperation operation =
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);


        while(!operation.isDone){
            yield return null;
        }

        playerScript = FindPlayerInstance();
        while (restartPanel.color.a != 0)
        {
            restartPanel.color = FadeColorAlpha(restartPanel.color, 0, restartSpeed * Time.deltaTime);
            yield return null;
        }
        isLoading = false;
        yield return null;
    }

    Color FadeColorAlpha(Color currentColor,float targetAlpha, float fadedPace){
        return new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.MoveTowards(currentColor.a, targetAlpha, fadedPace));

    }

    PlayerScript FindPlayerInstance(){
        return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
}
