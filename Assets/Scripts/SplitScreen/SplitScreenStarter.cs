using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenStarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if (GameManager.instance.gameState == GameManager.GameState.SplitScreen) {
            GameManager.instance.StartSplitScreenGame();
        }	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
