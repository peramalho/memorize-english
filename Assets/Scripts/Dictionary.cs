using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dictionary : MonoBehaviour {

	public int lenght;

	private TextAsset wordsEnglishAsset;
	private TextAsset wordsPortugueseAsset;
	private string[] wordsEnglish;
	private string[] wordsPortuguese;

    private void Awake() {
        Application.targetFrameRate = 60;

        if (!PlayerPrefs.HasKey("highestRank"))
        {

            PlayerPrefs.SetInt("highestRank", 1); //Set rank first time

        }

        wordsEnglishAsset = Resources.Load ("wordsEnglish") as TextAsset;
		wordsPortugueseAsset = Resources.Load ("wordsPortuguese") as TextAsset;

		wordsEnglish = wordsEnglishAsset.text.Split ('\n');
		wordsPortuguese = wordsPortugueseAsset.text.Split ('\n');

        lenght = wordsEnglish.Length;

		int numGameSessions = FindObjectsOfType<Dictionary>().Length;
		if (numGameSessions > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}

    }

	private void Start (){

		if(!PlayerPrefs.HasKey("isFirstTime")) {
			PlayerPrefs.SetString ("isFirstTime", "false");
			SceneManager.LoadScene(3);
		}
	}
	//Returns a binary word array english-portuguese
	public string[] GetWordPairs() {
		int rng = Random.Range (0, lenght);
		string[] wordPair = new string[] { wordsEnglish [rng], wordsPortuguese [rng] };
		return wordPair;
	}
    
}
