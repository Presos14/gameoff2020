using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RocketController : MonoBehaviour
{
    public ParticleSystem mainThrustEffect;
    public ParticleSystem leftThrustEffect;
    public ParticleSystem rightThrustEffect;
    public GameObject accelerationSound;
    public GameObject stopAccelerationSound;
    public GameObject goalIndicator;
    public GameObject center;
    ConstantForce2D thrust;
    Rigidbody2D rigidBody;
    private bool thrusted = false;

    int mainThrustForce = 30;
    int sideThrustForce = 1;

    public float maxFuel = 1000;
    float fuel;
    bool emptyFuel;

    bool isGrounded;
    bool takingOff;
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

    void onenable()
    {
        accelerationSound.SetActive(false);
        stopAccelerationSound.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (WorldController.instance.getState() != WorldController.WorldState.Running && LevelCompleteCollider.instance != null) 
        {
            goalIndicator.SetActive(false);
            return;
        }else
            goalIndicator.SetActive(true);

        thrusted = false;
        WorldController.instance.checkCameraNeedsUpdate(transform.position);

        if (Input.GetKey(KeyCode.Space)) {
            if (fuel > 0) {
                thrust.relativeForce = Vector2.up * mainThrustForce;
                setParticleEmission(mainThrustEffect.emission, true);
                decreaseFuel(true);
                if (isGrounded) {
                    // Stop planet rotation while rocket is taking off
                    groundPlanetRotateBehavior.rotate = false;
                }
                shakeBehavior.GenerateImpulse(takingOff ? 1f : 0.4f);
                thrusted = true;
            } else {
                thrust.relativeForce = Vector2.zero;
                if (mainThrustEffect.isEmitting) {
                    setParticleEmission(mainThrustEffect.emission, false);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            if (emptyFuel) {
                gameOver();
            }
            thrust.relativeForce = Vector2.zero;
            if (mainThrustEffect.isEmitting) {
                setParticleEmission(mainThrustEffect.emission, false);
            }
            if (fuel <= 0) {
                fuel = 0;
                emptyFuel = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
            if (leftThrustEffect.isEmitting) {
                setParticleEmission(leftThrustEffect.emission, false);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
            if (rightThrustEffect.isEmitting) {
                setParticleEmission(rightThrustEffect.emission, false);
            }
        }

        if(accelerationSound.activeSelf==false && thrusted) 
        {
            accelerationSound.SetActive(true);
            stopAccelerationSound.SetActive(false);
        }
        if(accelerationSound.activeSelf==true && stopAccelerationSound.activeSelf == false  && !thrusted) 
        {
            accelerationSound.SetActive(false);
            stopAccelerationSound.SetActive(true);
        }
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
            if (fuel > 0) {
                Vector2 worldForcePosition = transform.TransformPoint(new Vector2(-0.5f,0f));
                rigidBody.AddForceAtPosition(transform.up * sideThrustForce, worldForcePosition);
                setParticleEmission(leftThrustEffect.emission, true);
                decreaseFuel(false);
            }
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
            if (fuel > 0) {
                Vector2 worldForcePosition = transform.TransformPoint(new Vector2(0.5f,0f));
                rigidBody.AddForceAtPosition(transform.up * sideThrustForce, worldForcePosition);
                setParticleEmission(rightThrustEffect.emission, true);
                decreaseFuel(false);
            }
        }
        SetGoalIndicatorPosition();
    }

    public void SetGoalIndicatorPosition()
    {
        Vector2 direction = LevelCompleteCollider.instance.gameObject.transform.position - center.transform.position;
        direction.Normalize();
        Vector2 point = (Vector2)center.transform.position + direction * 1f;
        //Debug.Log("point:" + point);
        goalIndicator.transform.position = point;
        goalIndicator.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
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

    void decreaseFuel(bool main) {
        fuel -= (main ? 100f : 25f) * Time.deltaTime;
        UIController.instance.SetFuelBarValue(fuel / maxFuel);
    }

    void gameOver() {
        WorldController.instance.gameOver("Gone adrift... Press to retry!");
    }

    public void hit() {
        shakeBehavior.GenerateImpulse(2f);
    }

    public bool getIsGrounded() {
        return isGrounded;
    }
}
