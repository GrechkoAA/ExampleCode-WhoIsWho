using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Touchpad _touchpad;
    [SerializeField, Range(0f, 1f)] float _rotationalSpeed;

    private float _rotationClamp = 900000;
    private float _currentHorizontalPosition;
    private float _horizontalDistance;

    private void RotateHorizontally(Vector2 position)
    {
        float _nomralizeClamp = _rotationClamp / _rotationalSpeed;
        _currentHorizontalPosition = Mathf.Clamp(position.x + _horizontalDistance, -_nomralizeClamp, _nomralizeClamp);

        if (_currentHorizontalPosition >= (_nomralizeClamp) || _currentHorizontalPosition <= -(_nomralizeClamp))
        {
            _horizontalDistance = _currentHorizontalPosition - position.x;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, _currentHorizontalPosition * _rotationalSpeed, 0));
    }

    private void OnEnable()
    {
        _touchpad.Pressed += (position) => _horizontalDistance = _currentHorizontalPosition - position.x;
        _touchpad.Dragged += RotateHorizontally;
        _touchpad.Released += (position) => _horizontalDistance = _currentHorizontalPosition - position.x;
    }
}