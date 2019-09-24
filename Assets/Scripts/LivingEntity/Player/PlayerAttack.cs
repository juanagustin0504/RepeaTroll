using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Paramiter")]
    public float _dmg = 2f;
    public float _ats = 10f;
    public float _atd =  0.5f;

    public bool enemySearched = false;

    private bool canAttack = true;
    private Vector3 selectDistanceVec;

    [Header("Hand")]
    public Hands handScript;

    // Use this for initialization
    void Start () {

        InGameButtonEvent.OnAttackButton += TryAttack;
	}
    private void Update()
    {
        StartCoroutine(EnemySearch());
    }

    IEnumerator EnemySearch()
    {
        Enemy[] enemys = GameObject.FindObjectsOfType<Enemy>();

        if(enemys.Length == 0)
        {
            PortalManager.Instance._isEnemyAlive = false;
            enemySearched = false;
            yield break;
        }

        enemySearched = true;
        PortalManager.Instance._isEnemyAlive = true;

        Enemy selectEnemy = enemys[0];

        float distance1 = GetDistanceToEnemy(enemys[0].transform.position);
        //Debug.Log(distance1);

        for (int i = 1; i < enemys.Length; i++)
        {
            try {
                float distance2 = GetDistanceToEnemy(enemys[i].transform.position);

                if (distance2 <= 0) {
                    selectEnemy = enemys[i];
                    break;
                }

                if (distance1 > distance2) {
                    distance1 = distance2;
                    selectEnemy = enemys[i];
                }
            } catch (Exception) { } //Debug.Log("Missing index is [ " + i + " ]"); }
            

            yield return null;
        }

        try {
            selectDistanceVec = selectEnemy.transform.position - transform.position;
        } catch (Exception e) { } // Debug.Log(e.ToString()); }
        
    }

    float GetDistanceToEnemy(Vector2 enemyPos)
    {
        return Vector2.Distance(enemyPos, transform.position);
    }

    void TryAttack()
    {
        if (!CanAttack())
        {
            return;
        }

        if (!enemySearched)
        { 
            return;
        }

        float rot_z = Mathf.Atan2(selectDistanceVec.y, selectDistanceVec.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, rot_z - 90);

        handScript.HandSpawn(_ats, rot);

        StartCoroutine(WaitForAttackDelay());
    }

    bool CanAttack()
    {
        return canAttack;
    }

    IEnumerator WaitForAttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(_atd);
        canAttack = true;
    }
}
