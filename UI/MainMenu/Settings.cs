using DG.Tweening;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private RectTransform _panelContex;
    [SerializeField] private Ease _easy;
    [SerializeField] private float _delay;

    private bool isOpen;

    public void Open()
    {
        if (DOTween.IsTweening(_panelContex) == false)
        {
            _panelContex.DOAnchorPosY(isOpen == false ? 64.41f : 407f, _delay, true).SetEase(_easy);

            isOpen = !isOpen; 
        }
    }
}