using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy {
    
    private Animator _animator;

    public void Start() {
        /*_velocity = 4f;
        _dmg = 3f;
        _range = 3f;
        _ats = 5f;*/
        _animator = GetComponent<Animator>();
        LevelUp();
    }

    private new void Update() {
        if (_target._playerHp > 0)
            Bowshot();
        base.Update();
    }

    public void Bowshot() {
        float distance = Vector2.Distance(_target.transform.position, transform.position);
        
        Vector2 moveVec = new Vector2(_target.transform.position.x - transform.position.x, _target.transform.position.y - transform.position.y);

        moveVec.x = Mathf.RoundToInt(moveVec.x);
        moveVec.y = Mathf.RoundToInt(moveVec.y);

        Vector2 moveDirection = moveVec.normalized;

        _animator.SetFloat("DirX", moveVec.x);
        _animator.SetFloat("DirY", moveVec.y);

        if (_range >= distance) {
            _velocity = 0;
            _animator.SetBool("Walking", false);
            Attack();
        } else {
            _velocity = 4f;
            _animator.SetBool("Walking", true);
        }
    }

    protected override void Attack() {

        if (!_attackable) return;

        _animator.SetBool("Attacking", true);

        Vector2 dir = (transform.position - _target.transform.position).normalized;
        
        StartCoroutine("AttackingAnimation", dir);
        

        StartCoroutine("AttackDelay");

    }

    protected override void LevelUp() {
        base.LevelUp();
    }

    IEnumerator AttackDelay() {
        _attackable = false;

        yield return new WaitForSeconds(_ats);
        _attackable = true;

    }

    IEnumerator AttackingAnimation(Vector2 dir) {
        Vector2 startPos = transform.position;
        Vector2 endPos = _target.transform.position;
        float i = 0;
        while (i < 1) {
            i += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, endPos, i);
        }
        transform.position = new Vector2(transform.position.x + dir.x * 1.3f, transform.position.y + dir.y * 1.3f);

        yield return new WaitForSeconds(1f);
        _animator.SetBool("Attacking", false);
    }


}
