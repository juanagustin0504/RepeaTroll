using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool _canStart; // 시작할 수 있는지 검사한다

    private bool _enableSpawnEnemy;
    //private bool _enableSpawnMine;
    private bool _enableSpawnLandform;

    private int _countEnemy;
    //private int _countMine;
    private int _countLandform;

    private int _increase; // 증감값

    public int _maxSpawn = 8; // 최대 적 생성 수
    //public int _maxMine = 4; // 최대 지뢰 생성 수
    public int _maxLandform = 20; // 최대 지형물 생성 수

    public Enemy[] _prefabsEnemys; // prefabs으로 사용할 Enemy들을 배열로 받아옴
    //public Mine _mine; // prefabs으로 사용할 Mine을 받아옴
    public Landform _landform; // prefabs으로 사용할 Landform을 받아옴

    void SpawnEnemy() { // Enemy 생성 함수
        float randomX = Random.Range(-25f, 25f); //적이 나타날 X좌표를 랜덤으로 생성
        float randomY = Random.Range(-14f, 10f); //적이 나타날 Y좌표를 랜덤으로 생성
        if (randomX >= -5 && randomX <= 5 || randomY >= -2 && randomY <= 2) { // 적이 이 좌표값들 사이에서 생성이 된다면 증감값을 0으로 만들어 다시 실행하게 함
            _increase = 0;
            return;
        }
        _increase = 1;
        if (_enableSpawnEnemy) { // Enemy 생성이 가능하다면 생성
            Instantiate(_prefabsEnemys[Random.Range(0, _prefabsEnemys.Length)], new Vector3(randomX, randomY, 0f), Quaternion.identity);
        }
    }

    //void SpawnMine() { // Mine 생성 함수
    //    float randomX = Random.Range(-25f, 25f); //함정이 나타날 X좌표를 랜덤으로 생성
    //    float randomY = Random.Range(-14f, 10f); //함정이 나타날 Y좌표를 랜덤으로 생성
    //    if (randomX >= -5 && randomX <= 5 || randomY >= -2 && randomY <= 2) {
    //        _increase = 0;
    //        return;
    //    }
    //    _increase = 1;
    //    if (_enableSpawnMine) {
    //        Instantiate(_mine, new Vector3(randomX, randomY, 0f), Quaternion.identity);
    //    }
    //}

    void SpawnLandform() { // Landform 생성 함수
        float randomX = Random.Range(-25f, 25f); //지형물이 나타날 X좌표를 랜덤으로 생성
        float randomY = Random.Range(-14f, 10f); //지형물이 나타날 Y좌표를 랜덤으로 생성
        if (randomX >= -5 && randomX <= 5 || randomY >= -2 && randomY <= 2) {
            _increase = 0;
            return;
        }
        _increase = 1;
        if (_enableSpawnLandform) {
            Instantiate(_landform, new Vector3(randomX, randomY, 0f), Quaternion.identity);
        }
    }


    void Start() {
        StartCoroutine("StartDelay");
    }

    void Update()
    {
        //  StartCoroutine("SpawnEnemyCoroutine");
        if (_canStart) {
            Condition();
        }
    }
    
    void Condition() { // 생성할 수 있는 지 검사
        if (_countEnemy < _maxSpawn) {
            _enableSpawnEnemy = true;
        } else {
            _enableSpawnEnemy = false;
        }

        if (_enableSpawnEnemy) {
            for (_countEnemy = 0; _countEnemy < _maxSpawn; _countEnemy += _increase) {
                SpawnEnemy();
            }
        }

        //if (_countMine < _maxMine) {
        //    _enableSpawnMine = true;
        //} else {
        //    _enableSpawnMine = false;
        //}

        //if (_enableSpawnMine) {
        //    for (_countMine = 0; _countMine < _maxMine; _countMine += _increase) {
        //        SpawnMine();
        //    }
        //}

        if (_countLandform < _maxLandform) {
            _enableSpawnLandform = true;
        } else {
            _enableSpawnLandform = false;
        }

        if (_enableSpawnLandform) {
            for (_countLandform = 0; _countLandform < _maxLandform; _countLandform += _increase) {
                SpawnLandform();
            }
        }
    }

    IEnumerator StartDelay() {
        _canStart = false;
        yield return new WaitForSeconds(3f);
        _canStart = true;
    }


}
