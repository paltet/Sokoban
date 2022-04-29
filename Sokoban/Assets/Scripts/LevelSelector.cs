using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Transform levelsPanel;
    public GameObject levelSelectorButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        List<Level> levels = DataManager.instance.GetLevelList();
        GameObject currentButton;

        foreach(Level level in levels)
        {
            //Debug.Log(level.name);
            currentButton = Instantiate(levelSelectorButtonPrefab, levelsPanel);
            currentButton.GetComponent<LevelSelectorButton>().Set(level);
        }
    }

}
