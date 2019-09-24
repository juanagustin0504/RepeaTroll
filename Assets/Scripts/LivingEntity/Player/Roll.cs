using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour {

    private Animator _animator;
    private CircleCollider2D _circle;
    private SpriteRenderer _sprite;

    public Sprite _sprite_roll;
    public Sprite _sprite_front;

    public GameObject _particle;

    private bool _canRoll = true;

    public float _rollDelay = 1f;
    public float _canRollDelay = 3f;
    


    private void Start() {
        _animator = GetComponent<Animator>();
        _circle = GetComponent<CircleCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        
    }

    public void Rolling() {

        if (!_canRoll) return;
        SoundManager.instance.PlayRoll();
        StartCoroutine("ChangeSprite");
//        StopCoroutine("ChangeSprite"); // 레킹볼

        
    }

    IEnumerator ChangeSprite() {

        _particle.SetActive(true);
        _animator.enabled = false;
        _circle.enabled = false;
        _sprite.sprite = _sprite_roll;

        yield return new WaitForSeconds(_rollDelay);

        _particle.SetActive(false);
        _animator.enabled = true;
        _circle.enabled = true;
        _sprite.sprite = _sprite_front;

        StartCoroutine("RollDelay");
    }

    IEnumerator RollDelay() {

        _canRoll = false;
        
        yield return new WaitForSeconds(_canRollDelay);
        _canRoll = true;
    }

}
