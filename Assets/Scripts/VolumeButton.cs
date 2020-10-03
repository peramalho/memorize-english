using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour {

	public bool isMuted = false;

	[SerializeField] private Sprite[] icon;

	private AudioManager audioManager;

	public void Mute(){

		GetComponent<Image> ().sprite = icon [0];

		if(PlayerPrefs.GetString ("isMuted") == "false"){
			GetComponent<Image> ().sprite = icon [1];
			PlayerPrefs.SetString("isMuted", "true");
			audioManager.MuteSound ();
		}

		else if (PlayerPrefs.GetString ("isMuted") == "true") {
			GetComponent<Image> ().sprite = icon [0];
			PlayerPrefs.SetString("isMuted", "false");
			audioManager.UnMuteSound ();
			audioManager.PlayButtonSound ();
		}
	}

	void Awake(){

		audioManager = GameObject.FindObjectOfType<AudioManager>();

		if (!PlayerPrefs.HasKey ("isMuted")){
			PlayerPrefs.SetString ("isMuted", "false");
		}

		if (PlayerPrefs.GetString ("isMuted") == "false") {
			GetComponent<Image> ().sprite = icon [0];
			audioManager.UnMuteSound ();
		}

		else if (PlayerPrefs.GetString ("isMuted") == "true") {
			GetComponent<Image> ().sprite = icon [1];
			audioManager.MuteSound ();
		}

	}

}
