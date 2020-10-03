using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighestScore : MonoBehaviour {

    private Text highestScoreText;

    // Use this for initialization
    void Start () {
        highestScoreText = GetComponent<Text>();
        highestScoreText.text = "Best Score: " + PlayerPrefs.GetInt ("highestScore").ToString();
    }

	public void ResetHighestScore () {
		PlayerPrefs.SetInt("highestScore", 0);
		highestScoreText = GetComponent<Text>();
		highestScoreText.text = "Best Score: " + PlayerPrefs.GetInt ("highestScore").ToString();
	}
}
