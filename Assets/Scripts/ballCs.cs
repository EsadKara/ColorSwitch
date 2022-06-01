using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCs : MonoBehaviour
{
    public Color[] colors;
    public float jumpForce;

    Rigidbody2D ballRb;
    SpriteRenderer spriteRenderer;
    gameManager gM;

    string[] availableColors = { "Yellow","Purple","Red","Turquoise" };
    string availableColor;
    int colorNo, colorNo2;

    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();

        SwitchColor();
        ballRb.gravityScale = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ballRb.gravityScale = 3;
            ballRb.velocity = Vector2.up * jumpForce;
        }
    }

    void SwitchColor()
    {
        colorNo = Random.Range(0, 4);
        if (colorNo != colorNo2)
            colorNo2 = colorNo;
        else
            SwitchColor();
        spriteRenderer.color = colors[colorNo];
        availableColor = availableColors[colorNo];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColorSwitchTool")
        {
            SwitchColor();
            Destroy(collision.transform.gameObject);
        }

        else if (collision.gameObject.tag == "Center")
        {
            gM.SpawnCircle();
            gM.IncreaseScore();
            Destroy(collision.transform.gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            gM.isOver = true;
        }
        else if (collision.gameObject.tag != availableColor)
        {
            gM.isOver = true;
        }

    }
}
