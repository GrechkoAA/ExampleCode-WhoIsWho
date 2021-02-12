using DG.Tweening;
using UnityEngine;

public class FingerMovement : MonoBehaviour
{
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private RectTransform _finger;
    [SerializeField] private float _duration;
    [SerializeField] private Transform[] _target;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _finger.position = GetStartedPosition(_target[0]);
        Vector3 endPosition = GetStartedPosition(_target[1]);
        _finger.DOMove(endPosition, _duration).SetLoops(-1);
    }

    private Vector3 GetStartedPosition(Transform target)
    {
        return target.CompareTag("Player") == false ? target.position : Camera.main.WorldToScreenPoint(target.position);
    }

    private void OnEnable()
    {
        _touchpad.SelectedSkin += (icon, position) =>
        {
            gameObject.SetActive(false);
        };
    }
}