using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayGamesScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        
        //GooglePlayGames.PlayGamesPlatform.Activate();

        //Social.localUser.Authenticate((bool success) => { });

        SignIn();
        
	}

    void SignIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, int score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
        //Social.ReportScore(score, leaderboardId, (bool success) => { });
    }

    public static void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }

    #endregion /Leaderboards

}
