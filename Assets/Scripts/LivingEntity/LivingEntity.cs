using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{

    [Header("Living Entity")]
    public float _hp = 10;  // 체력

    public virtual void Hit(float dmg)
    {    // 데미지를 받는 가상 함수
        _hp -= dmg; // 체력을 데미지만큼 깎는다.
        // Debug.Log(gameObject.name + " : " + _hp);
        if (_hp <= 0)   // 체력(hp)이 0이거나 0보다 작을 때 
            Die();  // 죽음
    }

    protected abstract void Die();  // 상속받은 클래스에서 반드시 정의 해주어야 하는 Die 함수
    protected abstract void LevelUp();

}