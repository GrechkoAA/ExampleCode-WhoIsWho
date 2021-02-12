using DG.Tweening;
using UnityEngine;

public class SceneAnimation : MonoBehaviour
{
    [SerializeField] private Animator[] _animators;
    [SerializeField] private float _interval;

    private EndGame _endGame;

    private void Awake()
    {
        _endGame = FindObjectOfType<EndGame>();
    }

    public void Play(bool isFinished)
    {
        if (isFinished)
        {
            DOTween.Sequence().AppendInterval(_interval).AppendCallback(()=> 
            {
                foreach (var animator in _animators)
                {
                    animator.SetTrigger("Play");
                }
            });
        }
    }

    private void OnEnable()
    {
        _endGame.IsFinished += Play;
    }
}