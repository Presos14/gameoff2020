using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetWithProjectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    CircleCollider2D circleCollider2D;
    public GameObject meteoritePrefab;
    public float meteoriteRate = 0.3f;
    public float minLaunchForce = 100;
    public float maxLaunchForce = 400;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        InvokeRepeating("launchMeteorite", 2.0f, meteoriteRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * (50 * Time.deltaTime));
    }

    void launchMeteorite() {
        Vector2 direction = Random.insideUnitCircle.normalized;

        GameObject projectileObject = Instantiate(meteoritePrefab, rigidbody2d.position + direction * circleCollider2D.radius, Quaternion.identity);

        projectileObject.transform.localScale *= Random.Range(1f, 2f); 

        Meteorite meteorite = projectileObject.GetComponent<Meteorite>();
        meteorite.Launch(direction, Random.Range(minLaunchForce, maxLaunchForce));
    }
}
