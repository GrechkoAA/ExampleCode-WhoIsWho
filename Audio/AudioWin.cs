using DG.Tweening;
using UnityEngine;

public class AudioWin : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private EndGame _endGame;

    private void Start()
    {
        _endGame = FindObjectOfType<EndGame>();
        _endGame.IsFinished += Play;
    }

    private void Play(bool isFinished)
    {
        if (isFinished == true)
        {
            DOTween.Sequence().AppendInterval(0.25f).AppendCallback(() => _audioSource.Play());
        }
    }
}
