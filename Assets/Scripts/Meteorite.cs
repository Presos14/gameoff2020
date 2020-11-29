using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    private int rotateDir;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rotateDir = Random.Range(0,2) == 0 ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 500.0f)
        {
            Destroy(gameObject);
        }
        transform.Rotate(rotateDir * Vector3.forward * (50 * Time.deltaTime));
    }
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RocketController e = other.collider.GetComponent<RocketController>();
        if (e != null) {
            e.hit();
        }
        
        Destroy(gameObject);
    }
}
