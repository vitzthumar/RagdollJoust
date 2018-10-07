using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public int playerNumber;
    public Color playerColor;
    public int playerWins;
    public GameObject bloodSplatter;

    // TESTING
    public bool dead = false;

    public bool playerReady = false;

    void Awake() {
        bloodSplatter = GameObject.Find("BloodFX");
        if (GameManager.instance.gameState == GameManager.GameState.Online) {
            GameObject[] onlinePlayers = GameObject.FindGameObjectsWithTag("Player");
            playerNumber = onlinePlayers.Length - 1;
        }


    }

    void OnCollisionEnter(Collision collision) {
            Instantiate(bloodSplatter, transform.position, transform.rotation);
        
    }


    // Update is called once per frame
    void Update() {

        // this sets the player's ready status to true
        // SHOULD BE REPLACED WITH SOME KIND OF UI BUTTON
        if (GameManager.instance.gameState == GameManager.GameState.Online && isLocalPlayer) {
            // check the input
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                CmdPlayerReady(true);
            }
        }
	}

    [Command]
    public void CmdPlayerReady(bool ready) {
        RpcPlayerReady(ready);
    }
    [ClientRpc]
    void RpcPlayerReady(bool ready) {
        playerReady = ready;
        NetworkInformer.instance.CheckToStart();
    }

    [Command]
    public void CmdPlayerDied() {
        RpcPlayerDied();
    }

    [ClientRpc]
    void RpcPlayerDied() {
        NetworkInformer.instance.PlayerDied(playerNumber);
    }
}
