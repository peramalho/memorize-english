using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	[SerializeField] private Text scoreText;
    [SerializeField] private GemIcon gemIcon;
	[SerializeField] private GameObject scoreAnimation;
	[SerializeField] private Canvas canvas;
	[SerializeField] private Manager manager;
	[SerializeField] private Color32[] floatingColor;

	private int score = 0;
	private Color32 color;

	void Start () {
		ResetScore ();
        gemIcon.SetBlueGem();

	}

	public void UpScore (Image panel, Image panelAux){
		score = score + GetScoreValue(manager.GetMatchSequence());
		WriteScore (score);
        CheckGemColor();

		GameObject floatingScore1 = Instantiate(scoreAnimation) as GameObject;
		floatingScore1.transform.SetParent(canvas.transform, false);
		Vector3 pos1 = panel.transform.position;
		floatingScore1.transform.position = pos1;
		floatingScore1.transform.SetSiblingIndex (6);

		floatingScore1.GetComponentInChildren<Text> ().text = "+" + GetScoreValue(manager.GetMatchSequence());
		floatingScore1.GetComponentInChildren<Text> ().color = color;

		GameObject floatingScore2 = Instantiate(scoreAnimation) as GameObject;
		floatingScore2.transform.SetParent(canvas.transform, false);
		Vector3 pos2 = panelAux.transform.position;
		floatingScore2.transform.position = pos2;
		floatingScore2.transform.SetSiblingIndex (6);

		floatingScore2.GetComponentInChildren<Text> ().text = "+" + GetScoreValue(manager.GetMatchSequence());
		floatingScore2.GetComponentInChildren<Text> ().color = color;

		scoreText.GetComponent<Animator> ().SetTrigger ("isScored");

	}

    private void CheckGemColor()
    {
        if (score >= 300 && gemIcon.GetColor() == GemIcon.GemIconState.Blue) gemIcon.SetGreenGem(); //300

        if (score >= 800 && gemIcon.GetColor() == GemIcon.GemIconState.Green) gemIcon.SetYellowGem(); //700

        if (score >= 2000 && gemIcon.GetColor() == GemIcon.GemIconState.Yellow) gemIcon.SetRedGem();

        if (score >= 4000 && gemIcon.GetColor() == GemIcon.GemIconState.Red) gemIcon.SetPurpleGem();

        if (score >= 7500 && gemIcon.GetColor() == GemIcon.GemIconState.Purple) gemIcon.SetGrayGem();

    }

    public int GetScoreValue (int matchSequence){

		int score = 0;

		if (matchSequence <= 5) {
			score = 10;
			color = floatingColor [0];
		} else if (matchSequence >= 6 && matchSequence <= 15) {
			score = 20;
			color = floatingColor [1];
		} else if (matchSequence >= 16) {
			score = 40;
			color = floatingColor [2];
		}

		return score;
	}

	public void ResetScore(){
		score = 0;
		WriteScore (score);
	}

	//Change score int to string and print it
	public void WriteScore(int valor){
		scoreText.text = valor.ToString();
	}

	public void OnDestroy (){
		PlayerPrefs.SetInt("finalScore", score);
        PlayerPrefs.SetInt("currentRank", gemIcon.GetRank());

		//First time playing
		if(!PlayerPrefs.HasKey("highestScore")){
			
			PlayerPrefs.SetInt("highestScore", score);

            PlayGamesScript.AddScoreToLeaderboard(Scoreboard.leaderboard_maiores_scores, score);
            PlayerPrefs.SetInt("highestRank", gemIcon.GetRank()); //Set rank first time

		}

        if (PlayerPrefs.HasKey("highestScore") && score > PlayerPrefs.GetInt("highestScore"))
        {
				
			PlayerPrefs.SetInt("highestScore", score);

            PlayGamesScript.AddScoreToLeaderboard(Scoreboard.leaderboard_maiores_scores, score);

        }

        if (PlayerPrefs.HasKey("highestRank") && gemIcon.GetRank() > PlayerPrefs.GetInt("highestRank"))
        {

            PlayerPrefs.SetInt("highestRank", gemIcon.GetRank());

        }

    }

}
