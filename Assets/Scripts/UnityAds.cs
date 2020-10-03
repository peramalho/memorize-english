using UnityEngine;
using UnityEngine.Monetization;

public class UnityAds : MonoBehaviour
{
    string gameId = "3002182";
    bool testMode = false;
    private int triggerAdsCount = 0;

    public int TriggerAdsCount
    {
        get
        {
            return triggerAdsCount;
        }

        set
        {
            triggerAdsCount = value;
        }
    }

    void Start()
    {
        Monetization.Initialize(gameId, testMode);
    }

}
