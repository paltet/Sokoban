using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public Level currentLevel = null;

    private string _levelFolderName = "leveldata";
    private string _dataFileExtension = ".json";

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
            Application.persistentDataPath, _levelFolderName + "/" + filename + _dataFileExtension
            );
        Debug.Log(path);
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    public void SaveLevel(Level level)
    {
        SaveData(level.name, level);
    }

    public List<Level> GetLevelList()
    {
        List<Level> levels = new List<Level>();

        string path = Path.Combine(
            Application.persistentDataPath, _levelFolderName
            );
        DirectoryInfo info = new DirectoryInfo(path);

        foreach(var file in info.GetFiles("*" + _dataFileExtension))
        {
            string data = File.ReadAllText(file.FullName);
            levels.Add(JsonUtility.FromJson<Level>(data));
        }

        return levels;
    }

    public void LoadNextLevel()
    {
        List<Level> levels = GetLevelList();
        int positionToLoad = -1;
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].name == currentLevel.name) positionToLoad = i+1;
        }
        if (positionToLoad < 0 || positionToLoad >= levels.Count)
        {
            currentLevel = null;
            SceneManager.LoadScene("LevelSelector");
        }
        else
        {
            currentLevel = levels[positionToLoad];
            SceneManager.LoadScene("LevelPlayer");
        }
    }
}

[Serializable]
public class Level
{
    public string name;
    public List<string> map;
}
