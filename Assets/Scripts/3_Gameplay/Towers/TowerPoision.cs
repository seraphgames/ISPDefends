using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoision : Tower
{


    private GameObject _bullet;
    public GameObject m_objCricleRange;
    [SerializeField]
    private Transform m_tranBulletPos;
    [Space(20)]
    public List<GameObject> LIST_TOWER_LEVEL;
    // public SpriteRenderer sprTowerRender;


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

        _bullet = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Bullet_Posion).GetItem();
        if (_bullet)
        {
            
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_poison);//sound
            _bullet.GetComponent<BulletBezier>().Shot(m_tranBulletPos.position, _enemy, this);
           // _bullet.transform.position = m_tranBulletPos.position;
            _bullet.SetActive(true);
        }

    }

    public override void SetTowerRender(TheEnumManager.TOWER_LEVEL _level)
    {
        int _total = LIST_TOWER_LEVEL.Count;
        for (int i = 0; i < _total; i++)
        {
            if (i ==(int) _level) LIST_TOWER_LEVEL[i].SetActive(true);
            else LIST_TOWER_LEVEL[i].SetActive(false);
        }
        //sprTowerRender.sprite = LIST_TOWER_LEVEL[(int)_level];
    }
}
