using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject worldCamera;
    public GameObject rocketCamera;

    // Start is called before the first frame update
    void Start()
    {
        worldCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
