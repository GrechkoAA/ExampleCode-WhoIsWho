using UnityEngine;

public class EventZoom : MonoBehaviour
{
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private FingerZoom _fingerZoom;
    [SerializeField] private GameObject _Icons;

    private void StartFingerZomm()
    {
        _fingerZoom.gameObject.SetActive(true);
        _touchpad.ReleasedTraining -= StartFingerZomm;

        _touchpad.ReleasedTraining += OpenIcons;
    }

    private void OpenIcons()
    {
        _Icons.SetActive(true);
    }

    private void OnEnable()
    {
        _touchpad.ReleasedTraining += StartFingerZomm;
    }
}