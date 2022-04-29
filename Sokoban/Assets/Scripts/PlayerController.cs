using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite downSprite, leftSprite, rightSprite, upSprite;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-1f, 0, 0));
            spriteRenderer.sprite = leftSprite;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(1f, 0, 0));
            spriteRenderer.sprite = rightSprite;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -1f, 0));
            spriteRenderer.sprite = downSprite;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 1f, 0));
            spriteRenderer.sprite = upSprite;
        }
    }
}
