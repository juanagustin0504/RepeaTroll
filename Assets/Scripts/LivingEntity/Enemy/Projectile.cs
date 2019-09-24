using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float _speed;
    private bool _init;

    private Archer _archerScript;
    private Magician _magicianScript;
    private Boss _bossScript;

    public Projectile _prefabs;

    private void Awake()
    {
        _archerScript = GameObject.FindObjectOfType<Archer>();
        _magicianScript = GameObject.FindObjectOfType<Magician>();
        _bossScript = GameObject.FindObjectOfType<Boss>();
    }

    public void ProjectileSpawn(float speed, Quaternion rot)
    {
        Projectile pjt = Instantiate(_prefabs, transform.parent.position, Quaternion.identity, transform.parent) as Projectile;
        //hand.transform.parent.GetComponent<Player>()._RH = hand;
        pjt._speed = speed;
        pjt.transform.rotation = rot;
        pjt.transform.SetParent(null);
        pjt._init = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SoundManager.instance.PlayCollsion();
            Player player = collision.gameObject.GetComponent<Player>();

            if (transform.name.Equals("Arrow(Clone)"))
            {
                player.Hit(_archerScript._dmg);
            }
            if (transform.name.Equals("MagicBall(Clone)"))
            {
                player.Hit(_magicianScript._dmg);
            }
            if(transform.name.Equals("Sword(Clone)")) {
                player.Hit(_bossScript._dmg);
            }
            Destroy(this.gameObject);
        }
    }
}

