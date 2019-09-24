using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public Text _playTimeLabel;

    private float _timeCount;
    private int _timeMinute;
    private float _timeSecond;

    public static bool _isClear;

    private Player _player;
    private PlayerAttack _playerAttack;

    private bool _isPlayerAlive;

    private void Awake() {

        _player = FindObjectOfType<Player>();
        _playerAttack = FindObjectOfType<PlayerAttack>();
        GetTime();

        if (_player._playerHp == 0) {
            _isPlayerAlive = false;
        } else {
            _isPlayerAlive = true;
        }

    }
    
    private void Update() {

        PlayTime();

        Clear();
    }

    public void Clear() {
        if (_isClear) {
            PlayerPrefs.SetInt("PlayTime_Minute", _timeMinute);
            PlayerPrefs.SetFloat("PlayTime_Second", _timeSecond);
            BestPlayTime();
        }
    }

    public void GetTime() {
        if (GameManager.level > 1) {
            _timeMinute = PlayerPrefs.GetInt("PlayTime_Minute");
            _timeSecond = PlayerPrefs.GetFloat("PlayTime_Second");
            _timeCount = PlayerPrefs.GetFloat("PlayTime_Count");
        }
    }

    public void PlayTime() {
        if (_isPlayerAlive) {
            _timeCount += Time.deltaTime;
            if (_timeCount >= 60.0f) {
                _timeMinute = (int)_timeCount / 60;
                _timeSecond = _timeCount % 60;
            } else {
                _timeSecond = _timeCount;
            }

            if (_timeMinute < 10) {
                if(_timeSecond < 10) {
                    _playTimeLabel.text = "0" + _timeMinute + " : 0" + string.Format("{0:N2}", _timeSecond);
                } else {
                    _playTimeLabel.text = "0" + _timeMinute + " : " + string.Format("{0:N2}", _timeSecond);
                }
            } else {
                if(_timeSecond < 10) {
                    _playTimeLabel.text = _timeMinute + " : 0" + string.Format("{0:N2}", _timeSecond);
                } else {
                    _playTimeLabel.text = _timeMinute + " : " + string.Format("{0:N2}", _timeSecond);
                }
            }

            if (!_playerAttack.enemySearched) {
                PlayerPrefs.SetInt("PlayTime_Minute", _timeMinute);
                PlayerPrefs.SetFloat("PlayTime_Second", _timeSecond);
                PlayerPrefs.SetFloat("PlayTime_Count", _timeCount);
            }
        }
    }

    public void BestPlayTime() {

        GetTime();

        if (PlayerPrefs.GetInt("BestPlayTime_Minute") == 0) {
            if (PlayerPrefs.GetFloat("BestPlayTime_Second") == 0) {
                PlayerPrefs.SetInt("BestPlayTime_Minute", _timeMinute);
                PlayerPrefs.SetFloat("BestPlayTime_Second", _timeSecond);
            }
        } else {
            if (PlayerPrefs.GetInt("BestPlayTime_Minute") > PlayerPrefs.GetInt("PlayTime_Minute")) {
                    PlayerPrefs.SetInt("BestPlayTime_Minute", _timeMinute);
                    PlayerPrefs.SetFloat("BestPlayTime_Second", _timeSecond);
                
            } else if (PlayerPrefs.GetInt("BestPlayTime_Minute") == _timeMinute) {
                if (PlayerPrefs.GetFloat("BestPlayTime_second") > _timeSecond) {
                    PlayerPrefs.SetInt("BestPlayTime_Minute", _timeMinute);
                    PlayerPrefs.SetFloat("BestPlayTime_Second", _timeSecond);
                }
            }
        }



    }

}
