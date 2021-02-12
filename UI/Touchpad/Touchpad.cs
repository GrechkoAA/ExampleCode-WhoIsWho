using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touchpad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private int _idPressedFingerSkinIcon;

    public event Action<Vector2> Pressed;
    public event Action<Vector2> Dragged;
    public event Action<Vector2> Released;
    public event Action<GameObject, Vector2> SelectedSkin;
    public event Action<Vector2> DraggedSkin;
    public event Action<Vector2> ReleasedSkin;
    public event Action ReleasedTraining;
    public bool IsSkinIcon;

    public int ClickCount { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickCount++;

        if (IsSkinIcon == false)
        {
            _idPressedFingerSkinIcon = eventData.pointerId;
            SelectedSkin?.Invoke(GetSkinIcon(eventData), eventData.position);
        }

        if (CanRotated())
        {
            Pressed?.Invoke(eventData.position);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CanMoveSkinIcon(eventData))
        {
            DraggedSkin?.Invoke(eventData.position);
        }
        else if (CanRotated())
        {
            Dragged?.Invoke(eventData.position);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ClickCount--;

        if (IsFingerWithSkinIcon(eventData))
        {
            _idPressedFingerSkinIcon = -10;
            IsSkinIcon = false;
            ReleasedSkin?.Invoke(eventData.position);
        }

        if (ClickCount == 1)
        {
            Released.Invoke(eventData.pointerId == 0 ? Input.GetTouch(1).position : Input.GetTouch(0).position);
        }

        ReleasedTraining?.Invoke();
    }

    private bool IsFingerWithSkinIcon(PointerEventData eventData)
    {
        return IsSkinIcon == true && _idPressedFingerSkinIcon == eventData.pointerId;
    }

    private bool CanMoveSkinIcon(PointerEventData eventData)
    {
        return IsSkinIcon == true && eventData.pointerId == _idPressedFingerSkinIcon;
    }

    private bool CanRotated()
    {
        return ClickCount == 1 && IsSkinIcon == false;
    }

    private GameObject GetSkinIcon(PointerEventData eventData)
    {
        var currentObject = eventData.pointerCurrentRaycast.gameObject;
        var skinIcon = currentObject == gameObject ? null : currentObject;

        IsSkinIcon = skinIcon != null;

        return skinIcon;
    }
}