using UnityEngine;
using System.Collections;
using System;

public class WallGeneration : MonoBehaviour
{

    private const float TIMER_WALLS_TOUCHING = 0.24f;

    private float time = 0.0f;
    private float timer = TIMER_WALLS_TOUCHING;
    private int row = 0;

    private float[] boundaryX = new float[] { -8.0f, 7.0f };

    //private string[][] level;

    public Texture2D levelTexture;
    private int[,] level;

    void Start()
    {
        //string file = "Assets/Resources/Levels/Level_0.txt";
        //level = ReadFile(file);

        level = ReadImage(levelTexture);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            ResetLevel();
        if (row < level.Length)
        {
            time += Time.deltaTime;
            if (time > timer)
            {
                RenderRow(row);
                row++;
                time = 0.0f;
            }
        }
    }

    void ResetLevel()
    {
        row = 0;
        time = 0.0f;
    }

    void RenderRow(int row)
    {
        for (int j = 0; j < level.GetLength(1); j++)
        {
            //if (level[row][j] == "1" || level[row][j] == "1\r")
            if (level[row,j] == 0)
            {
                Vector3 spawnPosition = new Vector3(j + boundaryX[0], 12, 0);
                GameObject wall = Instantiate(Resources.Load("Prefabs/Wall"), spawnPosition, Quaternion.identity) as GameObject;
            }
        }
    }

    // level reading based on an image
    int[,] ReadImage(Texture2D texture)
    {
        int[,] levelBase = new int[texture.height, texture.width];
        for (int i = 0; i < texture.height; i++)
        {
            for (int j = 0; j < texture.width; j++)
            {
                if (levelTexture.GetPixel(j, i).Equals(Color.black))
                    levelBase[i,j] = 0;
                else
                    levelBase[i,j] = 1;
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
