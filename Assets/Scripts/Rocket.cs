using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public ParticleSystem mainThrustEffect;
    public ParticleSystem leftThrustEffect;
    public ParticleSystem rightThrustEffect;
    ConstantForce2D thrust;
    Rigidbody2D rigidBody;

    int mainThrustForce = 3;
    int sideThrustForce = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        thrust = GetComponent<ConstantForce2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            thrust.relativeForce = Vector2.up * mainThrustForce;
            ParticleSystem.EmissionModule em = mainThrustEffect.emission;
            em.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            thrust.relativeForce = Vector2.zero;
            if (mainThrustEffect.isEmitting) {
                ParticleSystem.EmissionModule em = mainThrustEffect.emission;
                em.enabled = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            if (leftThrustEffect.isEmitting) {
                ParticleSystem.EmissionModule em = leftThrustEffect.emission;
                em.enabled = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            if (rightThrustEffect.isEmitting) {
                ParticleSystem.EmissionModule em = rightThrustEffect.emission;
                em.enabled = false;
            }
        }  
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            Vector2 worldForcePosition = transform.TransformPoint(new Vector2(-0.5f,0f));
            rigidBody.AddForceAtPosition(transform.up * sideThrustForce, worldForcePosition);
            ParticleSystem.EmissionModule em = leftThrustEffect.emission;
            em.enabled = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            Vector2 worldForcePosition = transform.TransformPoint(new Vector2(0.5f,0f));
            rigidBody.AddForceAtPosition(transform.up * sideThrustForce, worldForcePosition);
            ParticleSystem.EmissionModule em = rightThrustEffect.emission;
            em.enabled = true;
        }
    }
}
