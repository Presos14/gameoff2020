using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float gravity;
    public float linearDrag;
    CircleCollider2D planetGravityCollider;

    float maxDistance;
    private List<Collider2D> colliders = new List<Collider2D>();
    public List<Collider2D> GetColliders () { return colliders; }

    public bool orbit;
    public bool decreaseWithDistance = true;
    public bool inverseGravity = false;
 
    void Start() {
        planetGravityCollider = GetComponent<CircleCollider2D>();
        maxDistance = planetGravityCollider.radius;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        other.attachedRigidbody.drag = linearDrag;
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }
 
    private void OnTriggerExit2D(Collider2D other) {
        other.attachedRigidbody.drag = 0;
        colliders.Remove(other);
        RocketController rocket = other.GetComponent<RocketController>();
        if (rocket != null) {
            // Restore planet rotation when Rocket leaves gravity field
            rocket.onTakeOffGravityFieldExit();
        }
    }

    void FixedUpdate() {

        foreach (Collider2D collider in colliders) {
            if (collider.name == "GravityField") {
                continue;
            }

            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            Vector2 relativePosition = ((Vector2) transform.position) - rb.position;
            float distance = relativePosition.magnitude;
    
            float gravityFactor = decreaseWithDistance ? (1 - (distance / maxDistance)) : 1;
 
            // Apply some force towards the ground for gravity
            Vector2 dir = relativePosition.normalized;
            Vector2 force = (inverseGravity ? -1 : 1) * collider.attachedRigidbody.drag * dir * gravity * gravityFactor;
            collider.attachedRigidbody.AddForce(force);
            Debug.DrawLine(rb.position, rb.position + force, Color.white);

            if (!orbit) continue;

            var left = Vector2.SignedAngle(rb.velocity, relativePosition) > 0;

            // Apply force perpendicular to the distance vector for rotation
            Vector2 side = new Vector2(-dir.y, dir.x).normalized;
            if (left) side *= -1;

            collider.attachedRigidbody.AddForce(side * gravity * gravityFactor);
            Debug.DrawLine(rb.position, rb.position + side * gravity * gravityFactor, Color.white);
            Debug.Log("Side force: " + side * gravity * gravityFactor);
        }
    }

    public List<Collider2D> getColliders() {
        return colliders;
    }
}
