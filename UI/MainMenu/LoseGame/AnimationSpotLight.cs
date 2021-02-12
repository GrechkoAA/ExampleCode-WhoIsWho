using System.Collections;
using UnityEngine;

public class AnimationSpotLight : MonoBehaviour
{
    [SerializeField] private Light _spotLight;
    [SerializeField, Min(0)] private float _interval;
    
    private EndGame _endGame;

    private void Start()
    {
        _endGame = FindObjectOfType<EndGame>();
        _endGame.IsFinished += PlayAnimation;
    }

    private void PlayAnimation(bool isFinished)
    {
        if (isFinished == false)
        {
            StartCoroutine(PlayAnaimation());
        }
    }

    IEnumerator PlayAnaimation()
    {
        for (int i = 0; i < 3; i++)
        {
            _spotLight.enabled = true;
            yield return new WaitForSeconds(_interval);

            _spotLight.enabled = false;
            yield return new WaitForSeconds(_interval);
        }
    }
}