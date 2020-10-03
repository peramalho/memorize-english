using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	public bool isPaused = false;
	public GameObject spawner;

	[SerializeField] private Sprite[] icon;
	[SerializeField] private GameObject pausePanel;
	[SerializeField] private GameObject pausesRemaining;
    [SerializeField] private GameObject volumeButton;
    [SerializeField] private GameObject lightningBar;
    [SerializeField] private GameObject lightningPanel;

    private AudioManager audioManager;
	private int pausesCount = 5;

	public void Pause(){
		GetComponent<Image> ().sprite = icon [0];
		audioManager.PlayButtonSound ();

		if(!isPaused){
			Time.timeScale = 0;
			isPaused = true;
            audioManager.MuteSound();
            volumeButton.GetComponent<Button>().interactable = false;

            GetComponent<Image> ().sprite = icon [1];

            Vector3 temp = new Vector3(0, 1000, 0);
            spawner.transform.Translate(temp);
            lightningBar.transform.Translate(temp);
            lightningPanel.transform.Translate(temp);

            pausePanel.SetActive (true);
			pausesCount--;
			pausesRemaining.GetComponent<Text> ().text = pausesCount + " pauses remaining";
            AudioListener.pause = true;
        }

		else{
			Time.timeScale = 1;
			isPaused = false;
            if (PlayerPrefs.GetString("isMuted") == "false") audioManager.UnMuteSound();

            volumeButton.GetComponent<Button>().interactable = true;

            GetComponent<Image> ().sprite = icon [0];

            Vector3 temp = new Vector3(0, -1000, 0);
            spawner.transform.Translate(temp);
            lightningBar.transform.Translate(temp);
            lightningPanel.transform.Translate(temp);

            pausePanel.SetActive (false);
			if (pausesCount <= 0) Object.Destroy(gameObject);
            AudioListener.pause = false;
        }
	}

	private void OnSpawn(){
		spawner = GameObject.FindGameObjectWithTag ("Spawner");
	}

	void Awake(){
		Messenger.AddListener (GameEvent.SPAWNED, OnSpawn);
		audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

	void OnDestroy(){
		Messenger.RemoveListener (GameEvent.SPAWNED, OnSpawn);
	}
		
	public void OnApplicationPause(bool pauseStatus){

		if (!isPaused) Pause();

	}

}
