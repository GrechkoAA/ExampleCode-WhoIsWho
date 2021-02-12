using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private UnityEngine.UI.Image _effectsIcon;
    [SerializeField] private UnityEngine.UI.Image _musicIcon;

    private SaveSystem _saveSystem;
    private bool _isMusicOff;
    private bool _isEffectsOff;

    private void Start()
    {
        _saveSystem = FindObjectOfType<SaveSystem>();

        _isEffectsOff = _saveSystem.IsEffectsOff;
        _isMusicOff = _saveSystem.IsMusicOff;

        SetVolume();
    }

    private void SetVolume()
    {
        _isEffectsOff = _saveSystem.IsEffectsOff;
        _isMusicOff = _saveSystem.IsMusicOff;

        _mixer.audioMixer.SetFloat("VolumeEffects", _isEffectsOff == false ? -80 : 0);
        _mixer.audioMixer.SetFloat("VolumeUI", _isEffectsOff == false ? -80 : 0);
        _mixer.audioMixer.SetFloat("VolumeMusic", _isMusicOff == false ? -80 : 0);

        _musicIcon.DOFade(_isMusicOff == false ? 0.3f : 1, 0);
        _effectsIcon.DOFade(_isEffectsOff == false ? 0.3f : 1, 0);
    }

    public void OffEfects()
    {
        _isEffectsOff = !_isEffectsOff;
        _mixer.audioMixer.SetFloat("VolumeEffects", _isEffectsOff == false ? -80 : 0);
        _mixer.audioMixer.SetFloat("VolumeUI", _isEffectsOff == false ? -80 : 0);
        _effectsIcon.DOFade(_isEffectsOff == false ? 0.3f : 1, 0);

        _saveSystem.IsEffectsOff = _isEffectsOff;
    }

    public void OffMusic()
    {
        _isMusicOff = !_isMusicOff;
        _mixer.audioMixer.SetFloat("VolumeMusic", _isMusicOff == false ? -80 : 0);
        _musicIcon.DOFade(_isMusicOff == false ? 0.3f : 1, 0);

        _saveSystem.IsMusicOff = _isMusicOff;
    }
}