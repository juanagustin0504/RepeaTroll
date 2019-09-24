using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    private PlayerAttack _playerAttack;

    public GameObject[] _items;

    private Transform[] _points;

    private float _createTime = 5f;

    public enum _item {
        speedUp, 
        DamageUp,
        ProjectileSpeedUp,
        AttackDelayDown
    }

    private void Awake() {
        _playerAttack = FindObjectOfType<PlayerAttack>();
        _points = GameObject.Find("ItemSpawnPoint").GetComponentsInChildren<Transform>();

        if (_points.Length > 0) {
            StartCoroutine("SpawnItem");
        }
    }
    
    IEnumerator SpawnItem() {
        while (true) {

            if (_playerAttack.enemySearched) {

                int random = Random.Range(0, 99);

                yield return new WaitForSeconds(_createTime);

                int id = Random.Range(0, _points.Length - 1);

                if (random >= 0 && random < 25) { // AttackDelayDown Item 
                    Instantiate(_items[0], _points[id].position, Quaternion.identity);
                } else if (random >= 25 && random < 50) { // DamageUp Item
                    Instantiate(_items[1], _points[id].position, Quaternion.identity);
                } else if (random >= 50 && random < 75) { // PlayerSpeedUp Item
                    Instantiate(_items[2], _points[id].position, Quaternion.identity);
                } else if (random >= 75 && random < 100) { // ProjectileSpeedUp Item
                    Instantiate(_items[3], _points[id].position, Quaternion.identity);
                }
            }
            yield return null;
        }

    }

}