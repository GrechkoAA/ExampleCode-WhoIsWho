using UnityEngine;

public class AudioSelectionIcons : MonoBehaviour
{
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private ChangeSkin _changeSkin;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _drees;
    [SerializeField] private AudioClip _select;

    private void PutOn()
    {
        _audioSource.PlayOneShot(_drees);
    }

    private void OnEnable()
    {
        _changeSkin.SkinIconActivated += (iconSkin, position) =>
        {
            _audioSource.PlayOneShot(_select);
        };

        _changeSkin.DressedUp += PutOn;

        _touchpad.SelectedSkin += (iconSkin, _) =>
        {
            if (iconSkin != null)
            {
                _audioSource.PlayOneShot(_select);
            }
        };
    }
}
