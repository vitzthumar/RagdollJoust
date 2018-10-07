using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    Camera sceneCamera;
    public Camera playerCamera;
    NetworkStartPosition[] spawnPoints;

    // Use this for initialization
    void Start () {

        spawnPoints = GameObject.FindObjectsOfType<NetworkStartPosition>();

        if (GameManager.instance.gameState == GameManager.GameState.Online) {
            GameManager.instance.AddOnlinePlayer();

            // reset all of the online players' ready status to false when a new player joins
            GameObject[] onlinePlayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject onlinePlayer in onlinePlayers) {
                onlinePlayer.GetComponent<Player>().CmdPlayerReady(false);
            }


            gameObject.transform.position = spawnPoints[GetComponent<Player>().playerNumber].transform.position;
            gameObject.transform.rotation = spawnPoints[GetComponent<Player>().playerNumber].transform.rotation;
        }

        
        // DISABLE MOVEMENT UNTIL ALL OF THE PLAYERS HAVE CONNECTED TO THE GAME
        // DO NOT MOVE THE PLAYERS UNTIL THIS IS TRUE
       
        if (!isLocalPlayer) {

            this.gameObject.GetComponentInChildren<InputController_Horse_O>().enabled = false;

            playerCamera.gameObject.SetActive(false);

        } else {
        
            sceneCamera = Camera.main;
            if (sceneCamera != null) {
                sceneCamera.gameObject.SetActive(false);
            }
        }	
	}

    void OnDisable() {
        if (sceneCamera != null) {
            sceneCamera.gameObject.SetActive(true);
        }
        
    }
}
