using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Knight : MonoBehaviour {

    public Rigidbody[] knightComponents;
    public Controller_Horse horseController;

    void Awake() {
        knightComponents = GetComponentsInChildren<Rigidbody>();
    }

    void FixedUpdate() {

        if (GameManager.instance.gameCanRun) {
            foreach (Rigidbody component in knightComponents) {
                component.MovePosition(component.transform.position + horseController.transform.forward * Time.deltaTime * horseController.currentSpeed);
            }
        }
    }

    public void LethalCollision() {

        if (GameManager.instance.gameState == GameManager.GameState.SplitScreen) {
            GameManager.instance.PlayerDied(gameObject.transform.parent.GetComponent<SplitScreenPlayer>().playerNumber);
        }

        if (GameManager.instance.gameState == GameManager.GameState.Online) {
            gameObject.transform.parent.GetComponent<Player>().CmdPlayerDied();
        }
    }


}
