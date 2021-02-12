using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private Mesh _mesh;

    [SerializeField] private int _id = -1;

    public Mesh Mesh => _mesh;

    public int ID { get => _id; set => _id = value; }
}