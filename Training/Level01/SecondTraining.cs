using DG.Tweening;
using UnityEngine;

public class SecondTraining : MonoBehaviour
{
    [SerializeField] private GameObject _secondTraining;
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private RectTransform _finger;
    [SerializeField] private ChangeSkin _changeSkin;
    [SerializeField] private float _duration;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Outline _outlinePolicman;
    [SerializeField] private Collider _colliderPolicman;
    [SerializeField] private UnityEngine.Events.UnityEvent _nextedTraining;

    private Tween _move;
    private bool _isEndTraining = false;

    public event System.Action EndedFirstTraining;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _finger.position = _finger.position;
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
            _move.Kill();
            _outlinePolicman.OutlineWidth = 2;
            _colliderPolicman.enabled = true;
            _isEndTraining = true;
            _nextedTraining?.Invoke();
        };
    }
}