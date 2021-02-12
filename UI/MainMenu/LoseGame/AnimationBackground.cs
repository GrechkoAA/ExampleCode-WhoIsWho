using DG.Tweening;
using UnityEngine;

public class AnimationBackground : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _background;
    [SerializeField] private EndGameUI _endGame;

    public void OnPlay(bool isFInished)
    {
        if (isFInished == true)
        {
            _background.DOFade(0.5f, 0.3f);
        }
    }

    private void OnEnable()
    {
        _endGame.IsFinished += OnPlay;
    }
}