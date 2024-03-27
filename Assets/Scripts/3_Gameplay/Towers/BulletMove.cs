using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    protected Transform m_tranform;
    private GameObject m_gameobject;
    private Tower m_tower;
    private Enemy m_enemy;

    public float fSpeed;
    private bool bShot = false;



    private void Awake()
    {
        m_tranform = transform;
        m_gameobject = gameObject;
    }


    // Update is called once per fram
    Vector2 vCurrentPos;
    Vector2 vEnemyPos;


    void Update()
    {
        if (!bShot) return;

        if (m_enemy.isInGameplay())
            vEnemyPos = m_enemy.m_tranBodyPoint.position;

        vCurrentPos = Vector2.MoveTowards(vCurrentPos, vEnemyPos, fSpeed * Time.deltaTime);
        m_tranform.position = vCurrentPos;

        if (vCurrentPos == vEnemyPos)
            HitEnemy();


    }

    public void Shot(Vector2 _startpos, Enemy _enemy, Tower _tower)
    {
        m_tranform.position = _startpos;
        m_tower = _tower;
        m_enemy = _enemy;
        vCurrentPos = _startpos;
        vEnemyPos = m_enemy.GetCurrentPos();
        bShot = true;

    }

    private void HitEnemy()
    {
        if (m_enemy)
            m_enemy.HEALTH.ReduceHp(m_tower.TOWER_DATA.GetDamage(m_tower.eTowerLevel));

        m_gameobject.SetActive(false);
    }

}
