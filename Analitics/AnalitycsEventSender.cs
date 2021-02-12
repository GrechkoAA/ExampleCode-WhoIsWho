using GameAnalyticsSDK;
using UnityEngine;

public class AnalitycsEventSender : MonoBehaviour
{
    [SerializeField] private EndGame _endGame;
    [SerializeField] private SaveSystem _saveSystem;

    private string _progression01;

    private void Start()
    {
        _progression01 = $"{_saveSystem.LevelName}_{_saveSystem.LevelNumber}";

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, _progression01);
    }

    private void OnEnable()
    {
        _endGame.IsFinished += (isFinished) =>
        {
            if (isFinished)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _progression01);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, _progression01);
            }
        };
    }
}