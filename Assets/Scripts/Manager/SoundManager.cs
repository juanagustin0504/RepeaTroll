using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip _soundCollision;
    public AudioClip _soundClick;
    public AudioClip _soundRoll;

    public AudioClip _soundGameOver;


    AudioSource _audioSource;

    public static SoundManager instance;

    private void Awake() {
        if (SoundManager.instance == null)
            SoundManager.instance = this;
    }

    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();

	}
    

    public void PlayCollsion() {
        _audioSource.PlayOneShot(_soundCollision);
    }

    public void PlayClick() {
        _audioSource.PlayOneShot(_soundClick);
    }

    public void PlayRoll() {
        _audioSource.PlayOneShot(_soundRoll);
    }

    public void PlayGameOver() {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_soundGameOver);
    }
	
	
}
