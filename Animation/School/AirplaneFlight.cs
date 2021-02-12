using DG.Tweening;
using UnityEngine;

public class AirplaneFlight : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    public void OnFly()
    {
        _target.DOMoveZ(-4.5f, 1);
        _target.DORotate(new Vector3(18.7f,12.13f,33.82f), 1);
    }
}