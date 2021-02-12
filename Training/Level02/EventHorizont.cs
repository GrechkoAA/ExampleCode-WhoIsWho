using UnityEngine;

public class EventHorizont : MonoBehaviour
{
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private FingerMovement _fingerMovement;

    private void StartFingerZomm()
    {
        _fingerMovement.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _touchpad.ReleasedTraining += StartFingerZomm;
    }
}
