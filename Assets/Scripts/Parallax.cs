using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    private float lengthx, lengthy;
    private Vector2 pos;
    public float parallaxEffect;


    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        lengthx = 3*GetComponent<SpriteRenderer>().bounds.size.x;
        lengthy = 3*GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject cam = ((CinemachineVirtualCamera) CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera).gameObject;

        Vector2 tempPos = cam.transform.position * (1 - parallaxEffect);
        Vector2 dist = cam.transform.position * parallaxEffect;

        transform.position = pos + dist;
        if (tempPos.x > pos.x + lengthx) {
            pos.x += lengthx;
        } else if (tempPos.x < pos.x - lengthx) {
            pos.x -= lengthx;
        }

        if (tempPos.y > pos.y + lengthy) {
            pos.y += lengthy;
        } else if (tempPos.y < pos.y - lengthy) {
            pos.y -= lengthy;
        }
    }
}