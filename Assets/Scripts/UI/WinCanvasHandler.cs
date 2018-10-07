using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCanvasHandler : MonoBehaviour {
    public Text redWin;
    public Text blueWin;
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetWinner(int winner) {
        if (winner == 0) {
            redWin.gameObject.SetActive(true);
            blueWin.gameObject.SetActive(false);
        } else {
            blueWin.gameObject.SetActive(true);
            redWin.gameObject.SetActive(false);
        }
    }
}
