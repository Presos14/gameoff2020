using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float gravity = 10;
    private List<Collider2D> colliders = new List<Collider2D>();
    public List<Collider2D> GetColliders () { return colliders; }
 
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
            Vector2 dir = Vector3.Normalize(transform.position -other.transform.position);
            other.attachedRigidbody.AddForce( dir * gravity);
        }
    }

    public List<Collider2D> getColliders() {
        return colliders;
    }
}
