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

    void setParticleEmission(ParticleSystem.EmissionModule emission, bool enabled) {
        emission.enabled = enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            thrust.relativeForce = Vector2.up * mainThrustForce;
            setParticleEmission(mainThrustEffect.emission, true);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            thrust.relativeForce = Vector2.zero;
            if (mainThrustEffect.isEmitting) {
                setParticleEmission(mainThrustEffect.emission, false);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            if (leftThrustEffect.isEmitting) {
                setParticleEmission(leftThrustEffect.emission, false);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            if (rightThrustEffect.isEmitting) {
                setParticleEmission(rightThrustEffect.emission, false);
            }
        }  
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            Vector2 worldForcePosition = transform.TransformPoint(new Vector2(-0.5f,0f));
            rigidBody.AddForceAtPosition(transform.up * sideThrustForce, worldForcePosition);
            setParticleEmission(leftThrustEffect.emission, true);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            Vector2 worldForcePosition = transform.TransformPoint(new Vector2(0.5f,0f));
            rigidBody.AddForceAtPosition(transform.up * sideThrustForce, worldForcePosition);
            setParticleEmission(rightThrustEffect.emission, true);
        }
    }
}
