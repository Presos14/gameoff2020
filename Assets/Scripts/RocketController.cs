using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RocketController : MonoBehaviour
{
    public ParticleSystem mainThrustEffect;
    public ParticleSystem leftThrustEffect;
    public ParticleSystem rightThrustEffect;
    ConstantForce2D thrust;
    Rigidbody2D rigidBody;

    int mainThrustForce = 3;
    int sideThrustForce = 2;

    float maxFuel = 1000;
    float fuel;

    public bool isGrounded;
    public bool takingOff;
    private RotateAroundPlanet groundPlanetRotateBehavior;
    private ShakeBehavior shakeBehavior;
    
    // Start is called before the first frame update
    void Start()
    {
        thrust = GetComponent<ConstantForce2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        shakeBehavior = GetComponent<ShakeBehavior>();

        fuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            if (fuel > 0) {
                thrust.relativeForce = Vector2.up * mainThrustForce;
                setParticleEmission(mainThrustEffect.emission, true);
                decreaseFuel();
                if (isGrounded) {
                    // Stop planet rotation while rocket is taking off
                    groundPlanetRotateBehavior.rotate = false;
                }
                shakeBehavior.GenerateImpulse(takingOff ? 1f : 0.4f);
            } else {
                thrust.relativeForce = Vector2.zero;
                if (mainThrustEffect.isEmitting) {
                    setParticleEmission(mainThrustEffect.emission, false);
                }
            }
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
            if (fuel > 0) {
                Vector2 worldForcePosition = transform.TransformPoint(new Vector2(-0.5f,0f));
                rigidBody.AddForceAtPosition(transform.up * sideThrustForce, worldForcePosition);
                setParticleEmission(leftThrustEffect.emission, true);
                decreaseFuel();
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (fuel > 0) {
                Vector2 worldForcePosition = transform.TransformPoint(new Vector2(0.5f,0f));
                rigidBody.AddForceAtPosition(transform.up * sideThrustForce, worldForcePosition);
                setParticleEmission(rightThrustEffect.emission, true);
                decreaseFuel();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("surface")) {
            isGrounded = true;
            takingOff = true;
            groundPlanetRotateBehavior = collision.gameObject.GetComponent<RotateAroundPlanet>();
            groundPlanetRotateBehavior.rotate = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("surface")) {
            isGrounded = false;
        }
    }

    public void onTakeOffGravityFieldExit() {
        if (groundPlanetRotateBehavior != null) {
            groundPlanetRotateBehavior.rotate = true;
            groundPlanetRotateBehavior = null;
            takingOff = false;
        }
    }

    void setParticleEmission(ParticleSystem.EmissionModule emission, bool enabled) {
        emission.enabled = enabled;
    }

    void decreaseFuel() {
        fuel -= 50f * Time.deltaTime;
        UIController.instance.SetFuelBarValue(fuel / maxFuel);

        if (fuel < 0) {
            fuel = 0;
            WorldController.instance.gameOver("Gone adrift... Press to retry!");
        }
    }
}
