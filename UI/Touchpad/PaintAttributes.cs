using UnityEngine;

[RequireComponent(typeof(ChangeSkin))]
public class PaintAttributes : MonoBehaviour
{
    [SerializeField] private ChangeSkin _changeSkin;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Attribut[] _attributs;

    private void OnEnable()
    {
        _changeSkin.CharacterPainted += (character, isPainted) =>
        {
            for (int i = 0; i < _attributs.Length; i++)
            {
                if (CharactersAreEqual(character, i))
                {
                    Paint(i, isPainted == true ? _attributs[i].Material : _defaultMaterial);
                }
            }
        };

        bool CharactersAreEqual(GameObject character, int i)
        {
            return _attributs[i].Character == character;
        }
    }

    private void Paint(int idAttrubut, Material material)
    {
        for (int x = 0; x < _attributs[idAttrubut].MeshRenderers.Length; x++)
        {
            _attributs[idAttrubut].MeshRenderers[x].material = material;
        }
    }
}

[System.Serializable]
public class Attribut
{
    [SerializeField] private GameObject _character;
    [SerializeField] private Material _material;
    [SerializeField] private MeshRenderer[] _MeshRenderers;

    public GameObject Character => _character;

    public Material Material => _material;

    public MeshRenderer[] MeshRenderers => _MeshRenderers;
}