using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private Save _save = new Save();

    public string Path { get; private set; }

    public string LevelName { get => _save.LevelName; set => _save.LevelName = value; }

    public bool IsMusicOff { get => _save.IsMusicOff; set => _save.IsMusicOff = value; }

    public bool IsEffectsOff { get => _save.IsEffectsOff; set => _save.IsEffectsOff = value; }

    public bool IsFirstStarting { get => _save.IsFirstStart; set => _save.IsFirstStart = value; }

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Path = System.IO.Path.Combine(Application.persistentDataPath, "Data.json");
#else
        Path = System.IO.Path.Combine(Application.dataPath, "Data.json");
#endif
        if (File.Exists(Path))
        {
            _save = JsonUtility.FromJson<Save>(File.ReadAllText(Path));
        }
    }

    public void OnSave()
    {
        File.WriteAllText(Path, JsonUtility.ToJson(_save));
    }

    private void OnApplicationQuit()
    {
        OnSave();
    }

    private void OnApplicationPause(bool pause)
    {
        OnSave();
    }
}

[System.Serializable]
public class Save
{
    public int LevelNumber;
    public string LevelName;

    public bool IsMusicOff = true;
    public bool IsEffectsOff = true;

    public bool IsFirstStart;
}