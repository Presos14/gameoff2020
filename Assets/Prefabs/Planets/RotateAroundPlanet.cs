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
    void FixedUpdate()
    {
        if (rotate) {
            transform.RotateAround(planet.transform.position, Vector3.forward, speed * Time.deltaTime);

            // Rotate grounded objects as well (e.g. Rocket)
            if (gravityField != null) {
                foreach (Collider2D collider in gravityField.getColliders()) {
                    RocketController rocket = collider.GetComponent<RocketController>();
                    if (rocket != null && rocket.getIsGrounded()) {
                        rocket.transform.RotateAround(planet.transform.position, Vector3.forward, speed * Time.deltaTime);
                    }
                }
            }
        }
    }
}
