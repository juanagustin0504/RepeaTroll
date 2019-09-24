using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour {
     
    private float _speed;
    private bool _init;

    public Hands prefabs;


    public void HandSpawn(float speed, Quaternion rot)
    {
        Hands hand = Instantiate(prefabs, transform.parent.position, Quaternion.identity, transform.parent) as Hands;
        //hand.transform.parent.GetComponent<Player>()._RH = hand;
        hand._speed = speed;
        hand.transform.rotation = rot;
        hand.transform.SetParent(null);
        hand._init = true;
        
    }

    void Update()
    {
        Shooting();
    }


    public void Shooting()
    {
        if (_init)
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }
    }



   /* void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {  // 콜라이더에 닿은 오브젝트의 Tag가 "Enemy"인 경우
            Enemy enemy = coll.gameObject.GetComponent<Enemy>(); // 콜라이더에 닿은 오브젝트에서 Enemy 스크립트 가져옴
            enemy.Hit(1);  // Plyaer의 데미지를 Enemy에게 
            Destroy(gameObject);
        }
    }*/
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            SoundManager.instance.PlayCollsion();
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Hit(Player.AttackScript._dmg);
            Destroy(this.gameObject);
        } else if(collision.transform.CompareTag("Boss")) {
            SoundManager.instance.PlayCollsion();
            Boss boss = collision.gameObject.GetComponent<Boss>();
            boss.Hit(Player.AttackScript._dmg);
            Destroy(this.gameObject);
        }
    }

}
