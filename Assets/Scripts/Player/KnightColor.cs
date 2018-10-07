using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightColor : MonoBehaviour {

    public GameObject player;
    public MeshRenderer[] knightComponents;
    public Material[] knightColors;

    void Start() {


        // give a knight its color based on which position it spawns at
        knightComponents = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer knightComponent in knightComponents) {

            if (GameManager.instance.gameState == GameManager.GameState.SplitScreen) {
                knightComponent.material = knightColors[player.GetComponent<SplitScreenPlayer>().playerNumber];
            }

            if (GameManager.instance.gameState == GameManager.GameState.Online) {
                knightComponent.material = knightColors[player.GetComponent<Player>().playerNumber];
            }
            
        }
    }

}
