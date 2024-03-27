
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBezier : MonoBehaviour
{

    protected Transform m_tranform;
    public Tower m_tower;
    private GameObject m_gameobject;




    //public bool bIsHero;// Arrow is Hero;
    protected Enemy m_enemy;




    private Vector2 vStartPos;
    public float fSpeed;
    private bool bShot = false;
    public float fHigh;


    public bool bIsBulletPosion;


    private void Awake()
    {
        m_tranform = transform;
        m_gameobject = gameObject;

    }


    // Update is called once per fram
    Vector2 vCurrentPos;
    Vector2 vEnemyPos;
    float _time;

    void Update()
    {
        if (!bShot) return;

        _time += Time.deltaTime * fSpeed;
        if (m_enemy.isInGameplay())
            vEnemyPos = m_enemy.m_tranBodyPoint.position;


        //CACH 1
        if (_time <= 1)
        {
            vCurrentPos = Bezier.GetBezier(vStartPos, vEnemyPos, _time, fHigh);
            Rotation(m_tranform.position, vCurrentPos);
            m_tranform.position = vCurrentPos;

        }
        else
        {
            bShot = false;

            if (bIsBulletPosion)
            {
                if (!IsInvoking("Destroy"))
                    Invoke("Destroy", 0.3f);
            }
            else
                m_gameobject.SetActive(false);
        }



    }

    public void Shot(Vector2 _startpos, Enemy _enemy, Tower _tower)
    {
        m_tranform.position = _startpos;
        //  bIsHero = false;
        _time = 0;

        m_tower = _tower;
        m_enemy = _enemy;

        vStartPos = _startpos;
        vCurrentPos = _startpos;

        if (_enemy)
            vEnemyPos = m_enemy.m_tranBodyPoint.position;

        bShot = true;
    }


    public virtual void HitEnemy()
    {
        if (m_enemy)
            m_enemy.HEALTH.ReduceHp(m_tower.TOWER_DATA.GetDamage(m_tower.eTowerLevel));

    }


    virtual public void Rotation(Vector2 _f, Vector2 _t)
    {

    }


    private void Destroy()
    {

        m_gameobject.SetActive(false);
    }



    private void OnDisable()
    {
        m_tranform.eulerAngles = new Vector3(0, 0, 90);
    }
}
