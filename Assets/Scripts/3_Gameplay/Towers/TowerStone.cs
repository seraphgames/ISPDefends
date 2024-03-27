using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStone : Tower
{

    public GameObject _bullet;
    public Transform m_StartBullet;
    public GameObject m_objCricleRange;
    [Space(20)]
    public List<GameObject > LIST_TOWER_LEVEL;

   


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


        _bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_TowerStone).GetItem();
        if (_bullet)
        {
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_cannonner); //sound

            _bullet.GetComponent<BulletBezier>().Shot(m_StartBullet.position, _enemy, this);
           // _bullet.transform.position = m_StartBullet.position;
            _bullet.SetActive(true);
        }

    }

    public override void SetTowerRender(TheEnumManager.TOWER_LEVEL _level)
    {
        int _total = LIST_TOWER_LEVEL.Count;
        for (int i = 0; i < _total; i++)
        {
            if (i == (int)_level) LIST_TOWER_LEVEL[i].SetActive(true);
            else LIST_TOWER_LEVEL[i].SetActive(false  );
        }
    }
}
