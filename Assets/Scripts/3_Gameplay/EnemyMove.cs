using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform m_transform;
    private Transform m_TranRender;
    private Enemy m_enemy;
    public float fMoveSpeed;
    private TheRoad m_theRoad;
    int iIndexTheRoad;

    


    // Use this for initialization
    void Awake()
    {
        m_enemy = GetComponent<Enemy>();

        m_transform = transform;
        m_TranRender = m_transform.GetChild(0);

    }



    public void Init()
    {
        iIndexTheRoad = 0;
        fMoveSpeed = m_enemy.DATA.fConfig_MoveSpeed;
        vTargetPos = m_theRoad.GetPos(iIndexTheRoad);
        m_transform.position = vTargetPos;
    }




    // Update is called once per frame
    void Update()
    {
        if (TheGameStatusManager.CURRENT_STATUS != TheGameStatusManager.GAME_STATUS.Playing) return;
        if (m_enemy.isStatus(Enemy.STATUS.Moving))
            Move();        
    }

    //SET ROAD
    public void SetRoad(TheRoad _road)
    {
        if (_road)
            m_theRoad = _road;
        else
            m_theRoad = TheLevel.Instance.LIST_THE_ROAD[0];
        m_transform.position = m_theRoad.LIST_POS[0];
    }


    //MOVE===============================================
    Vector3 vCurrentPos;
    Vector3 vTargetPos;
    private void Move()
    {
        vCurrentPos = m_transform.position;
        vCurrentPos = Vector2.MoveTowards(vCurrentPos, vTargetPos, Time.deltaTime * fMoveSpeed);
        if (vCurrentPos == vTargetPos)
        {
            if (iIndexTheRoad < m_theRoad.iTotalPos - 1)
            {
                iIndexTheRoad++;
                vTargetPos = m_theRoad.GetPos(iIndexTheRoad);
                Rotation();
            }
            else
            {
                m_enemy.SetStatus(Enemy.STATUS.CompleteMission);
            }

        }
        vCurrentPos.z = vCurrentPos.y;
        m_transform.position = vCurrentPos;
    }

    Vector2 vSoldierPos;
    private void MoveToSoldier()
    {
        vCurrentPos = m_transform.position;
        vCurrentPos = Vector2.MoveTowards(vCurrentPos, vTargetPos, Time.deltaTime * fMoveSpeed);
        if ((Vector2)vCurrentPos == vSoldierPos)
        {
            iIndexTheRoad++;

        }
        vCurrentPos.z = vCurrentPos.y;
        m_transform.position = vCurrentPos;
    }
    public void MoveToSoldier(Vector2 _pos)
    {
        vSoldierPos = _pos;
    }


    private void Rotation()
    {
        if (vCurrentPos.x > vTargetPos.x)
        {
            // m_SpriteRender.flipX = false;
            m_TranRender.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            // m_SpriteRender.flipX = true;
            m_TranRender.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    public void Rotation(float _yAngle)
    {
        m_TranRender.eulerAngles = new Vector3(0, _yAngle, 0);
    }
}
