using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy {

    private void Start() {
        _velocity = 3f;
        _dmg = 2f;
        _range = 3f;
    }

    private new void Update() {
        if(_target._playerHp > 0) {
            Direction();
            Bowshot();
        }
        base.Update();
        
    }

    public void Direction() {
        if(transform.position.x < _target.transform.position.x) {
            transform.localScale = new Vector3(1, 1, 0);
        } else {
            transform.localScale = new Vector3(-1,1,0);
        }
    }

    public void Bowshot() {
        float distance = Vector2.Distance(_target.transform.position, transform.position);

        if (_range >= distance) {
            _velocity = 3f;
        } else {
            _velocity = 8f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.CompareTag("Player")) {
            Player player = _target.GetComponent<Player>();
            player.Hit(_dmg);
        }
    }

}
