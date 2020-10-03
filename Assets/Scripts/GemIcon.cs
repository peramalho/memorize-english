using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GemIcon : MonoBehaviour {

    [SerializeField] Animator animator;

    public enum GemIconState { Blue = 1, Green = 2, Yellow = 3, Red = 4, Purple = 5, Gray = 6 }
    private GemIconState gemColor;

    public void SetBlueGem()
    {
        animator.SetTrigger("BlueGem");
        gemColor = GemIconState.Blue;
    }

    public void SetGreenGem()
    {
        animator.SetTrigger("GreenGem");
        gemColor = GemIconState.Green;
    }

    public void SetYellowGem()
    {
        animator.SetTrigger("YellowGem");
        gemColor = GemIconState.Yellow;
    }

    public void SetRedGem()
    {
        animator.SetTrigger("RedGem");
        gemColor = GemIconState.Red;
    }

    public void SetPurpleGem()
    {
        animator.SetTrigger("PurpleGem");
        gemColor = GemIconState.Purple;
    }
    public void SetGrayGem()
    {
        animator.SetTrigger("GrayGem");
        gemColor = GemIconState.Gray;
    }

    public void ResetGem()
    {
        animator.SetTrigger("Reset");
        gemColor = GemIconState.Blue;
        PlayerPrefs.SetInt("highestRank", 0);
    }

    public GemIconState GetColor()
    {
        return gemColor;
    }

    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0) LoadHighestRank();
        if (scene.buildIndex == 2) LoadCurrentRank();
        if (scene.buildIndex == 1 || scene.buildIndex == 3) SetBlueGem();
    }

    public int GetRank()
    {
        return (int)gemColor;
    }

    public void LoadCurrentRank()
    {
        int gemRank = PlayerPrefs.GetInt("currentRank");
        switch (gemRank)
        {
            case 1: SetBlueGem(); break;
            case 2: SetGreenGem(); break;
            case 3: SetYellowGem(); break;
            case 4: SetRedGem(); break;
            case 5: SetPurpleGem(); break;
            case 6: SetGrayGem(); break;
        }
    }

    public void LoadHighestRank()
    {
        int gemRank = PlayerPrefs.GetInt("highestRank");
        switch (gemRank)
        {
            case 1: SetBlueGem(); break;
            case 2: SetGreenGem(); break;
            case 3: SetYellowGem(); break;
            case 4: SetRedGem(); break;
            case 5: SetPurpleGem(); break;
            case 6: SetGrayGem(); break;
        }
    }

}
