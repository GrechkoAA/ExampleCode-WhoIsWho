using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;

    private void Start()
    {
        _saveSystem.IsFirstStarting = true;
        _saveSystem.OnSave();
        SceneManager.LoadScene(_saveSystem.LevelName);
    }
}