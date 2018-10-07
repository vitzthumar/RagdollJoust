using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public AudioClip[] clips;
    private int clipIndex;
    private AudioSource audioSource;
    float timeLeft = 6.0f;


    private void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();

    }
    void Update() {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0) {
            clipIndex = Random.Range(0, clips.Length - 1);
            audioSource.clip = clips[clipIndex];
            audioSource.PlayOneShot(clips[clipIndex], 1.0f);
            timeLeft = Random.Range(2.0f, 7.0f);
        }

    }
}