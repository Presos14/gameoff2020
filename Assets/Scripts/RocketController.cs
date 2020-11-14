using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public ParticleSystem mainThrustEffect;
    public ParticleSystem leftThrustEffect;
    public ParticleSystem rightThrustEffect;
    ConstantForce2D thrust;
    Rigidbody2D rigidBody;

    int mainThrustForce = 3;
    int sideThrustForce = 2;

    float maxFuel = 250;
    float fuel;
    
    // Start is called before the first frame update
    void Start()
    {
        thrust = GetComponent<ConstantForce2D>();
        rigidBody = GetComponent<Rigidbody2D>();

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

    void setParticleEmission(ParticleSystem.EmissionModule emission, bool enabled) {
        emission.enabled = enabled;
    }

    void decreaseFuel() {
        fuel -= 50f * Time.deltaTime;
        UIController.instance.SetFuelBarValue(fuel / maxFuel);

        if (fuel < 0) {
            fuel = 0;
            UIController.instance.gameOverText.gameObject.SetActive(true);
            WorldController.instance.state = WorldController.WorldState.GameOver;
        }
    }
}
