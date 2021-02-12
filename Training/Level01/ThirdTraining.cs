using DG.Tweening;
using UnityEngine;

public class ThirdTraining : MonoBehaviour
{
    [SerializeField] private GameObject _firstTraining;
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private RectTransform _finger;
    [SerializeField] private ChangeSkin _changeSkin;
    [SerializeField] private float _duration;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    private Tween _move;
    private bool _isEndTraining = false;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _finger.position = Camera.main.WorldToScreenPoint(_startPoint.position);
        Vector3 endPosition = Camera.main.WorldToScreenPoint(_endPoint.position);
        _move = _finger.DOMove(endPosition, _duration).SetLoops(-1);
    }

    private void OnEnable()
    {
        _touchpad.SelectedSkin += (icon, position) =>
        {
            gameObject.SetActive(false);
        };

        _touchpad.ReleasedTraining += () =>
        {
            if (_isEndTraining == true)
                return;

            gameObject.SetActive(true);
            _move.Restart();
            PlayAnimation();
        };

        _changeSkin.DressedUp += () =>
        {
            if (_isEndTraining == true)
                return;

            _isEndTraining = true;
            _move.Kill();
            _firstTraining.SetActive(false);
        };
    }
}