using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCreator : MonoBehaviour
{
    public static LevelCreator instance = null;

    [Header("Grid Size")]
    public int width = 20;
    public int height = 15;

    [Header ("Brush UI")]
    public BrushLogic selectedBrush;
    public GameObject[] brushes;

    [Header ("Prefabs")]
    public GameObject tileContainer;
    public GameObject tileColumnPrefab;
    public GameObject tilePrefab;

    private GameObject[][] tilemap;

    [Header ("Save UI")]
    public InputField levelNameInput;
    public Button levelSaveButton;

    private Level level;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        level = new Level();

        GameObject currentRow;
        tilemap = new GameObject[width][];

        for (int x = 0; x < width; x++)
        {
            currentRow = Instantiate(tileColumnPrefab, tileContainer.transform);
            tilemap[x] = new GameObject[height];

            for (int y = 0; y < height; y++)
            {
                tilemap[x][y] = Instantiate(tilePrefab, currentRow.transform);
            }
        }
    }

    void Map()
    {
        level.map = new List<string>();

        for (int x = 0; x < width; x++)
        {
            string currentColumn = "";

            for (int y = 0; y < height; y++)
            {
                TileLogic currentTile;
                currentTile = tilemap[x][y].GetComponent<TileLogic>();
                currentColumn += currentTile.id;
            }

            level.map.Add(currentColumn);
        }
    }

    public void SaveLevel()
    {
        Map();
        level.name = levelNameInput.text;
        Debug.Log(level.map);
        DataManager.instance.SaveLevel(level);
        levelSaveButton.GetComponentInChildren<Image>().color = Color.green;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }
}