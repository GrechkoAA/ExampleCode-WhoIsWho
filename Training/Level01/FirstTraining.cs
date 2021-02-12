using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class FirstTraining : MonoBehaviour
{
    [SerializeField] private GameObject _firstTraining;
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private RectTransform _finger;
    [SerializeField] private ChangeSkin _changeSkin;
    [SerializeField] private float _duration;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private UnityEngine.UI.Image[] _iconPolice;
    [SerializeField] private Collider _colliderPolice;
    [SerializeField] private Outline _outlinerPolice;
    [SerializeField] private Collider _colliderThief;
    [SerializeField] private Outline _outlinerThief;
    [SerializeField] private UnityEvent _nextedTraining;

    private Tween _move;
    private bool _isEndTraining = false;

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

            _outlinerPolice.OutlineWidth = 0;
            _outlinerThief.OutlineWidth = 2;

            _isEndTraining = true;
            _move.Kill();
            _firstTraining.SetActive(false);

            _iconPolice[0].DOFade(1, 0.5f);
            _iconPolice[1].DOFade(1, 0.5f);

            _iconPolice[0].raycastTarget = true;
            _colliderPolice.enabled = false;
            _colliderThief.enabled = true;

            _nextedTraining?.Invoke();
        }; 
    }
}