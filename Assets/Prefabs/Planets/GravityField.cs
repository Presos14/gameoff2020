using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float gravity = 10;
    public float linearDrag = 0.4f;
    CircleCollider2D planetGravityCollider;

    float maxDistance;
    private List<Collider2D> colliders = new List<Collider2D>();
    public List<Collider2D> GetColliders () { return colliders; }

    public bool orbit;
 
    void Start() {
        planetGravityCollider = GetComponent<CircleCollider2D>();
        maxDistance = planetGravityCollider.radius;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Set drag! " + linearDrag);
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
        if (!orbit) return;

        foreach (Collider2D collider in colliders) {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            Vector2 relativePosition = ((Vector2) transform.position) - rb.position;
            float distance = relativePosition.magnitude;
    
            float gravityFactor = 1 - (distance / maxDistance);
 
            Vector2 dir = relativePosition.normalized;
            // Apply some force towards the ground for gravity
            collider.attachedRigidbody.AddForce(collider.attachedRigidbody.drag * dir * gravity * gravityFactor);
            Debug.DrawLine(rb.position, rb.position + collider.attachedRigidbody.drag * dir * gravity * gravityFactor, Color.white);

            var left = Vector2.SignedAngle(rb.velocity, relativePosition) > 0;

            // Apply force perpendicular to the distance vector for rotation
            Vector2 side = new Vector2(-dir.y, dir.x).normalized;
            if (left) side *= -1;

            collider.attachedRigidbody.AddForce(side * gravity * gravityFactor);
            Debug.DrawLine(rb.position, rb.position + side * gravity * gravityFactor, Color.white);
        }
    }

    public List<Collider2D> getColliders() {
        return colliders;
    }
}
