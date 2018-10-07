using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenPlayer : MonoBehaviour {

    public int playerNumber;
    public Color playerColor;
    public int playerWins;
    public GameObject bloodSplatter;

    private void Awake() {
        bloodSplatter = GameObject.Find("BloodFX");
    }

    void OnCollisionEnter(Collision collision) {
        Instantiate(bloodSplatter, transform.position, transform.rotation);

    }


    // Update is called once per frame
    void Update () {
    
    }
}
