using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour {

	[SerializeField] private GameObject resetPanel;
	[SerializeField] private GameObject confirmationPanel;
	[SerializeField] private HighestScore highestScore;
	private AudioManager audioManager;

	public void ResetConfirmation(){

		resetPanel.SetActive (false);
		confirmationPanel.SetActive (true);
		audioManager.PlayButtonSound ();

	}

	public void YesButton(){

		highestScore.ResetHighestScore ();
		resetPanel.SetActive (true);
		confirmationPanel.SetActive (false);
		audioManager.PlayButtonSound ();
        PlayerPrefs.SetInt("highestRank", 0);

    }

	public void NoButton(){

		resetPanel.SetActive (true);
		confirmationPanel.SetActive (false);
		audioManager.PlayButtonSound ();

	}

	void Awake(){

		audioManager = GameObject.FindObjectOfType<AudioManager>();

	}

}
