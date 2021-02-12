using UnityEngine;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void OnPlay()
    {
        _audioSource.Play();
    }
}