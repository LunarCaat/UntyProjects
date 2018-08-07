using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    static public UIManager instance;

    public Image hpBar;
    public PlayerEntity playerScript;
    public Gradient barColors;
    public Image restartPanel;
    public float restartSpeed;

    bool isLoading = false;
	
	
	
	public enum GameState { NOT_FINISHED, GAMEOVER,SUCCESS};
    public static GameState state;
    public GameObject gameOverUI;
    public GameObject successUI;
    public Text successText;
    private bool finished=false;
	
	
	public Transform collection;
    public GameObject[] weaponPrefab;
	int lastColorIndex;
	public float spacing =75;
	public MonsterMovement25D playerMovement;

    // public CamBehaviour camBehaviour;
    // CamBehaviour.CamData defaultData;

    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            DestroyImmediate (gameObject);
        }
        
    }

	// Use this for initialization
	void Start () {
        if (playerScript == null) {
            InitGameScripts ();
        }
		
		for (int i =0; i<weaponPrefab.Length;i++)
        {
            Instantiate(weaponPrefab[i],collection);
            collection.GetChild(i).localPosition= new Vector3 (spacing*i,0,0);
        }
		lastColorIndex = playerMovement.ColorIndex;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!finished) {
            if (state == GameState.GAMEOVER)
            {
                gameOverResult();
            }
            if (state == GameState.SUCCESS)
            {
                successResult();
            }
		}
        if (playerScript != null && hpBar.fillAmount != playerScript.normalizedHP) {
            float delta = Mathf.Abs (hpBar.fillAmount - playerScript.normalizedHP);
            if (delta < 0.2f) { delta = 0.2f; }
            hpBar.fillAmount = Mathf.MoveTowards (hpBar.fillAmount, playerScript.normalizedHP, 2f * delta * Time.deltaTime);
            hpBar.color = barColors.Evaluate (hpBar.fillAmount);
        }
		
		
		
		if (lastColorIndex!= playerMovement.ColorIndex)
        {
            for (int i =0;i<collection.childCount;i++)
            {
                float targetSize = (i == playerMovement.ColorIndex) ? 50 : 30;
                collection.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(targetSize, targetSize);
            }
            Debug.Log("Weapon Changed!");
        }
        lastColorIndex = playerMovement.ColorIndex;

		
		
        // if (!isLoading && Input.GetKeyDown (KeyCode.T)) {
            // isLoading = true;
            // StartCoroutine (RestartProcess ());
        // }
		
		
        /*if (!isLoading && Input.GetKeyDown (KeyCode.M)) {
            if (camBehaviour.GetCamData ().CompareValues (defaultData)) {
                camBehaviour.SetCamData (new CamBehaviour.CamData (17f, Vector3.up * 25f, GameObject.FindWithTag ("Player").transform, true)); //GameObject.Find ("Platform_Base").transform));
            } else {
                camBehaviour.SetCamData (defaultData);
            }
        }*/
        // if (!isLoading && Input.GetKeyDown (KeyCode.N)) {
            // camBehaviour.SetCamData (new CamBehaviour.CamData (20f, new Vector3 (1f, 2f, -4.75f), defaultData.target.Find ("LongTarget")));
            // playerScript.currentPower.SetAlpha (0.25f);
            // playerScript.SetSightMode (true);
        // } else if (!isLoading && Input.GetKeyUp (KeyCode.N)) {
            // camBehaviour.SetCamData (defaultData);
            // playerScript.currentPower.SetAlpha (1f);
            // playerScript.SetSightMode (false);
        // }
	}

    IEnumerator RestartProcess () {
        restartPanel.gameObject.SetActive (true);
        while (restartPanel.color.a != 1) {
            restartPanel.color = FadeColorAlpha (restartPanel.color, 1, restartSpeed * Time.deltaTime);
            yield return null;
        }

        playerScript = null;
        AsyncOperation operation = SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);

        while (!operation.isDone) {
            yield return null;
        }

        InitGameScripts ();

        while (restartPanel.color.a != 0) {
            restartPanel.color = FadeColorAlpha (restartPanel.color, 0, restartSpeed * Time.deltaTime);
            yield return null;
        }
        isLoading = false;
        yield return null;
    }

    Color FadeColorAlpha (Color currentColor, float targetAlpha, float fadePace) {
        return new Color (currentColor.r, currentColor.g, currentColor.b, Mathf.MoveTowards (currentColor.a, targetAlpha, fadePace));
    }

    PlayerEntity FindPlayerInstance () {
        return GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerEntity> ();
    }

    void InitGameScripts () {
        playerScript = FindPlayerInstance ();
		
		
        //camBehaviour = Camera.main.GetComponent<CamBehaviour> ();
        //defaultData = camBehaviour.GetCamData ();
    }
	
	void gameOverResult()
    {
        showGameOverScreen();
        finished = true;
    }

    void showGameOverScreen()
    {
        gameOverUI.SetActive(true);
    }

    public void resetGame()
    {
        SceneManager.LoadScene("MainMenu");
        resetState();
    }
    void successResult()
    {
        showSuccessScreen();
        finished = true;
    }
    void showSuccessScreen()
    {
        
        successText.text = "Congratulations "+DataManager.instance.currentData.playerName+"!";
        //timerText.enabled =false;
        successUI.SetActive(true);
    }
    void resetState()
    {
        state = GameState.NOT_FINISHED;
    }
	
	
}
