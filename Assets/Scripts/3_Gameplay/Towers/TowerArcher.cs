using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArcher : Tower
{

    private GameObject _bullet;
    public Transform m_StartBullet1;

    [Space(20)]
    public SpriteRenderer m_ArcherRender;
    public Sprite m_sprArcher_normal, m_sprArcher_attack;
    public GameObject m_objCricleRange;

    [Space(20)]
    public List<Sprite> LIST_TOWER_SPRITE;
    public SpriteRenderer sprTowerRender;
    public GameObject objEffMagicCircle;//hieu ung vong tron ma thuat cho level4


    public override void Init()
    {
        base.Init();
        objEffMagicCircle.SetActive(false);
    }


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

      //  StopCoroutine(Shot(_enemy));
        StartCoroutine(Shot(_enemy));

        //ANGLE
        //if (_enemy.CurrentPos().x > vCurrentPos.x)
        //    StartCoroutine(ArcherAnimation(180));
        //else
        //    StartCoroutine(ArcherAnimation(0));
    }

    WaitForSeconds _wait = new WaitForSeconds(0.2f);
    public IEnumerator Shot(Enemy _enemy)
    {

        if (eTowerLevel == TheEnumManager.TOWER_LEVEL.level_1 || eTowerLevel == TheEnumManager.TOWER_LEVEL.level_2)
        {
            if ( _enemy.isInGameplay())
            {
                _bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_TowerArcher).GetItem();
                if (_bullet)
                {
                    //sound
                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_archer);

                    _bullet.GetComponent<BulletArcher>().Shot(m_StartBullet1.position, _enemy, this);
                    _bullet.transform.position = m_StartBullet1.position;
                    _bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    _bullet.SetActive(true);
                }
            }

        }
        else
        {
            for (int i = 0; i < TOWER_DATA.GetNumberOfBullet(eTowerLevel); i++)
            {
                if (_enemy.isInGameplay())
                {
                    _bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_TowerArcher).GetItem();
                    if (_bullet)
                    {
                        //sound
                        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_archer);
                       // _bullet.transform.position = m_StartBullet1.position;
                        _bullet.GetComponent<BulletArcher>().Shot(m_StartBullet1.position, _enemy, this);

                        _bullet.SetActive(true);
                    }
                }
                yield return _wait;
            }
        }

    }




    public override void SetTowerRender(TheEnumManager.TOWER_LEVEL _level)
    {
        sprTowerRender.sprite = LIST_TOWER_SPRITE[(int)_level];
        if (_level == TheEnumManager.TOWER_LEVEL.level_4)
        {
            objEffMagicCircle.SetActive(true);
        }
    }
}
