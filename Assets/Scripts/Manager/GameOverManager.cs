using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public Text _gameOverText;
    public GameObject _gameOverView;

    public Text _gameClearText;
    public GameObject _gameClearView;

    private CanvasGroup _gameOverCanvasGroup;
    private CanvasGroup _gameClearCanvasGroup;

    private void Start() {
        _gameOverText.text = "Game Over";
        _gameOverCanvasGroup = _gameOverView.GetComponent<CanvasGroup>();
        _gameOverCanvasGroup.alpha = 0;
        _gameOverView.SetActive(false);

        _gameClearText.text = "{ Game Clear }";
        _gameClearCanvasGroup = _gameClearView.GetComponent<CanvasGroup>();
        _gameClearCanvasGroup.alpha = 0;
        _gameClearView.SetActive(false);

    }

    private void Update() {
        if (GameManager._gameOver) { 
            StartCoroutine("OverViewAlphaControl");
            PlayerPrefs.SetFloat("HP", 20);
        }
        if (!BossStageManager._isBossAlive && GameManager.level == 10) {
            StartCoroutine("ClearViewAlphaControl");
            PlayerPrefs.SetInt("BestStage", GameManager.level);
            PlayerPrefs.SetFloat("HP", 20);
        }
    }

    IEnumerator OverViewAlphaControl() {

        _gameOverView.SetActive(true);
        float i = 0;
        while (i < 1) {
            i += Time.deltaTime * 0.5f;
            _gameOverCanvasGroup.alpha = i;

            yield return null;
        }

    }

    IEnumerator ClearViewAlphaControl() {

        _gameClearView.SetActive(true);
        float i = 0;
        while (i < 1) {
            i += Time.deltaTime * 0.5f;
            _gameClearCanvasGroup.alpha = i;

            yield return null;  
        }

    }
}
