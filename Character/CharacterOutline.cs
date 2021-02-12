using UnityEngine;

public class CharacterOutline : MonoBehaviour
{
  [SerializeField] private Touchpad _touchpad;

  private Outline[] _characters;
  private Outline _outlinable;

  private void Start()
  {
    _characters = FindObjectsOfType<Outline>();
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

  private void Disable()
  {
    _outlinable = null;
  }

  private void Enable(Outline outline)
  {
    _outlinable = outline;
    _outlinable.enabled = true;
  }

  private void OnEnable()
  {
    _touchpad.SelectedSkin += (icon, position) => OutlineAll(icon, position);
    _touchpad.DraggedSkin += (position) =>
    {
      if (FindCharacters(position, 50) is GameObject character)
      {
        if (character.TryGetComponent(out Outline outline))
        {
          if (HighlightedAnotherCharacter(outline))
          {
            ChangeColor(Color.yellow, _outlinable);
            Disable();
          }
          else
          {
            ChangeColor(Color.green, outline);
            Enable(outline);
          }
        }
        else
        {
          if (_outlinable != null)
          {
            ChangeColor(Color.yellow, _outlinable);
            Disable();
          }
        }
      }
      else
      {
        if (_outlinable != null)
        {
          ChangeColor(Color.yellow, _outlinable);
          Disable();
        }
      }
    };

    _touchpad.ReleasedSkin += (position) =>
    {
      if (_outlinable != null)
      {
        Disable();
      }

      RemoveAllOutlines();
    };
  }

  private void ChangeColor(Color color, Outline outline)
  {
    outline.OutlineColor = color;
  }

  private void OutlineAll(GameObject icon, Vector2 position)
  {
    if (icon != null || IsCharacterDressed(icon, position))
    {
      foreach (var outline in _characters)
      {
        outline.enabled = true;
        outline.OutlineColor = Color.yellow;
      }
    }
  }

  private bool IsCharacterDressed(GameObject icon, Vector2 position)
  {
    if (FindCharacters(position, 0) is GameObject character)
    {
      var characterData = character.GetComponent<CharacterData>();

      return !IsCharacterNaked(characterData);
    }
    else
    {
      return false;
    }

    bool IsCharacterNaked(CharacterData characterData)
    {
      return characterData == null || characterData.ID == -1;
    }
  }

  private void RemoveAllOutlines()
  {
    foreach (var character in _characters)
    {
      character.enabled = false;
    }
  }

  private bool HighlightedAnotherCharacter(Outline outline)
  {
    return _outlinable != null && outline.GetHashCode() != _outlinable.GetHashCode();
  }
}