using DG.Tweening;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private EndGame _endGame;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _winGame;
    [SerializeField] private UnityEngine.Events.UnityEvent Ended;
    [SerializeField] private float _intervalWin;
    [SerializeField] private float _intervalOver = 0.2f;

    public event System.Action<bool> IsFinished; 

    private void OnEnable()
    {
        _endGame.IsFinished += (isFinished) =>
        {
            if (isFinished == true)
            {
                Ended?.Invoke();

                DOTween.Sequence().AppendInterval(_intervalWin).AppendCallback(() =>
                {
                    _winGame.SetActive(true);
                    IsFinished?.Invoke(true);
                });
            }
        };
    }
}