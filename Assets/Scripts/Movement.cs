using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour
{

    public int blobSize = 1;
    private int maxGrow = 6;

    [Range(-1, 1)]
    public int facing = 1;

    public int score = 0;
    Text mouseText;
    Text scoreText;

    void Start()
    {
        mouseText = GameObject.FindGameObjectWithTag("mouseText").GetComponent<Text>();
        scoreText = GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>();
    }


    void Update()
    {
        // facing == 1
        Vector2 blobBoundary = new Vector2(transform.position.x, transform.position.x + blobSize);
        // facing = -1
        if (facing == -1)
            blobBoundary = new Vector2(transform.position.x - blobSize, transform.position.x);

        var mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // mouse is inside blobBoundary
        if (mousePos.x > blobBoundary.x && mousePos.x < blobBoundary.y)
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                if (facing == -1)
                    transform.position = new Vector3(transform.position.x - blobSize, 0, 0);
                facing = 1;
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                if (facing == 1)
                    transform.position = new Vector3(transform.position.x + blobSize, 0, 0);
                facing = -1;
            }
        }
        // mouse is outside blobBoundary
        else if (mousePos.x > blobBoundary.y)
            transform.position = new Vector3(transform.position.x + 1, 0, 0);
        else if (mousePos.x < blobBoundary.x)
            transform.position = new Vector3(transform.position.x - 1, 0, 0);

        Grow();
        Shrink();
        UpdateFacing();

        mouseText.text = "MouseX: " + mousePos.x.ToString("G2") +
                            "\nPlayerX: " + transform.position.x.ToString("G2") +
                            "\nbInf: " + blobBoundary.x.ToString("G2") + "   bSup: " + blobBoundary.y.ToString("G2");
        scoreText.text = "Score " + "\n" + score + " ";
    }

    void UpdateFacing()
    {
        if (facing == 1)
        {
            transform.localScale = new Vector3(blobSize, blobSize, 1);
        }
        else if (facing == -1)
        {
            transform.localScale = new Vector3(-blobSize, blobSize, 1);
        }
    }

    void Grow()
    {
        // left click
        if (Input.GetMouseButtonDown(0) && blobSize < maxGrow)
        {
            blobSize++;
        }
    }

    void Shrink()
    {
        // jump or right click
        if ((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(1)) && blobSize > 1)
        {
            blobSize--;
        }
    }

    void Merge()
    {

    }

    void Split()
    {

    }



    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "leftBox")
            score--;
        else if (coll.gameObject.tag == "rightBox")
            score++;
        else if (coll.gameObject.tag == "wall")
            score++;
    }
}
