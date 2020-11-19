using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float gravity = 10;
    //Apply gravity
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody)
        {
            Vector2 dir = Vector3.Normalize(transform.position -other.transform.position);
            other.attachedRigidbody.AddForce( dir * gravity);
        }
    }
}
