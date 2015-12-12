using UnityEngine;
using System.Collections;

public class WallGeneration : MonoBehaviour {

    private float time = 0.0f;
    private float timer = 1f;
    private int row = 0;

    private float[] boundaryX = new float[] { -8.0f, 7.0f };

    private string[][] level;
	
    void Start()
    {
        string file = "Assets/Resources/Levels/Level_0.txt";
        level = ReadFile(file);
    }

    void Update()
    {
        //if (Input.GetButtonDown("Jump"))
        //    RenderWalls(level);
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

    void RenderRow(int row)
    {
        for (int j = 0; j < level[0].Length; j++)
        {
            if (level[row][j] == "1" || level[row][j] == "1\r")
            {
                Vector3 spawnPosition = new Vector3(j + boundaryX[0], 10, 0);
                GameObject wall = Instantiate(Resources.Load("Prefabs/Wall"), spawnPosition, Quaternion.identity) as GameObject;
            }
        }
    }


    string[][] ReadFile(string file)
    {
        string fileText = System.IO.File.ReadAllText(file);
        string[] lines = fileText.Split("\n"[0]);
        int rows = lines.Length;

        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] stringsOfLine = lines[i].Split(" "[0]);
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }
}
