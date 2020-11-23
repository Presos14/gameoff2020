using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPlanet : MonoBehaviour
{
    public GameObject planet;
    public GravityField gravityField;
    public float speed;
    public bool rotate = true;

    // Update is called once per frame
    void Update()
    {
        if (rotate) {
            transform.RotateAround(planet.transform.position, Vector3.forward, speed * Time.deltaTime);

            // Rotate grounded objects as well (e.g. Rocket)
            if (gravityField != null) {
                foreach (Collider2D collider in gravityField.getColliders()) {
                    RocketController rocket = collider.GetComponent<RocketController>();
                    Rigidbody2D rocketRigidBody = rocket.GetComponent<Rigidbody2D>();
                    Rigidbody2D planetRigidBody = gameObject.GetComponent<Rigidbody2D>();
                    if (rocket != null && rocket.isGrounded) {
                        rocket.transform.RotateAround(planet.transform.position, Vector3.forward, speed * Time.deltaTime);
                    }
                }
            }
        }
    }
}
