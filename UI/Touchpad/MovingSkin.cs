using UnityEngine;

[RequireComponent(typeof(Touchpad), typeof(ChangeSkin))]
public class MovingSkin : MonoBehaviour
{
    [SerializeField] private Touchpad _touchpad;
    [SerializeField] private ChangeSkin _changeSkin;

    public Transform _currentIconSkin;

    private void ThrowSkin(Vector2 position)
    {
        float offsetRay = 50;
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(position.x, position.y + offsetRay, 100));

        if (Physics.Raycast(ray, out hit) == false)
        {
            ResetSkinIcon();
        }
    }

    private void ResetSkinIcon()
    {
        var iconSkinData = _currentIconSkin.GetComponent<SkinData>();

        _currentIconSkin.position = iconSkinData.OriginPosition;
        _currentIconSkin.localScale = iconSkinData.MaxScale;
    }

    private void OnEnable()
    {
        _changeSkin.SkinIconActivated += (iconSkin, position) =>
        {
            _currentIconSkin = iconSkin.transform;
            _currentIconSkin.position = position;
            _currentIconSkin.transform.localScale = iconSkin.GetComponent<SkinData>().MinScale;
        };

        _touchpad.SelectedSkin += (iconSkin, _) =>
        {
            if (iconSkin != null)
            {
                _currentIconSkin = iconSkin.transform;
                _currentIconSkin.transform.localScale = iconSkin.GetComponent<SkinData>().MinScale;
            }
        };

        _touchpad.DraggedSkin += (position) =>
        {
            if (_currentIconSkin != null)
            {
                _currentIconSkin.position = position;
            }
        };

        _touchpad.ReleasedSkin += (position) =>
        {
            ThrowSkin(position);

            _currentIconSkin = null;
        };
    }
}