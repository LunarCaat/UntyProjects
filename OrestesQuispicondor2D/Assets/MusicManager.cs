using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public static MusicManager current;
    public AudioClip firstStage;
    public AudioClip bossStage;
    private AudioSource source;
    // Use this for initialization
    void Awake()
    {
        current = this;
    }
    void Start () {
        source = GetComponent<AudioSource>();
        playFirstStage();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playFirstStage()
    {
        playMusic(firstStage);
    }
    public void playBossStage()
    {
        playMusic(bossStage);
    }


    public void playMusic(AudioClip thisClip)
    {
        source.loop = true;
        source.clip = thisClip;
        source.Play();
    }

}
