using UnityEngine;

public class SkinData : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Material _material;
    [SerializeField] private Mesh _mesh;

    private Vector2 _maxScale;
    private Vector2 _minScale;
    private Vector2 _originPosition;

    public int ID => _id;

    public Material Material => _material;

    public Mesh Mesh => _mesh;

    public Vector2 MinScale => _minScale;

    public Vector2 MaxScale => _maxScale;

    public Vector2 OriginPosition => _originPosition;

    private void Start()
    {
        _maxScale = transform.localScale;
        _minScale = _maxScale * 0.7f;
        _originPosition = transform.position;
    }
}