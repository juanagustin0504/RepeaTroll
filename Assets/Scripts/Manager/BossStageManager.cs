using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossStageManager : MonoBehaviour {

    public Sprite[] _maps;
    
    public GameObject _backgroundMap;

    public GameObject _boss;

    public Transform _bossSpawnPoint;

    public static bool _isBossAlive; // 보스가 살아 있는지 확인한다

    private bool _canStart; // 시작할 수 있는지 검사한다

    private int _countBoss = 0;

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = _backgroundMap.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _maps[Random.Range(0, 3)];
    }

    private void Start() {
        _isBossAlive = true;
        StartCoroutine("StartDelay");
        StartCoroutine("Clear"); // 게임을 시작하고 나서 부터 게임이 끝날 때까지 실행되는 코루틴 실행
        

    }

    private void Update() {

        if (_canStart) {
            SpawnBoss();
        }
        
        
    }
    
    void SpawnBoss() {
        if (_countBoss > 0) return;
        Instantiate(_boss, new Vector3(_bossSpawnPoint.position.x, _bossSpawnPoint.position.y, 0), Quaternion.identity);
        _countBoss++;
    }

    IEnumerator Clear() { // 게임을 클리어 했을 때
        while (true) {
            if (GameManager.level == 10 && !_isBossAlive) {
                TimeManager._isClear = true;
                StartCoroutine("Go2Main");
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator StartDelay() {
        _canStart = false;
        yield return new WaitForSeconds(3f);
        _canStart = true;
    }

    IEnumerator Go2Main() { // 게임 종료 후 메인으로 돌아가는 코루틴

        yield return new WaitForSeconds(3f);
        GameManager.level = 0;
        TimeManager._isClear = false;

        SceneManager.LoadScene(GameManager.level);
    }

}
