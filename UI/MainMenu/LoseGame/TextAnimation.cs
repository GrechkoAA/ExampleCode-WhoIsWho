using DG.Tweening;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _text;
    [SerializeField] private float _endPosition;
    [SerializeField] private float _duration;
    private void Start()
    {
        //OnText();
    }

    public void OnText()
    {
        _text.DOAnchorPosY(_endPosition, _duration);
    }
}