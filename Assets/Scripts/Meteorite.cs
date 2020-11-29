using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    private int rotateDir;
    private float timer;
    private float timerCap = 15f;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rotateDir = Random.Range(0,2) == 0 ? 1 : -1;
    }

    public void OnEnable()
    {
        timer = timerCap;
    }
    // Update is called once per frame
    void Update()
    {
        if(timer <= 0f)
        {
            gameObject.SetActive(false);
        }
        transform.Rotate(rotateDir * Vector3.forward * (50 * Time.deltaTime));
        timer -= Time.deltaTime;
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
