using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluationManager : MonoBehaviour
{
    public GameObject evaluationPanel;

    // Start is called before the first frame update
    void Start()
    {
        //First time playing
        if (!PlayerPrefs.HasKey("gamesPlayed"))
        {
            PlayerPrefs.SetInt("gamesPlayed", 0);
        }

        //Plus 1 to games played
        if (PlayerPrefs.GetInt("gamesPlayed") <= 15)
        {
            PlayerPrefs.SetInt("gamesPlayed", PlayerPrefs.GetInt("gamesPlayed") + 1);
        }

        //Show Panel
        if (PlayerPrefs.GetInt("gamesPlayed") == 2)
        {
            evaluationPanel.SetActive(true);
        }
        if (PlayerPrefs.GetInt("gamesPlayed") == 15)
        {
            evaluationPanel.SetActive(true);
        }

    }

    public void GoEvaluate()
    {
        Application.OpenURL("market://details?id=com.dezivo.memorizeenglish");
        PlayerPrefs.SetInt("gamesPlayed", 20);
        evaluationPanel.SetActive(false);
    }

    public void Cancel()
    {
        evaluationPanel.SetActive(false);
    }

}
