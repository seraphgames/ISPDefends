using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArcher : BulletBezier
{

    bool bAllowShowDetail = false;
    float xDis, yDis, alphal;
    Quaternion _qua;

    public bool bIsArcher, bIsRocket;
    private GameObject objExplosion;
    private GameObject _detailOfBow;

    private void OnEnable()
    {
        Invoke("AllowShowDetail", 1.0f);
    }

    private void AllowShowDetail()
    {
        bAllowShowDetail = true;
    }


    public override void Rotation(Vector2 _from, Vector2 _to)
    {
        xDis = _to.x - _from.x;
        yDis = _to.y - _from.y;

        xDis += 0.001f;


        alphal = Mathf.Atan(yDis / xDis) * Mathf.Rad2Deg;
        if (xDis <= 0)
            alphal = 180 + alphal;

        _qua = Quaternion.Euler(0, 0, alphal);
        m_tranform.rotation = _qua;
    }

    private void OnDisable()
    {
        if (bIsArcher) //archer
        {
            HitEnemy();
            if ((!m_enemy || !m_enemy.gameObject.activeInHierarchy) && bAllowShowDetail)
            {
                _detailOfBow = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BowDetail).GetItem();
                if (_detailOfBow)
                {
                    _detailOfBow.transform.position = m_tranform.position;
                    _detailOfBow.transform.eulerAngles = m_tranform.eulerAngles;
                    _detailOfBow.SetActive(true);
                }
            }
        }
        else if (bIsRocket && bAllowShowDetail)
        {
            if (m_tower)
                TheEventManager.PostEvent_RocketHit(m_tranform.position, m_tower.TOWER_DATA.GetDamage(m_tower.eTowerLevel));

            objExplosion = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_of_Rocket).GetItem();
            if (objExplosion)
            {
                objExplosion.transform.position = m_tranform.position;
                objExplosion.SetActive(true);
                TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.explosion);//sound
            }
        }
        else if (bIsBulletPosion && bAllowShowDetail)
        {
            HitEnemy();
            objExplosion = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_Poision).GetItem();
            objExplosion.transform.position = m_tranform.position;
            objExplosion.SetActive(true);
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.explosion_poison);//sound
        }
        m_tranform.eulerAngles = new Vector3(0, 0, 90);
    }
}
