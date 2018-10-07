using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller_Horse))]
public class InputController_Horse : MonoBehaviour {

    Controller_Horse horseController;

    // Use this for initialization
    void Start() {
        // get the horse controlller so that the scripts can communicate
        horseController = GetComponent<Controller_Horse>();
    }

    // Update is called once per frame
    void Update() {

        // get the first player's horizontal and vertical input
        Vector2 axisInputFirstPlayer = new Vector2(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1"));
        // do the same for the second player
        Vector2 axisInputSecondPlayer = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));

        // if the input's magnitude for either player is greater than 1, normalize it
        if (axisInputFirstPlayer.magnitude > 1) {
            axisInputFirstPlayer.Normalize();
        }
        if (axisInputSecondPlayer.magnitude > 1) {
            axisInputSecondPlayer.Normalize();
        }

        // pass the input based on which player gives
        if (transform.parent.GetComponent<SplitScreenPlayer>().playerNumber == 0) {
            horseController.axisInput = axisInputFirstPlayer;
            horseController.toggleInput = Input.GetButtonDown("Toggle1");
        } else {
            horseController.axisInput = axisInputSecondPlayer;
            horseController.toggleInput = Input.GetButtonDown("Toggle2");
        }
    }
}