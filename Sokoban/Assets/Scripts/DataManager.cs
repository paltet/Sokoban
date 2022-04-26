using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        if (DataManager.instance == null)
        {
            DataManager.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void SaveData(string filename, object data)
    {
        string path = Path.Combine(
            Application.persistentDataPath, "leveldata/", filename + ".json"
            );
        Debug.Log(path);
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    public void SaveLevel(Level level)
    {
        SaveData(level.name, level);
    }
}

[Serializable]
public class Level
{
    public string name;
    public List<string> map;
}
