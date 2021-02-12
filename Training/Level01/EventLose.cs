using UnityEngine;

public class EventLose : MonoBehaviour
{
    [SerializeField] private EndGame _endGame;
    [SerializeField] private FingerMovement _fingerMovement;

    private void OnEnable()
    {
        _endGame.IsFinished += (isFinished) =>
        {
            if (isFinished == false)
            {
                _fingerMovement.gameObject.SetActive(true); 
            }
        };
    }
}