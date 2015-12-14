using UnityEngine;
using System.Collections;

// this script is used to adjust the camera's viewport according to the game window's current size and the desired aspect ratio

public class CameraRatio : MonoBehaviour
{
    // 320×256
    private const float RES = 5.0f / 4.0f;

    private float original_y;

    void Start ()
    {
        original_y = transform.position.y;

        // set the desired aspect ratio
        float targetAspect = RES;

        // determine the game window's current aspect ratio
        float currentAspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleHeight = currentAspect / targetAspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    void Update ()
    {
        transform.position = new Vector3(transform.position.x, original_y, transform.position.z);
    }
}
