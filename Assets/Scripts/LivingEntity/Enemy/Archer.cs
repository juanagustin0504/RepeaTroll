using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy 
{
    public Projectile _point;

    private Animator _animator;

    public void Start() {
        /*_velocity = 2f;
        _dmg = 3f;
        _range = 6f;
        _ats = 2f;*/
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
            _velocity = 1f;
            _animator.SetBool("Walking", true);
        }
    }

    protected override void Attack() {

        if (!_attackable) return;

        _animator.SetBool("Attacking", true);

        StartCoroutine("AttackingAnimation");

        Vector2 dir = (transform.position - _target.transform.position).normalized;

        float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(0, 0, rot_z + 90);

        _point.ProjectileSpawn(10f, rot);

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

    IEnumerator AttackingAnimation() {
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("Attacking", false);
    }



}
