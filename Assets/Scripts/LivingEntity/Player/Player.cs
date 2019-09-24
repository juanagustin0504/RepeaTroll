using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{

    public Hands _point;
    public LayerMask _layerMask;

    public float _playerHp;
    public float _velocity; 
    
    private Animator _animator;

    //private Transform _attackPoint;

    //private Transform _enemy; // 적
    private float _nextDamagebleTime = 0;

    private static PlayerAttack attackScript;
    
    [Header("Joystick")]
    public Joystick joystick;

    private HungerBar _hungerScript;

    public static PlayerAttack AttackScript
    {
        get { return attackScript; }
    }
    

    private void Awake()
    {
        _hungerScript = FindObjectOfType<HungerBar>();
        attackScript = GetComponent<PlayerAttack>();
        _animator = GetComponent<Animator>();

        _animator.SetBool("Die", false);
        //_attackPoint = gameObject.GetComponent<Transform>();
        // _enemy = GameObject.FindObjectOfType<Enemy>().transform;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        
        JoysticMove();
        
        //KeybordMove();

    }

    public void DyingAnimation() {
        _animator.SetBool("Die", true);
    }



    /*public void Attack()
    {
        if (_enemy.gameObject)
            _enemy = GameObject.FindObjectOfType<Enemy>().transform;

        if (_attackPoint.position.x > _enemy.position.x) // Player 왼쪽, Enemy 오른쪽
        {
            if (_attackPoint.position.y > _enemy.position.y) // Player 위쪽, Enemy 아래쪽
            {
                if (Mathf.Abs(_attackPoint.position.x - _enemy.position.x) > Mathf.Abs(_attackPoint.position.y - _enemy.position.y))
                { // Player.x - Enemy.x 의 절댓값이 Player.y - Enemy.y 의 절댓값 보다 클 때 == X 가 더 클 때 
                    _Point.HandSpawn(10f, Quaternion.Euler(new Vector3(0, 0, 90))); // 왼쪽으로 공격
                }
                else if (Mathf.Abs(_attackPoint.position.x - _enemy.position.x) < Mathf.Abs(_attackPoint.position.y - _enemy.position.y))
                { // Player.x - Enemy.x 의 절댓값이 Player.y - Enemy.y 의 절댓값 보다 작을 때 == Y 가 더 클 때 
                    _Point.HandSpawn(10f, Quaternion.Euler(new Vector3(0, 0, -180))); // 아래쪽으로 공격
                }
                else
                {

                }
            }
            else if (_attackPoint.position.y < _enemy.position.y) // Player 아래쪽, Enemy 위쪽
            {
                if (Mathf.Abs(_attackPoint.position.x - _enemy.position.x) > Mathf.Abs(_attackPoint.position.y - _enemy.position.y))
                { // Player.x - Enemy.x 의 절댓값이 Player.y - Enemy.y 의 절댓값 보다 클 때 == X 가 더 클 때 
                    _Point.HandSpawn(10f, Quaternion.Euler(new Vector3(0, 0, 90))); // 왼쪽으로 공격
                }
                else if (Mathf.Abs(_attackPoint.position.x - _enemy.position.x) < Mathf.Abs(_attackPoint.position.y - _enemy.position.y))
                { // Player.x - Enemy.x 의 절댓값이 Player.y - Enemy.y 의 절댓값 보다 작을 때 == Y 가 더 클 때 
                    _Point.HandSpawn(10f, Quaternion.Euler(new Vector3(0, 0, 0))); // 아래쪽으로 공격
                }
                else
                {

                }
            }
            // _Point.HandSpawn(10f, Quaternion.Euler(new Vector3(0, 0, 90)));
        }
        else if (_attackPoint.position.x < _enemy.position.x) // Player 오른쪽, Enemy 왼쪽
        {
            if (_attackPoint.position.y > _enemy.position.y) // Player 위쪽, Enemy 아래쪽
            {
                if (Mathf.Abs(_attackPoint.position.x - _enemy.position.x) > Mathf.Abs(_attackPoint.position.y - _enemy.position.y))
                { // Player.x - Enemy.x 의 절댓값이 Player.y - Enemy.y 의 절댓값 보다 클 때 == X 가 더 클 때 
                    _Point.HandSpawn(10f, Quaternion.Euler(new Vector3(0, 0, -90))); // 오른쪽으로 공격
                }
                else if (Mathf.Abs(_attackPoint.position.x - _enemy.position.x) < Mathf.Abs(_attackPoint.position.y - _enemy.position.y))
                { // Player.x - Enemy.x 의 절댓값이 Player.y - Enemy.y 의 절댓값 보다 작을 때 == Y 가 더 클 때 
                    _Point.HandSpawn(10f, Quaternion.Euler(new Vector3(0, 0, -180))); // 아래쪽으로 공격
                }
                else
                {

                }
            }
            else if (_attackPoint.position.y < _enemy.position.y) // Player 아래쪽, Enemy 위쪽
            {
                if (Mathf.Abs(_attackPoint.position.x - _enemy.position.x) > Mathf.Abs(_attackPoint.position.y - _enemy.position.y))
                { // Player.x - Enemy.x 의 절댓값이 Player.y - Enemy.y 의 절댓값 보다 클 때 == X 가 더 클 때 
                    _Point.HandSpawn(10f, Quaternion.Euler(new Vector3(0, 0, -90))); // 왼쪽으로 공격
                }
                else if (Mathf.Abs(_attackPoint.position.x - _enemy.position.x) < Mathf.Abs(_attackPoint.position.y - _enemy.position.y))
                { // Player.x - Enemy.x 의 절댓값이 Player.y - Enemy.y 의 절댓값 보다 작을 때 == Y 가 더 클 때 
                    _Point.HandSpawn(10f, Quaternion.Euler(new Vector3(0, 0, 0))); // 아래쪽으로 공격
                }
                else
                {
                    _Point.HandSpawn(10f, Quaternion.identity);
                }
            }
        }
        StartCoroutine("ShootDelay");
       
    }
    */

    void JoysticMove()
    {
        if (_playerHp == 0) return;
        Vector2 moveVec = joystick.GetInputVector(); // 조이스틱 이용하여 플레이어 이동

        Vector2 moveDirection = moveVec.normalized;
        Vector2 moveVelocity = moveDirection * _velocity;

        _animator.SetFloat("DirX", moveVec.x);
        _animator.SetFloat("DirY", moveVec.y);

        if (moveVec != Vector2.zero) { // 변경부분
            if (_playerHp != 0)
                _animator.SetBool("Walking", true);
            else
                _animator.SetBool("Walking", false);
        } else
            _animator.SetBool("Walking", false);

        Ray2D ray = new Ray2D(transform.position, moveDirection);

        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, (_velocity * Time.deltaTime) + 0.75f, _layerMask);

        if(rayHit.collider == null) {
            transform.Translate(moveVelocity * Time.deltaTime, Space.World);
        }
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -26f, 26f),
            Mathf.Clamp(transform.position.y, -14.5f, 11.5f)
        );


    }

    void KeybordMove() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveVec = new Vector2(moveX, moveY);

        moveVec.x = Mathf.RoundToInt(moveVec.x);
        moveVec.y = Mathf.RoundToInt(moveVec.y);

        Vector2 moveDirection = moveVec.normalized;
        Vector2 moveVelocity = moveDirection * _velocity;

        _animator.SetFloat("DirX", moveVec.x);
        _animator.SetFloat("DirY", moveVec.y);

        if (moveVec != Vector2.zero) // 변경부분
            _animator.SetBool("Walking", true);
        else
            _animator.SetBool("Walking", false);

        transform.Translate(moveVelocity * Time.deltaTime, Space.World);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -26f, 26f),
            Mathf.Clamp(transform.position.y, -14.5f, 11.5f)
        );


    }

    public override void Hit(float dmg)
    {  // 가상 메소드 오버라이딩
        if (_nextDamagebleTime <= Time.time)
        {  //  [다음 공격을 받을 수 있는 시간]보다 현재 시간이 클 경우
            _nextDamagebleTime = Time.time + 1; // [다음 공격을 받을 수 있는 시간]을 현재 시간 + 1초로 설정 
            _playerHp -= dmg; // 체력을 데미지만큼 깎는다.
            _hungerScript.HP_Hit(dmg);

            if (_playerHp <= 0) {  // 체력(hp)이 0이거나 0보다 작을 때 
                _playerHp = 0;
                GameManager._gameOver = true;
                Die();  // 죽음
            }
        }
    }

    protected override void Die()
    {  // 메소드 오버라이딩
        SoundManager.instance.PlayGameOver();
        DyingAnimation();
    }

    protected override void LevelUp() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform.CompareTag("Wings")) { // 플레이어 이동속도 증가 아이템을 먹었을 때
            _velocity *= 1.1f; // 플레이어 이동속도 10% 증가
        }

        if(collision.transform.CompareTag("ASU")) { // 공격 속도 증가 아이템을 먹었을 때
            attackScript._atd *= 0.85f; // 공격 딜레이 15% 감소
        }

        if(collision.transform.CompareTag("PJS")) { // 공격 탄환속도를 증가시키는 아이템을 먹었을 때
            attackScript._ats *= 1.1f; // 탄환속도 10% 증가

        }

        if (collision.transform.CompareTag("DMG"))
        {
            attackScript._dmg *= 1.2f; // 공격력 20% 증가
        }
        /*
        if (collision.transform.CompareTag("Projectile"))
        {
            if(collision.gameObject.name == "MagicBall(Clone)")
            {
                _hungerScript.HP_Hit(5f);
            }
            if (collision.gameObject.name == "Arrow(Clone)")
            {
                _hungerScript.HP_Hit(3f);
            }
        }*/
    }
    


    /*IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.3f);
    }*/

}
