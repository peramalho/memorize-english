using UnityEngine;
using UnityEngine.UI;

public class FinalScoreText : MonoBehaviour {

    [SerializeField] private GemIcon currentGemIcon;

    private Text finalScoreText;

	// Use this for initialization
	void Start () {
        finalScoreText = GetComponent<Text>();
        finalScoreText.text = "Score: " + PlayerPrefs.GetInt ("finalScore").ToString();

        currentGemIcon.LoadCurrentRank();
	}

}
