using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _touch;
    [SerializeField] private GameObject _mainMenu;

    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _rotation;

    public void OnStart()
    {
        _mainMenu.SetActive(false);
        _touch.SetActive(true);
    }
}
