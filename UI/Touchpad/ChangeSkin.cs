using UnityEngine;

[RequireComponent(typeof(Touchpad))]
public class ChangeSkin : MonoBehaviour
{
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private Mesh _baseMesh;
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private SkinData[] _skinDataList;

    private Transform _currentIconSkin;

    public event System.Action<GameObject, Vector2> SkinIconActivated;
    public event System.Action<GameObject, bool> CharacterPainted;
    public event System.Action RemovedSkin;
    public event System.Action DressedUp;

    private void Change(Vector2 position)
    {
        var IconSkinData = _currentIconSkin.GetComponent<SkinData>();

        if (FindCharacters(position, 50) is GameObject character)
        {
            var charactersSkinnedMeshRenderer = character.GetComponentInChildren<SkinnedMeshRenderer>();
            var characterData = character.GetComponent<CharacterData>();

            if (charactersSkinnedMeshRenderer == null)
            {
                ResetSkinIcon(IconSkinData);
            }
            else if (charactersSkinnedMeshRenderer.sharedMesh == _baseMesh)
            {
                PutOn(charactersSkinnedMeshRenderer, IconSkinData, characterData);
                CharacterPainted?.Invoke(character, true);

                DressedUp?.Invoke();
            }
            else if (charactersSkinnedMeshRenderer.sharedMesh != _baseMesh)
            {
                DressedUp?.Invoke();

                Swap(charactersSkinnedMeshRenderer, IconSkinData, characterData);
            }
            else
            {
                ResetSkinIcon(IconSkinData);
            }
        }
    }

    private void Swap(SkinnedMeshRenderer charactersSkinnedMeshRenderer, SkinData iconSkinData, CharacterData characterData)
    {
        SkinData icon = GetSkinData(characterData);
        icon.gameObject.SetActive(true);
        icon.transform.position = icon.OriginPosition;
        icon.transform.localScale = icon.MaxScale;
  
        PutOn(charactersSkinnedMeshRenderer, iconSkinData, characterData);
    }

    private void PutOn(SkinnedMeshRenderer charactersSkinnedMeshRenderer, SkinData iconSkinData, CharacterData characterData)
    {
        characterData.ID = iconSkinData.ID;
        charactersSkinnedMeshRenderer.sharedMesh = iconSkinData.Mesh;
        charactersSkinnedMeshRenderer.material = iconSkinData.Material;

        _currentIconSkin.gameObject.SetActive(false);
    }

    private void ResetSkinIcon(SkinData iconSkinData)
    {
        _currentIconSkin.position = iconSkinData.OriginPosition;
        _currentIconSkin.localScale = iconSkinData.MaxScale;
    }

    private GameObject FindCharacters(Vector2 position, float offsetRayUp)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(position.x, position.y + offsetRayUp, 100));

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    private void RemoveSkin(Vector2 position)
    {
        var character = FindCharacters(position, 0);

        if (character != null)
        {
            var characterSkinnedMeshRenderer = character.GetComponentInChildren<SkinnedMeshRenderer>();

            if (characterSkinnedMeshRenderer != null && characterSkinnedMeshRenderer.sharedMesh != _baseMesh)
            {
                SetSkinData(GetSkinData(character.GetComponent<CharacterData>()), position);

                characterSkinnedMeshRenderer.sharedMesh = _baseMesh;
                characterSkinnedMeshRenderer.material = _baseMaterial;

                CharacterPainted?.Invoke(character, false);
                RemovedSkin?.Invoke();
            }
        }
    }

    private void SetSkinData(SkinData skinData, Vector2 position)
    {
        _currentIconSkin = skinData.transform;
        skinData.gameObject.SetActive(true);
        SkinIconActivated.Invoke(skinData.gameObject, position);

        _touchpad.IsSkinIcon = true;
    }

    private SkinData GetSkinData(CharacterData characterData)
    {
        foreach (var skinData in _skinDataList)
        {
            if (characterData.ID == skinData.ID)
            {
                characterData.ID = -1;
                return skinData;
            }
        }

        throw new System.Exception("No grid found in the list");
    }

    private void OnEnable()
    {
        _touchpad.SelectedSkin += (iconSkin, position) =>
        {
            if (iconSkin != null)
            {
                _currentIconSkin = iconSkin.transform;
            }
            else
            {
                RemoveSkin(position);
            }
        };

        _touchpad.ReleasedSkin += (position) =>
        {
            Change(position);
        };
    }
}