using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller_Horse_O))]
public class InputController_Horse_O : MonoBehaviour {

    Controller_Horse_O horseController;

    // Use this for initialization
    void Start() {
        // get the horse controlller so that the scripts can communicate
        horseController = GetComponent<Controller_Horse_O>();
    }

    // Update is called once per frame
    void Update() {

        if (GameManager.instance.gameState == GameManager.GameState.SplitScreen) {
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

        // use this set of inputs if in online mode
        if (GameManager.instance.gameState == GameManager.GameState.Online) {

            // get the input from the horizontal and vertical axes
            Vector2 axisInput = new Vector2(Input.GetAxisRaw("HorizontalOnline"), Input.GetAxisRaw("VerticalOnline"));

            // if the input's magnitude is greater than 1, normalize it
            if (axisInput.magnitude > 1) {
                axisInput.Normalize();
            }
            // input for the movement axis
            horseController.axisInput = axisInput;

            // input for toggling between horse and lance movement
            horseController.toggleInput = Input.GetButtonDown("ToggleOnline");
        }
    }
}
