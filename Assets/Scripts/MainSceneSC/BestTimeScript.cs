using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTimeScript : MonoBehaviour {

    public Text _bestPlayTimeLabel;

    private int _bestMinute;
    private float _bestSecond;
    //private void Awake() { // 게임 기록 초기화 하실려면 사용하세요!
    //    PlayerPrefs.SetInt("BestPlayTime_Minute", 0);
    //    PlayerPrefs.SetFloat("BestPlayTime_Second", 0);
    //    PlayerPrefs.SetInt("BestStage", 0);
    //}

    private void Start() {
        _bestMinute = PlayerPrefs.GetInt("BestPlayTime_Minute");
        _bestSecond = PlayerPrefs.GetFloat("BestPlayTime_Second");

        if(_bestMinute == 0) {
            if(_bestSecond == 0) {
                _bestPlayTimeLabel.text = "00 : 00";
            } else {
                _bestPlayTimeLabel.text = "00 : " + _bestSecond;
            }
        } else if(_bestSecond == 0) {
            if(_bestMinute < 10) {
                _bestPlayTimeLabel.text = "0" + _bestMinute + " : 00"; 
            } else {
                _bestPlayTimeLabel.text = _bestMinute + " : 00";
            }
        } else {
            if(_bestMinute < 10) {
                if(_bestSecond < 10) {
                    _bestPlayTimeLabel.text = "0" + _bestMinute + " : 0" + _bestSecond;
                } else {
                    _bestPlayTimeLabel.text = "0" + _bestMinute + " : " + _bestSecond;
                }
            } else {
                if (_bestSecond < 10) {
                    _bestPlayTimeLabel.text = _bestMinute + " : 0" + _bestSecond;
                } else {
                    _bestPlayTimeLabel.text = _bestMinute + " : " + _bestSecond;
                }
            }
        }
        
    }
}
