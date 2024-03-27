using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMagic : Tower
{
    public SpriteRenderer m_WitchSpriterRenderer;
    public Sprite m_spriteWitch_normal, m_spriteWitch_attack;
    public GameObject m_objCricleRange;
    public GameObject objEffMagicCircle;//hieu ung vong tron ma thuat cho level4

    [Space(20)]
    public Transform m_tranStartBullet;
    GameObject _bullet;


    [Space(20)]
    public List<Sprite> LIST_TOWER_SPRITE;
    public SpriteRenderer sprTowerRender;



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

        //ANGLE
        if (_enemy.GetCurrentPos().x > vCurrentPos.x)
            StartCoroutine(AnimationWitch(180));
        else
            StartCoroutine(AnimationWitch(0));

        //BULLET
        _bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_TowerMagic).GetItem();
        if (_bullet)
        {

            //sound
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_magic);

            _bullet.GetComponent<BulletMove>().Shot(m_tranStartBullet.position, _enemy, this);
           // _bullet.transform.position = m_tranStartBullet.position;
            _bullet.SetActive(true);
        }


    }


    WaitForSeconds _wait = new WaitForSeconds(0.3f);
    public IEnumerator AnimationWitch(float _angle)
    {
        m_WitchSpriterRenderer.sprite = m_spriteWitch_attack;
        yield return _wait;
        m_WitchSpriterRenderer.sprite = m_spriteWitch_normal;
        m_WitchSpriterRenderer.transform.eulerAngles = new Vector3(0, _angle, 0);
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
