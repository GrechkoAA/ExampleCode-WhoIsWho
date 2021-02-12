using UnityEngine;

public class LevelBoard : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private TMPro.TMP_Text _levelNumber;

    private void Start()
    {
        SetLevelNumber();
    }

    private void SetLevelNumber()
    {
        _levelNumber.text = _saveSystem.LevelNumber.ToString();
    }
}