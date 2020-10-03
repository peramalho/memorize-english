using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LeaderboardButton : MonoBehaviour
{
    public void ShowLeaderboardUI()
    {
        //Social.ShowLeaderboardUI();
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI2f-KnOMFEAIQAA");
        //TODO Loading toast
    }
}
