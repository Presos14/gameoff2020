using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGreen : MonoBehaviour
{

    public int rotateDir = 1;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateDir * Vector3.forward * (100 * Time.deltaTime));
    }
}
