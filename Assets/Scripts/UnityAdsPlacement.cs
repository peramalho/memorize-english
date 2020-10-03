using UnityEngine.Monetization;
using UnityEngine;
using System.Collections;

public class UnityAdsPlacement : MonoBehaviour
{
    string gameId = "3002182";
    bool testMode = false;
    public string placementId = "LevelComplete";

    public void ShowAd()
    {
        StartCoroutine(ShowAdWhenReady());
    }

    private IEnumerator ShowAdWhenReady()
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
            Monetization.Initialize(gameId, testMode); //Ensure Ads is initialized
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }

    public void Start()
    {
        if (GameObject.FindObjectOfType<UnityAds>().TriggerAdsCount >= 3)
        {
            ShowAd(); //Show Ads at Count in 3
            GameObject.FindObjectOfType<UnityAds>().TriggerAdsCount = 0;
        }
    }

}
