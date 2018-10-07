using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Knight_O : MonoBehaviour {

    public Rigidbody[] knightComponents;
    public Controller_Horse_O horseController;
    public GameObject player;

    void Awake() {
        knightComponents = GetComponentsInChildren<Rigidbody>();
    }

    void FixedUpdate() {

        // check to see if the game manager is allowing movement of any kind
        if (NetworkInformer.instance.pauseGame && GameManager.instance.gameState == GameManager.GameState.Online) {
            return;
        }

        foreach (Rigidbody component in knightComponents) {
            component.MovePosition(component.transform.position + horseController.transform.forward * Time.deltaTime * horseController.currentSpeed);
        }
    }

    public void LethalCollision() {

        if (GameManager.instance.gameState == GameManager.GameState.SplitScreen) {
            GameManager.instance.PlayerDied(gameObject.transform.parent.GetComponent<SplitScreenPlayer>().playerNumber);
        }

        if (GameManager.instance.gameState == GameManager.GameState.Online) {
            player.GetComponent<Player>().CmdPlayerDied();
        }
    }


}