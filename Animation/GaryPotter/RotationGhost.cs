using DG.Tweening;
using UnityEngine;

public class RotationGhost : MonoBehaviour
{
    public void PlayAnimation()
    {
        transform.DOLocalRotate(new Vector3(0, 180, 0), 1.5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}