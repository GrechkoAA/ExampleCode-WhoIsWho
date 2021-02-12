using UnityEngine;

public class BulletAnimation : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;

    private void OnPlay()
    {
        foreach (var rigidbodie in _rigidbodies)
        {
            rigidbodie.isKinematic = false;
        }
    }
}