using DG.Tweening;
using UnityEngine;

public class CameraStartGame : MonoBehaviour
{
    [SerializeField] private Vector3 _originTransform;
    [SerializeField] private Vector3 _originRotation;
    [SerializeField] private float _duration;
    [SerializeField] private UnityEngine.Events.UnityEvent Started;

    public void StarGame()
    {
        transform.DOMove(_originTransform, _duration).SetEase(Ease.Linear);
        transform.DORotate(_originRotation, _duration).SetEase(Ease.Linear);
        DOTween.Sequence().AppendInterval(_duration).AppendCallback(() => Started?.Invoke());
    }
}