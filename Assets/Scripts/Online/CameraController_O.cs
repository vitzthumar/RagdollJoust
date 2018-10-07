using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_O : MonoBehaviour {

    public Camera playerCam;
    private float startingYPosition;
    public float startingYRotation = 8f;
    public GameObject horse;
    public GameObject knight;

    public float baseDistance = 9f;
    public float distanceModifier = 6f;
    public float currentDistance;

    // the height we want the camera to be above the target
    public float height = 8.75f;
    // damping applied to the rotation
    //public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    // Use this for initialization
    void Awake() {

        playerCam.enabled = true;
        startingYPosition = horse.transform.position.y;

        currentDistance = baseDistance;
    }

    // Update is called once per frame
    void LateUpdate() {

        // calculate the current rotation angles
        float wantedRotationAngle = horse.transform.eulerAngles.y;
        //float wantedHeight = playerHorse.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;

        // damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // set the position of the camera on the x-z plane the distance in meters behind the target
        transform.position = horse.transform.position;
        // calculate how far the camera should be from the player
        currentDistance = CalculateDistance();

        transform.position -= currentRotation * Vector3.forward * currentDistance;

        // set the height of the camera
        transform.position = new Vector3(transform.position.x, startingYPosition + height, transform.position.z);

        // look at the target, but disregard the y component
        Vector3 targetPostition = new Vector3(horse.transform.position.x, startingYRotation, horse.transform.position.z);
        transform.LookAt(targetPostition);
    }

    // Initialize a camera based on which game mode and how many players are playing
    public void InitializeCamera(int playerNumber) {

        // split the screens
        if (GameManager.instance.gameState == GameManager.GameState.SplitScreen) {
            if (playerNumber == 0) {
                playerCam.rect = new Rect(0, 0, .5f, 1);
            } else {
                playerCam.rect = new Rect(0.5f, 0, .5f, 1);
            }
        }

        // enable the camera corresponding to the active player when in online mode
        if (GameManager.instance.gameState == GameManager.GameState.Online) {

        }
    }

    // Calculate the distance the camera should be from the player depending on its current speed
    private float CalculateDistance() {

        float currentSpeed = horse.GetComponent<Controller_Horse_O>().currentSpeed;
        float baseSpeed = horse.GetComponent<Controller_Horse_O>().baseSpeed;

        float speedProportion = currentSpeed / baseSpeed;

        float modifiedDist = speedProportion * distanceModifier;

        if (speedProportion < 0) {
            return (baseDistance - modifiedDist);
        } else {
            return (baseDistance + modifiedDist);
        }

    }


}
