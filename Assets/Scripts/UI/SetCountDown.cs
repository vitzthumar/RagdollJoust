using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCountDown : MonoBehaviour {

    public GameObject CountDownCanvas;

    private void Awake() {
        if (GameManager.instance.gameState == GameManager.GameState.SplitScreen) {
            this.gameObject.GetComponent<Animator>().Play("countdown");
        }

        if (GameManager.instance.gameState == GameManager.GameState.Online) {
            GameManager.instance.countDownDone = true;
            CountDownCanvas.SetActive(false);
        }
        GameManager.instance.countDownDone = false;
    }
    public void SetCountDownNow() {

        GameManager.instance.countDownDone = true;
    }
}
