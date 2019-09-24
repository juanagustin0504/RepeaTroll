using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    public Projectile _point;

    private Animator _animator;

    private void Start() {
        _hp = 20000f;
        _dmg = 15f;
        _range = 6f;
        _ats = 0.2f;
        _velocity = 10f;

        _animator = GetComponent<Animator>();
    }

    private new void Update() {
        if(_target._playerHp > 0 && _hp > 0) {
            Bowshot();
        }
        base.Update();
        if (_hp <= 0)
            BossStageManager._isBossAlive = false;
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
            _velocity = 2;
            _animator.SetBool("Walking", false);
            Attack();
        } else {
            _velocity = 10f;
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

        _point.ProjectileSpawn(20f, rot);

        StartCoroutine("AttackDelay");

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

    protected override void Die() {
        _velocity = 0;
        _hp = 0;
        DyingAnimation();
    }

    public void DyingAnimation() {
        _animator.SetBool("Walking", false);
        _animator.SetBool("Die", true);
    }

    public override void Hit(float dmg) {
        BossHealthBar bossHealthBar = FindObjectOfType<BossHealthBar>();
        base.Hit(dmg);
        bossHealthBar.HP_Hit(dmg);
    }

}
