using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudiAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private EndGame _endGame;

    private void Start()
    {
        _endGame = FindObjectOfType<EndGame>();
        _endGame.IsFinished += Play;
    }

    private void Play(bool isFinished)
    {
        if (isFinished == false)
        {
            StartCoroutine(PlayEffect());
        }
    }

    IEnumerator PlayEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            _audioSource.Play();
            yield return new WaitForSeconds(_audioSource.clip.length / 1.9f);
        }
    }
}
