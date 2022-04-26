using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushLogic : MonoBehaviour
{
    public char id;
    public Sprite sprite;
    public GameObject prefab;

    public void SelectBrush()
    {
        LevelCreator.instance.selectedBrush = this;
    }
}
