using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Movement : MonoBehaviour
{

    public bool isPLaying = false;
    private bool instructionsShowing = true;

    public int blobSize = 1;
    private int maxGrow = 6;

    [Range(-1, 1)]
    public int facing = 1;

    public int score = 0;
    Text mouseText;
    Text scoreText;

    SpriteRenderer blobRenderer;

    AudioClip HitSound;

    public Text scoreBubbleText;
    public GameObject scoreBubblePanel;
    public GameObject gameOver;
    public GameObject instructions;

    void Start()
    {
        //mouseText = GameObject.FindGameObjectWithTag("mouseText").GetComponent<Text>();
        scoreText = GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>();
        blobRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isPLaying)
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

            //mouseText.text = "MouseX: " + mousePos.x.ToString("G2");
            //mouseText.text += "\nPlayerX: " + transform.position.x.ToString("G2");
            //mouseText.text += "\nbInf: " + blobBoundary.x.ToString("G2") + "   bSup: " + blobBoundary.y.ToString("G2");
            scoreText.text = "Score " + "\n" + score + " ";
        }
        else if (!isPLaying && instructionsShowing && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
        else if (!isPLaying && !instructionsShowing && Input.GetMouseButtonDown(0))
        {
            instructions.SetActive(true);
            instructionsShowing = true;
        }
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
        if ((Input.GetKeyDown("g") || Input.GetMouseButtonDown(0)) && blobSize < maxGrow)
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "coin")
        {
            coll.gameObject.GetComponent<AudioSource>().Play();
            score = score + blobSize;
            StartCoroutine(DisplayScoreGained(blobSize));
            coll.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (coll.gameObject.tag == "spikes")
        {
            coll.gameObject.GetComponent<AudioSource>().Play();
            GetHitOnce();
        }
        else if (coll.gameObject.tag == "wall")
        {
            coll.gameObject.GetComponent<AudioSource>().Play();
            GetHitAll();
        }

    }

    void GetHitOnce()
    {
        if (blobSize > 1)
        {
            blobSize--;
            StartCoroutine(Blink(1f, 0.2f));
        }
        else if (blobSize == 1)
            GameOver();
    }

    void GetHitAll()
    {
        if (blobSize > 1)
        {
            blobSize = 1;
            StartCoroutine(Blink(1f, 0.2f));
        }
        else if (blobSize == 1)
            GameOver();
    }

    IEnumerator Blink(float duration, float blinkTime)
    {
        while (duration > 0f)
        {
            duration -= .2f;// Time.deltaTime;
            transform.GetComponent<BoxCollider2D>().enabled = false;
            if (blobRenderer.color.Equals(Color.red))
                blobRenderer.color = Color.white;
            else if (blobRenderer.color.Equals(Color.white))
                blobRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkTime);
        }
        blobRenderer.color = Color.white;
        transform.GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator DisplayScoreGained(int n)
    {
        scoreBubbleText.enabled = true;
        if (facing == 1)
            scoreBubbleText.rectTransform.localScale = new Vector3(1, 1, 1);
        else if (facing == -1)
            scoreBubbleText.rectTransform.localScale = new Vector3(-1, 1, 1);
        scoreBubblePanel.SetActive(true);
        scoreBubbleText.text = "+" + n;

        yield return new WaitForSeconds(2);
        scoreBubbleText.enabled = false;
        scoreBubblePanel.SetActive(false);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        isPLaying = false;
        Time.timeScale = 0.0f;
        Camera.main.GetComponent<AudioSource>().Stop();
        blobRenderer.enabled = false;
    }

    public void ShowInstructions()
    {
        instructions.SetActive(true);
        instructionsShowing = true;
    }

    void StartGame()
    {
        instructions.SetActive(false);
        instructionsShowing = false;
        isPLaying = true;
        gameOver.SetActive(false);
        Time.timeScale = 1.0f;
        score = 0;
        Camera.main.GetComponent<AudioSource>().Play();
        Camera.main.GetComponent<ObstaclesGeneration>().ResetLevel();
        blobRenderer.enabled = true;
    }
}
