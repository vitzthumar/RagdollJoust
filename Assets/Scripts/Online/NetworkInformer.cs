using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkInformer : NetworkBehaviour {

    public static NetworkInformer instance = null;

    [SyncVar]
    public bool pauseGame = true;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void CheckToStart() {


        bool gameCanStart = true;
        GameObject[] onlinePlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject onlinePlayer in onlinePlayers) {
            if (onlinePlayer.GetComponent<Player>().playerReady == false) {
                gameCanStart = false;
            }
        }

        // if the condition was met and more than one player is in game
        if (gameCanStart && onlinePlayers.Length > 1) {
            // start a coroutine that will begin the game after X seconds

        }

    }


    public void PlayerDied(int playerNumber) {
        GameManager.instance.PlayerDied(playerNumber);
    }

    private void Update() {
        if (pauseGame) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }

    }

}
