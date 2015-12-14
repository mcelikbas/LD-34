using UnityEngine;
using System.Collections;
using System;

public class ObstaclesGeneration : MonoBehaviour
{
    private const float TIMER_OBSTACLES_TOUCHING = 0.24f;

    public float level_time = 0.0f;

    private float time = 0.0f;
    private float timer = TIMER_OBSTACLES_TOUCHING;
    private int row = 0;

    private float[] boundaryX = new float[] { -8.0f, 7.0f };

    //private string[][] level;

    public Transform obstacles;

    private Texture2D levelTexture;
    private int[,] level;

    private GameObject player;

    void Start()
    {
        Camera.main.aspect = 5.0f / 4.0f;

        //string file = "Assets/Resources/Levels/Level_1.txt";
        //level = ReadFile(file);

        levelTexture = (Texture2D)Resources.Load("Levels/Level_1");
        level = ReadImage(levelTexture);

        player = GameObject.FindGameObjectWithTag("Player");
        //print(levelTexture.GetPixel(0,0));
    }

    void Update()
    {
        if (player.GetComponent<Movement>().isPLaying)
        {
            if (row < level.GetLength(0))
            {
                time += Time.deltaTime;
                level_time += Time.deltaTime;
                if (time > timer)
                {
                    RenderRow(row);
                    row++;
                    time = 0.0f;
                }
            }
            else
            {
                player.GetComponent<Movement>().GameOver();
                player.GetComponent<Movement>().ShowInstructions();
            }
        }
    }

    public void ResetLevel()
    {
        row = 0;
        time = 0.0f;
        foreach (Transform child in obstacles)
        {
            Destroy(child.gameObject);
        }
    }

    void RenderRow(int row)
    {
        for (int j = 0; j < level.GetLength(1); j++)
        {
            Vector3 spawnPosition = new Vector3(j + boundaryX[0], 12, 0);
            //if (level[row][j] == "1" || level[row][j] == "1\r")
            if (level[row, j] == 1)
            {
                GameObject go = Instantiate(Resources.Load("Prefabs/Wall"), spawnPosition, Quaternion.identity) as GameObject;
                go.transform.parent = obstacles;
            }
            else if (level[row, j] == 2)
            {
                GameObject go = Instantiate(Resources.Load("Prefabs/Wall_Shadow"), spawnPosition, Quaternion.identity) as GameObject;
                go.transform.parent = obstacles;
            }
            else if (level[row, j] == 3)
            {
                GameObject go = Instantiate(Resources.Load("Prefabs/Spikes"), spawnPosition, Quaternion.identity) as GameObject;
                go.transform.parent = obstacles;
            }
            else if (level[row, j] == 4)
            {
                GameObject go = Instantiate(Resources.Load("Prefabs/Square"), spawnPosition, Quaternion.identity) as GameObject;
                go.transform.parent = obstacles;
            }
            else if (level[row, j] == 5)
            {
                GameObject go = Instantiate(Resources.Load("Prefabs/Coin"), spawnPosition, Quaternion.identity) as GameObject;
                go.transform.parent = obstacles;
            }
        }
    }

    // ***  0  -  WHITE - NULL
    // ***  1  -  BLACK - WALL
    // ***  2  -  GREEN - WALL_SHADOW
    // ***  3  -  RED   - SPIKES
    // ***  4  -  BLUE  - SQUARE
    // ***  5  -  CYAN  - COIN

    // level reading based on an image
    int[,] ReadImage(Texture2D texture)
    {
        int[,] levelBase = new int[texture.height, texture.width];
        for (int i = 0; i < texture.height; i++)
        {
            for (int j = 0; j < texture.width; j++)
            {
                if (levelTexture.GetPixel(j, i).Equals(Color.white))
                    levelBase[i, j] = 0;
                else if (levelTexture.GetPixel(j, i).Equals(Color.black))
                    levelBase[i, j] = 1;
                else if (levelTexture.GetPixel(j, i).Equals(Color.green))
                    levelBase[i, j] = 2;
                else if (levelTexture.GetPixel(j, i).Equals(Color.red))
                    levelBase[i, j] = 3;
                else if (levelTexture.GetPixel(j, i).Equals(Color.blue))
                    levelBase[i, j] = 4;
                else if (levelTexture.GetPixel(j, i).Equals(Color.cyan))
                    levelBase[i, j] = 5;
            }
        }
        return levelBase;
    }


    // level reading based on a text-file //
    // switch to reading image instead
    //string[][] ReadFile(string file)
    //{
    //    string fileText = System.IO.File.ReadAllText(file);
    //    string[] lines = fileText.Split("\n"[0]);
    //    int rows = lines.Length;

    //    string[][] levelBase = new string[rows][];
    //    for (int i = 0; i < lines.Length; i++)
    //    {
    //        // split experiment with "space" as a splitter in the text level
    //        //string[] stringsOfLine = lines[i].Split(""[0]);
    //        //levelBase[i] = stringsOfLine;

    //        // split every char in the text level
    //        string[] characters = new string[lines[i].Length];
    //        for (int j = 0; j < lines[i].Length; j++)
    //        {
    //            characters[j] = lines[i][j].ToString();
    //        }
    //        levelBase[i] = characters;
    //    }
    //    return levelBase;
    //}
}
