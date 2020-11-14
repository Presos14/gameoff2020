using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengthx, lengthy, xpos, ypos;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        xpos = transform.position.x;
        ypos = transform.position.y;
        lengthx = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthy = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float tempx = (cam.transform.position.x * (1 - parallaxEffect));
        float tempy = (cam.transform.position.y * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        float ydist = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(xpos + dist, ypos + ydist, transform.position.z);
        if (tempx > xpos + lengthx)
        {
            xpos += lengthx;
        }
        else if (tempx < xpos - lengthx)
        {
            xpos -= lengthx;
        }
        if (tempy > ypos + lengthy)
        {
            ypos += lengthy;
        }
        else if (ypos < ypos - lengthy)
        {
            ypos -= lengthy;
        }
    }
}