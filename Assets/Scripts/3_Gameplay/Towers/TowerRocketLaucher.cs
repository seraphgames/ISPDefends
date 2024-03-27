using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRocketLaucher : Tower
{

    private GameObject _bullet;
    public GameObject m_objCricleRange;
    [SerializeField]
    private Transform m_tranBulletPos1;

    [Space(20)]
    public List<GameObject> LIST_TOWER_LEVEL;



    public override void ShowCircle()
    {
        m_objCricleRange.transform.localScale = Vector3.one * fCurrentRange;
        m_objCricleRange.SetActive(false);
        m_objCricleRange.SetActive(true);
    }



    public override void Attack(Enemy _enemy)
    {
        if (TheGameStatusManager.CURRENT_STATUS != TheGameStatusManager.GAME_STATUS.Playing) return;
        if (!_enemy.isInGameplay()) return;


        StartCoroutine(Shot(_enemy));

    }

    public override void SetTowerRender(TheEnumManager.TOWER_LEVEL _level)
    {
        int _total = LIST_TOWER_LEVEL.Count;
        for (int i = 0; i < _total; i++)
        {
            if (i == (int)_level) LIST_TOWER_LEVEL[i].SetActive(true);
            else LIST_TOWER_LEVEL[i].SetActive(false);
        }
    }


    WaitForSeconds _wait = new WaitForSeconds(0.2f);
    public IEnumerator Shot(Enemy _enemy)
    {
        if (eTowerLevel == TheEnumManager.TOWER_LEVEL.level_1 || eTowerLevel == TheEnumManager.TOWER_LEVEL.level_2)
        {
            if (_enemy.isInGameplay())
            {
                _bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_RocketLaucher).GetItem();
                if (_bullet)
                {
                    //sound
                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_magic);
                   // _bullet.transform.position = m_tranBulletPos1.position;
                    _bullet.GetComponent<BulletArcher>().Shot(m_tranBulletPos1.position, _enemy, this);

                    _bullet.SetActive(true);
                }
            }

        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (_enemy.isInGameplay())
                {
                    _bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_RocketLaucher).GetItem();
                    if (_bullet)
                    {
                        //sound
                        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_rocket_laucher);

                       // _bullet.transform.position = m_tranBulletPos1.position;
                        _bullet.GetComponent<BulletArcher>().Shot(m_tranBulletPos1.position, _enemy, this);
                        _bullet.SetActive(true);
                    }
                }
                yield return _wait;
            }
        }

    }
}

