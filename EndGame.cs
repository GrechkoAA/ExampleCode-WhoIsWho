using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private ChangeSkin _changeSkin;
    [SerializeField] private CharacterData[] _characterDataList;

    private int _currentNumberCharactersDressed;
    private int _maximumNumberCharacters;

    public event System.Action<bool> IsFinished;

    private void Start()
    {
        _maximumNumberCharacters = _characterDataList.Length;
    }

    private bool IsEndGame()
    {
        return _maximumNumberCharacters == _currentNumberCharactersDressed;
    }

    private bool LevelIsPassed()
    {
        foreach (var characterData in _characterDataList)
        {
            if (MeshesAreEqual(characterData))
            {
                return false;
            }
        }

        return true;

        bool MeshesAreEqual(CharacterData characterData)
        {
            return characterData.Mesh != characterData.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
        }
    }

    private void OnEnable()
    {
        _changeSkin.CharacterPainted += (_, isPainted) =>
        {
            _currentNumberCharactersDressed = isPainted == true ? ++_currentNumberCharactersDressed
                                                                : --_currentNumberCharactersDressed;

            if (IsEndGame() == true)
            {
                IsFinished?.Invoke(LevelIsPassed());
            }
        };
    }
}