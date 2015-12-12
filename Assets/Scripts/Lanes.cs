using UnityEngine;
using System.Collections;

public class Lanes : MonoBehaviour
{
    public Material lineMat;
    
    void DrawLanes()
    {
        
        for (float i = -7f; i < 8f; i++)
        {
            Vector3 p1 = new Vector3(i, 14, 0);
            Vector3 p2 = new Vector3(i, -2, 0);

            GL.Begin(GL.LINES);
            lineMat.SetPass(0);
            GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
            GL.Vertex3(p1.x, p1.y, p1.z);
            GL.Vertex3(p2.x, p2.y, p2.z);
            GL.End();
        }
    }

    // To show the lines in the game window whne it is running
    void OnPostRender()
    {
        DrawLanes();
    }

    // To show the lines in the editor
    void OnDrawGizmos()
    {
        DrawLanes();
    }
}