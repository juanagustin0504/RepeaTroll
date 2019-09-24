using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {

    public Image _mask;

    private RectTransform _maskRect;

    private float _currentHP = 0f;
    private float _maxHpBarWidth;
    private float _maxHP = 20000f;
    private float _hit_Damage = 0f;

    private void Awake() {
        _maskRect = _mask.GetComponent<RectTransform>();
    }
    private void Start() {
        _maxHpBarWidth = _maskRect.sizeDelta.x;
        _currentHP = _maxHP;
    }

    // Update is called once per frame
    void Update() {
        HpDown();
    }

    public void HP_Hit(float dmg) {
        _hit_Damage = dmg;
        _currentHP -= _hit_Damage;
    }

    void HpDown() {
        float deltaSize = _currentHP / _maxHP;

        _maskRect.sizeDelta = new Vector2(_maxHpBarWidth * deltaSize, _maskRect.sizeDelta.y);
    }
}
