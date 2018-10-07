using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

    public GameObject knight;

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.CompareTag("Killzone")) {
            knight.GetComponent<Controller_Knight>().LethalCollision();
        }
    }
}
