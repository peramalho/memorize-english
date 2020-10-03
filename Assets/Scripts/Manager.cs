using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	[SerializeField] private int currentLevel = 1;
	[SerializeField] private int matchSequence = 0;
	[SerializeField] private GameObject spawnFormation;
	[SerializeField] private Canvas canvas;
	[SerializeField] private ScoreManager scoreManager;
	[SerializeField] private GameObject pauseButton;

	public bool isLevelSelected = false;

    void Update () {

        if (!isLevelSelected)
        {
			GameObject spawn = Instantiate(spawnFormation) as GameObject;
            spawn.transform.SetParent(canvas.transform, false);
			spawn.transform.SetSiblingIndex (5); //Move In Hierarchy
            isLevelSelected = true;
        }
			
    }
		
	public void OnLevelCompleted(){
		currentLevel++;
		isLevelSelected = false;
	}

	public void OnGameLost(){
		SceneManager.LoadScene(2);
	}

	public int GetCurrentLevel (){
		return currentLevel;
	}

	public void IncreaseMatchSequence (){
		matchSequence++;
	}

	public void ResetMatchSequence (){
		matchSequence = 0;
	}

	public int GetMatchSequence (){
		return matchSequence;
	}
		
	public void OnApplicationPause(bool pauseStatus){

		if (pauseButton == null)	OnGameLost ();

	}
		
}
