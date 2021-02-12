using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private EndGame _endGame;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private UnityEngine.UI.Button _nextLevel;
    [SerializeField] private string _nameNextLevel;

    private void GoNextScene(bool isFinished)
    {
        if (isFinished == true)
        {
            _saveSystem.LevelNumber++;
            _saveSystem.LevelName = _nameNextLevel;
            _nextLevel.onClick.AddListener(() => _sceneLoader.OnLoad(_nameNextLevel));
        }
    }

    public void OnGoNextLevel()
    {
        if (string.IsNullOrEmpty(_saveSystem.LevelName))
        {
            _sceneLoader.OnLoad(_nameNextLevel); 
        }
        else
        {
            _sceneLoader.OnLoad(_saveSystem.LevelName);
        }
    }

    private void OnEnable()
    {
        if (_endGame != null)
        {
            _endGame.IsFinished += GoNextScene; 
        }
    }
}