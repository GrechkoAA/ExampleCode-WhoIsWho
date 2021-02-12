using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private float _duration;
    [SerializeField, Range(1, 2)] private float _multiplyScale;
    [SerializeField] private Image _imageStartGame;
    [SerializeField] private Button _buttonStartGame;
    [SerializeField] private TMPro.TMP_Text _textStartGame;

    private SaveSystem _saveSystem;

    private void Start()
    {
        _saveSystem = FindObjectOfType<SaveSystem>();

        if (IsFirstLaunchGame())
        {
            _saveSystem.IsFirstStarting = false;
            _saveSystem.OnSave();
            ShowMenu();
        }
        else
        {
            _transform.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }

        _transform.DOScale(_transform.localScale.x * _multiplyScale, _duration).SetLoops(-1, LoopType.Yoyo);
    }

    private void ShowMenu()
    {
        _imageStartGame.enabled = true;
        _imageStartGame.raycastTarget = true;
        _buttonStartGame.interactable = true;
        _textStartGame.enabled = true;
    }

    private bool IsFirstLaunchGame()
    {
        return _saveSystem.IsFirstStarting == true;
    }
}