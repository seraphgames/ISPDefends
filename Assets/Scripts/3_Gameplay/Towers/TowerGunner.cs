using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGunner : Tower
{

    private GameObject _bullet;
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
        if (_enemy.eStatus == Enemy.STATUS.Die) return;


        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_gunmen);//sound
        _enemy.HEALTH.ReduceHp(TOWER_DATA.GetDamage (eTowerLevel));
        ShowFire(_enemy.GetCurrentPos());      
    }


   



    public override void SetTowerRender(TheEnumManager.TOWER_LEVEL _level)
    {
        sprTowerRender.sprite = LIST_TOWER_SPRITE[(int)_level];
        if(_level==TheEnumManager.TOWER_LEVEL.level_4)
        {
            objEffMagicCircle.SetActive(true);
        }
    }

    //SHOW FIRE
    public GameObject objFire_0, objFire_45, objFire_90, objFire_135, objFire_180;
    float fAngle;
    private void ShowFire(Vector2 _posOfEnemy)
    {
        //left
        if (_posOfEnemy.x <= vCurrentPos.x)
        {
            if(_posOfEnemy.y>vCurrentPos.y)
            {
                objFire_180.SetActive(true);
            }
            else if ((vCurrentPos.x-_posOfEnemy.x)>((vCurrentPos.y - _posOfEnemy.y)))
            {
                objFire_135.SetActive(true);
            }
            else objFire_90.SetActive(true);


        }
        //right
        else
        {
            if (_posOfEnemy.y > vCurrentPos.y)
            {
                objFire_0.SetActive(true);
            }
            else if (( _posOfEnemy.x - vCurrentPos.x) > ((_posOfEnemy.y - vCurrentPos.y)))
            {
                objFire_45.SetActive(true);
            }
            else objFire_90.SetActive(true);
        }
    }
}
