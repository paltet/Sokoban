using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileLogic : MonoBehaviour
{
    public Image image;
    public char id = ' ';

    public void SetTile()
    {
        BrushLogic brush = LevelCreator.instance.selectedBrush;
        if (brush != null)
        {
            image.sprite = brush.sprite;
            id = brush.id;
        }

        if (id == ' ') image.enabled = false;
        else image.enabled = true;
    }
}
