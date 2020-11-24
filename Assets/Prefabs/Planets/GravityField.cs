using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float gravity = 10;
    CircleCollider2D planetGravityCollider;

    float maxDistance;
    private List<Collider2D> colliders = new List<Collider2D>();
    public List<Collider2D> GetColliders () { return colliders; }
 
    void Start() {
        planetGravityCollider = GetComponent<CircleCollider2D>();
        maxDistance = planetGravityCollider.radius;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }
 
    private void OnTriggerExit2D(Collider2D other) {
        colliders.Remove(other);
        RocketController rocket = other.GetComponent<RocketController>();
        if (rocket != null) {
            // Restore planet rotation when Rocket leaves gravity field
            rocket.onTakeOffGravityFieldExit();
        }
    }

    //Apply gravity
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody)
        {
            Vector2 relativePosition = transform.position - other.transform.position;
            float distance = Mathf.Clamp(relativePosition.magnitude, 0, maxDistance);
    
            //the force of gravity will reduce by the distance squared
            //float gravityFactor = 1 - (Mathf.Sqrt(distance) / Mathf.Sqrt(maxDistance));
            float gravityFactor = 1 - (distance / maxDistance);
 
            Vector2 dir = Vector3.Normalize(relativePosition);
            Debug.Log(dir * gravity * gravityFactor);
            other.attachedRigidbody.AddForce( dir * gravity * gravityFactor);
        }
    }

    public List<Collider2D> getColliders() {
        return colliders;
    }
}
