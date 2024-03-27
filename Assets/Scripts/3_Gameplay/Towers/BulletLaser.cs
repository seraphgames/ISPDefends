using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaser : MonoBehaviour
{
    public LineRenderer m_lineRenderer;
    private Enemy m_enemy;
    private Vector2 vStarPos;




    private bool bAllowShot = false;

    Vector2 vEnemyPos;

    private void Start()
    {
        vStarPos = transform.position;

    }

    public void Shot( Enemy _enemy)
    {
        m_enemy = _enemy;
       

         bAllowShot = true;
        m_lineRenderer.positionCount = 2;
        m_lineRenderer.gameObject.SetActive(true);
    }

    private void DrawLine(Vector3 _from, Vector3 _to)
    {
        _from.z = _to.z = -10;
        m_lineRenderer.SetPosition(0, _from);
        m_lineRenderer.SetPosition(1, _to);
    }

    public void StopShot()
    {
        m_lineRenderer.positionCount = 0;
        m_enemy = null;
    }



    private void Update()
    {

        if (!bAllowShot) return;

        if (m_enemy.isInGameplay())
        {
            vEnemyPos = m_enemy.GetCurrentPos();
            m_lineRenderer.positionCount = 2;
            DrawLine(vStarPos, vEnemyPos);
        }
        else
        {
            bAllowShot = false;
            StopShot();
        }


    }
}
