using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent _loading;

    public void OnLoad(string scene)
    {
        _loading.Invoke();

        SceneManager.LoadScene(scene);
    }

    public void OnRestart()
    {
        _loading.Invoke();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}