using UnityEngine;

public class FingerZoom : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private Sprite _zoomeIcon;
    [SerializeField] private Sprite _reduceIcon;
    [SerializeField] private UnityEngine.UI.Image _currentIcon;
    [SerializeField] private float _interval;

    private float _zoom;

    private void Start()
    {
        _currentIcon.sprite = _zoomeIcon;
    }

    private void Update()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _interval += Time.deltaTime;
        _zoom += Time.deltaTime / 5f;

        if (_interval > 1)
        {
            _zoom = 0;
            _interval = 0;
            _currentIcon.sprite = _currentIcon.sprite == _zoomeIcon ? _reduceIcon : _zoomeIcon;
        }

        _camera.fieldOfView += _currentIcon.sprite == _zoomeIcon ? _zoom : -_zoom;
    }

    private void OnEnable()
    {
        _touchpad.SelectedSkin += (icon, position) =>
        {
            gameObject.SetActive(false);
        };
    }
}