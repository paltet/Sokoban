using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectorButton : MonoBehaviour
{
    Level level;

    public void Set(Level newLevel)
    {
        GetComponentInChildren<Text>().text = newLevel.name;
        level = newLevel;
    }

    public void Load()
    {
        DataManager.instance.currentLevel = level;
        SceneManager.LoadScene("LevelPlayer");
    }

    public void LoadLevelCreator()
    {
        SceneManager.LoadScene("LevelCreator");
    }
}
