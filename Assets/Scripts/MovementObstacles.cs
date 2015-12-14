using UnityEngine;
using System.Collections;

public class MovementObstacles : MonoBehaviour {

    private float speed = 4.0f;
	
	void FixedUpdate () 
	{
        if (transform.position.y > -1.5) {
            //transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
            Vector3 destination = new Vector3(transform.position.x, transform.position.y - 1, 0);
            transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
        }
        else
        {
            if (gameObject.tag == "ground")
                transform.position = new Vector3(transform.position.x, 13, 0);
            else
                Destroy(gameObject);
        }
	}
}
