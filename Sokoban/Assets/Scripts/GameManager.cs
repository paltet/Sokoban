using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Level level;
    GameObject prefab, currentTile;
    int moves = 0;
    bool won = false;

    private GameObject[][] map;
    private Vector2Int player;
    private List<Vector2Int> flowers;

    public Text text;
    public string instructions = "Arrows to move, R to restart. | MOVES = "; 
    public BrushLogic[] brushes;

    // Start is called before the first frame update
    void Start()
    {
        level = DataManager.instance.currentLevel;
        flowers = new List<Vector2Int>();
        if (level != null) Load();
        else Debug.Log("Level not selected!");
    }

    private void Load()
    {
        List<string> charmap = level.map;
        map = new GameObject[charmap.Count][];

        int minX = charmap.Count, maxX = 0;
        int minY = 0, maxY = -charmap[0].Length;

        for (int x = 0; x < charmap.Count; x++)
        {
            string currentColumn = charmap[x];
            map[x] = new GameObject[currentColumn.Length];

            for (int y = 0; y < currentColumn.Length; y++)
            {
                prefab = GetBrushPrefab(currentColumn[y]);
                if (prefab != null)
                {
                    map[x][y] = Instantiate(prefab, this.transform);
                    map[x][y].transform.position = new Vector3(x, -y, 0);

                    switch (map[x][y].tag)
                    {
                        case "Player":
                            player = new Vector2Int(x, y);
                            break;
                        case "Flower":
                            flowers.Add(new Vector2Int(x, y));
                            break;
                    }

                    if (x < minX) minX = x;
                    else if (x > maxX) maxX = x;

                    if (-y < minY) minY = -y;
                    else if (-y > maxY) maxY = -y;
                }
            }
        }

        SetCamera(minX, maxX, minY, maxY);
    }

    GameObject GetBrushPrefab(char id)
    {
        foreach (BrushLogic brush in brushes)
        {
            if (id == brush.id) return brush.prefab;
        }
        Debug.Log(id + " brush not found.");

        return null;
    }

    void SetCamera(int minX, int maxX, int minY, int maxY)
    {
        Camera.main.transform.position = new Vector3(minX + (maxX - minX) / 2, maxY + (minY - maxY) / 2, Camera.main.transform.position.z);
        //Camera.main.orthographicSize = ((maxY) - (-minY)) * 2;
        //Debug.Log(Camera.main.aspect);
        //Debug.Log(Camera.main.transform.position);
        //Debug.Log(minX + " " + maxX + " " + minY + " " + maxY);
    }

    private void Update()
    {

        if (player != null && !won)
        {
            text.text = instructions + moves.ToString();
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (MoveLeft(player)) moves++;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (MoveRight(player)) moves++;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (MoveDown(player)) moves++;
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (MoveUp(player)) moves++;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene("LevelPlayer");
    }

    bool IndexInMapBounds(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0) return false;
        else if (pos.x >= map.Length) return false;
        else if (pos.y >= map[0].Length) return false;
        else return true;
    }

    bool MoveLeft(Vector2Int pos) {     return Move(pos, new Vector2Int(pos.x - 1, pos.y)); }
    bool MoveRight(Vector2Int pos) {    return Move(pos, new Vector2Int(pos.x + 1, pos.y)); }
    bool MoveDown(Vector2Int pos) { return Move(pos, new Vector2Int(pos.x, pos.y+1)); }
    bool MoveUp(Vector2Int pos) { return Move(pos, new Vector2Int(pos.x , pos.y-1)); }

    bool Move(Vector2Int pos, Vector2Int newPos)
    {
        GameObject gameObject;

        if (!IndexInMapBounds(pos)) return false;
        if (map[pos.x][pos.y] == null) return true;
        else
        {
            bool canMove;
            gameObject = map[pos.x][pos.y];

            switch (gameObject.tag)
            {
                case "Player":
                    canMove = Move(newPos, newPos + (newPos-pos));
                    if (canMove)
                    {
                        gameObject.transform.Translate(new Vector3(newPos.x - pos.x, -(newPos.y -pos.y), 0));
                        map[newPos.x][newPos.y] = gameObject;
                        map[pos.x][pos.y] = null;
                        player = newPos;
                    }
                    return canMove;

                case "Box":
                    canMove = Move(newPos, newPos + (newPos - pos));
                    if (canMove)
                    {
                        gameObject.transform.Translate(new Vector3(newPos.x - pos.x, -(newPos.y - pos.y), 0));
                        map[newPos.x][newPos.y] = gameObject;
                        map[pos.x][pos.y] = null;
                        if (flowers.Contains(newPos))
                        {
                            gameObject.tag = "Rock";
                            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                            flowers.Remove(newPos);
                            CheckForWin();
                        }
                    }
                    return canMove;

                case "Flower":
                    return true;

                case "Rock":
                    return false;

                default:
                    return false;
            }
        }
    }

    private void CheckForWin()
    {
        if (flowers.Count == 0)
        {
            won = true;
            text.color = Color.green;
            text.text = "You won!";
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
