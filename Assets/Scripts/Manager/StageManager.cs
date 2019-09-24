using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    private int _lastStage;
    private int _bestStage;

    private void Update() {
        if(GameManager._gameOver) {
            BestStage();
        }
    }

    public void BestStage() {
        _lastStage = PlayerPrefs.GetInt("LastStage");
        _bestStage = PlayerPrefs.GetInt("BestStage");

        if(_bestStage == 0) {
            PlayerPrefs.SetInt("BestStage", _lastStage);
        } else {
            if(_bestStage < _lastStage) {
                PlayerPrefs.SetInt("BestStage", _lastStage);
            }
        }
    }
   
}
