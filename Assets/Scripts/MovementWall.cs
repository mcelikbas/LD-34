using UnityEngine;
using System.Collections;

public class MovementWall : MonoBehaviour {

    public float speed = 5.0f;
	
	void Update () 
	{
        if (transform.position.y > -3)
            transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
        else
            Destroy(gameObject);
	}
}
