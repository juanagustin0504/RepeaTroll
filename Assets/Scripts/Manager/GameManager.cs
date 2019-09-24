using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static int level = 0; // 씬의 번호를 의미한다

    public static bool _gameOver = false; // 게임 오버가 되었는지 확인한다


    private Player _player;
    private PlayerAttack _playerAttack;

    private void Awake() {
        _player = FindObjectOfType<Player>();
        _playerAttack = FindObjectOfType<PlayerAttack>();
        GetInfo(); // 게임 정보를 불러옴
    }

    private void Start() {
        InGameButtonEvent.OnPortalButton += NextLevel;
    }

    private void Update() {
        
        SetInfo();
        if(_gameOver) { // 게임오버가 된다면
            GameReset(); // 게임을 리셋한다
        }
        
    }

    public void InitialSetting() { // 플레이어 정보의 초깃값을 설정
        PlayerPrefs.SetFloat("init_HP", 20f);
        PlayerPrefs.SetFloat("init_Velocity", 5f);
        PlayerPrefs.SetFloat("init_Damage", 2f);
        PlayerPrefs.SetFloat("init_AttackSpeed", 10f);
        PlayerPrefs.SetFloat("init_AttackDelay", 0.5f);
    }

    public void GameReset() { // 게임 종료 후 플레이어의 정보를 초깃값으로 되돌림과 동시에 LastStage에 기록을 저장함
        _player._playerHp = PlayerPrefs.GetFloat("init_HP");
        _player._velocity = PlayerPrefs.GetFloat("init_Velocity");
        _playerAttack._dmg = PlayerPrefs.GetFloat("init_Damage");
        _playerAttack._ats = PlayerPrefs.GetFloat("init_AttackSpeed");
        _playerAttack._atd = PlayerPrefs.GetFloat("init_AttackDelay");

        PlayerPrefs.SetInt("LastStage", level);

        StartCoroutine("Go2Main");
    }
    
    public void SetInfo() {
        if (!_playerAttack.enemySearched) { // 스테이지에서 적을 찾지 못하였을 때, 그 스테이지 클리어로 간주하고 플레이어 정보를 저장
            if(_player._playerHp <= 15) {
                PlayerPrefs.SetFloat("HP", _player._playerHp + 5f);
            } else {
                PlayerPrefs.SetFloat("HP", _player._playerHp);
            }
            PlayerPrefs.SetFloat("Velocity", _player._velocity);
            PlayerPrefs.SetFloat("Damage", _playerAttack._dmg);
            PlayerPrefs.SetFloat("AttackDelay", _playerAttack._atd);
            PlayerPrefs.SetFloat("AttackSpeed", _playerAttack._ats);
        }
    }

    public void GetInfo() {
        if (level > 1) { // 게임을 플레이 하면서 씬을 이동하고 나서 플레이어의 정보를 불러올 때
            _player._playerHp = PlayerPrefs.GetFloat("HP");
            _player._velocity = PlayerPrefs.GetFloat("Velocity");
            _playerAttack._dmg = PlayerPrefs.GetFloat("Damage");
            _playerAttack._atd = PlayerPrefs.GetFloat("AttackDelay");
            _playerAttack._ats = PlayerPrefs.GetFloat("AttackSpeed");
        } else { // 게임을 다시 처음부터 시작할 때 시간, 스테이지 기록을 초기값으로 되돌림 && 시작값
            InitialSetting();
            GameStart();
            SetInfo();
            PlayerPrefs.SetInt("LastStage", 0);
            PlayerPrefs.SetInt("PlayTime_Minute", 0);
            PlayerPrefs.SetFloat("PlayTime_Second", 0);
        }
    }

    void GameStart() {
        _player._playerHp = PlayerPrefs.GetFloat("init_HP");
        _player._velocity = PlayerPrefs.GetFloat("init_Velocity");
        _playerAttack._dmg = PlayerPrefs.GetFloat("init_Damage");
        _playerAttack._ats = PlayerPrefs.GetFloat("init_AttackSpeed");
        _playerAttack._atd = PlayerPrefs.GetFloat("init_AttackDelay");
    }
    
    public void NextLevel() {
        SceneManager.LoadScene(++level); // 다음 스테이지로 넘어감
    }

    IEnumerator Go2Main() { // 게임 종료 후 메인으로 돌아가는 코루틴


        yield return new WaitForSeconds(3f);
        level = 0;

        _gameOver = false;

        SceneManager.LoadScene(level);
    }


}
