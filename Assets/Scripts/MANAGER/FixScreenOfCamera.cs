using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScreenOfCamera : MonoBehaviour {
void Start () {
    // set the desired aspect ratio (the values in this example are
    // hard-coded for 16:9, but you could make them into public
    // variables instead so you can set them at design time)
    float targetaspect =  9.0f/16.0f;

    // determine the game window's current aspect ratio
    float windowaspect =  (float)Screen.height/ (float)Screen.width;

    // current viewport height should be scaled by this amount
    float scaleheight =  targetaspect / windowaspect;

    // obtain camera component so we can modify its viewport
    Camera camera = GetComponent<Camera> ();

   

        Rect rect = camera.rect;

          
            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1- scaleheight)/2;
            camera.rect = rect;
     
   }
}
