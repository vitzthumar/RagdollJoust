using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controller_Horse : NetworkBehaviour {

    Rigidbody horseRigidbody;
    public LanceController lanceController;

    [HideInInspector] public Vector2 axisInput;
    [HideInInspector] public bool toggleInput;
    // speed
    public float minimumSpeed = 10;
    public float baseSpeed = 16;
    public float maximumSpeed = 22;
    public float speedModifier = .05f;
    public float speedRegulator = .015f;
    public float currentSpeed;

    // rotation
    public float baseRotationSpeed = 50f;
    private Vector3 rotationVelocity;

    // movement will be controlled first
    private bool toggleLanceOn = false;

    // Use this for initialization
    void Awake () {
        horseRigidbody = gameObject.GetComponent<Rigidbody>();
        horseRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // set the initial speed of the horse to the base speed
        currentSpeed = baseSpeed;
        // only apply the rotational speed to the y-axis
        rotationVelocity = new Vector3(0, baseRotationSpeed, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!GameManager.instance.gameCanRun) {
            return;
        }

        // the player will only be able to move his lance or move his horse, not both at the same time
        if (toggleInput) {
            toggleLanceOn = !toggleLanceOn;
        }
        if (toggleLanceOn) {
            ControlLance();
        } else {
            ControlMovementAndFrequency();
        }

        // if movement is being controlled and the y-axis is not 0, do not regulate the speed
        if (!toggleLanceOn && axisInput.y != 0) {
        } else {
            currentSpeed = RegulateSpeed();
        }

        // move the horse in space
        horseRigidbody.MovePosition(horseRigidbody.transform.position + horseRigidbody.transform.forward * Time.deltaTime * currentSpeed);
    }

    // Move the lance, which can be found on the LanceController script
    private void ControlLance() {
        lanceController.UpdateLance(axisInput);
    }

    // Move the horse
    private void ControlMovementAndFrequency() {

        horseRigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // move the horse by a small amount in whichever direction is dictated by the axis input
        Quaternion deltaRotation = Quaternion.Euler(rotationVelocity * Time.deltaTime * axisInput.x);
        horseRigidbody.MoveRotation(horseRigidbody.transform.rotation * deltaRotation);

        horseRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // increase or decrease the speed based on which button was pressed
        if (axisInput.y != 0) {
            // speed
            if (axisInput.y > 0 && currentSpeed <= maximumSpeed) {
                currentSpeed += speedModifier * axisInput.y;
            }
            if (axisInput.y < 0 && currentSpeed >= minimumSpeed) {
                currentSpeed += speedModifier * axisInput.y;
            }
        }
    }

    // Method used to regulate an offset speed back to its base value
    private float RegulateSpeed() {

        if (currentSpeed > baseSpeed) {
            // slowly decrease the current speed back to the base speed
            currentSpeed -= speedRegulator;
        }
        if (currentSpeed < baseSpeed) {
            // slowly increase the current speed back to the base speed
            currentSpeed += speedRegulator;
        }

        // return the speed which the horse will use to move forward in space
        return currentSpeed;
    }

    void LateUpdate() {
        
    }
}
