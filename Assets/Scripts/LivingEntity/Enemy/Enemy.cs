using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public float _dmg;
    public float _range;
    public float _ats;

    public bool _attackable = true;

    public static Player _target;    //타겟(플레이어)을 적의 정적 변수로 지정

    public LayerMask _layerMask;

    public float _velocity = 3f;          // 이동 속도

    protected void Awake() {
        _target = FindObjectOfType<Player>();
    }

    private void Start() {
        LevelUp();
    }

    protected void Update()
    {
        if (_target._playerHp > 0)
        {  
            Tracking();
        }
    }

    protected override void LevelUp() {
        if(GameManager.level > 1) {
            _dmg *= 1.2f; // 레벨이 증가할 때마다 공격력 20%씩 증가
            _hp *= GameManager.level + 6.01f; // ㅎ
        }
    }
    
    private void Tracking()
    {

        GoingStraight(_velocity);  // 직진
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -26f, 26f),
            Mathf.Clamp(transform.position.y, -14.5f, 11.5f));
        
    }

    private void GoingStraight(float speed)
    {
        Vector2 dir = (_target.transform.position - transform.position).normalized;

        Ray2D ray = new Ray2D(transform.position, dir);

        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, (_velocity * Time.deltaTime) + 0.75f, _layerMask);

        if (rayHit.collider == null) {
            transform.Translate(dir * speed * Time.deltaTime); // 적 방향으로 이동
        }


        
    }

    protected override void Die()
    {  // 메소드 오버라이딩
        Destroy(this.gameObject);    // 자기 자신의 오브젝트 제거
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.CompareTag("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();
            SoundManager.instance.PlayCollsion();
            player.Hit(1);
        }
    }
    protected virtual void Attack() {
        _target.GetComponent<Player>().Hit(_dmg);
    }
}
