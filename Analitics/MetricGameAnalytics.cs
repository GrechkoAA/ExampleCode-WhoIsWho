using GameAnalyticsSDK;
using UnityEngine;

public class MetricGameAnalytics : MonoBehaviour
{
    private void Awake()
    {
        GameAnalytics.Initialize();
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("FirstAppLaunch"))
        {
            GameAnalytics.NewDesignEvent("FirstAppLaunch");
            PlayerPrefs.SetInt("FirstAppLaunch", 1);
        }

        GameAnalytics.NewDesignEvent("AppLaunch");
    }
}