using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject _pauseUI;

    private void Start() {
        _pauseUI.SetActive(false);
    }

    public void Pause() {
        SoundManager.instance.PlayClick();
        _pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume() {
        SoundManager.instance.PlayClick();
        _pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Go2Main() {
        Time.timeScale = 1;

        SoundManager.instance.PlayGameOver();
        GameManager._gameOver = true;

    }
	
}
