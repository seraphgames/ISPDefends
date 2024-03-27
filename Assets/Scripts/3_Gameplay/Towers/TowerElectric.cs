using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerElectric : Tower
{
    private GameObject _bullet;
    public GameObject m_objCricleRange;
    public GameObject m_objThunder;

    [SerializeField]
    private Transform m_tranBulletPos;
    private TheElectric m_MainTheElectric1;
    private TheElectric m_MainTheElectric2;


    [Space(20)]
    public List<GameObject> LIST_TOWER_LEVEL;
    public List<TheElectric> LIST_ELECTRIC_LEVEL;


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


        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_thunder); //sound


        if (m_MainTheElectric1)
            m_MainTheElectric1.ShowElectric(_enemy.m_tranBodyPoint.position);//thunder line
        if (m_MainTheElectric2)
            m_MainTheElectric2.ShowElectric(_enemy.m_tranBodyPoint.position);//thunder line


        StartCoroutine(ShowThunder(_enemy.m_tranBodyPoint.position));
        _enemy.HEALTH.ReduceHp(TOWER_DATA.GetDamage(eTowerLevel));

    }



    WaitForSeconds _wait = new WaitForSeconds(0.2f);
    private IEnumerator ShowThunder(Vector3 _pos)
    {
        m_objThunder.transform.position = _pos + new Vector3(0, 0, -0.02f);

        switch (eTowerLevel)
        {
            case TheEnumManager.TOWER_LEVEL.level_1:
                m_objThunder.transform.localScale = Vector3.one * 2.0f;
                break;
            case TheEnumManager.TOWER_LEVEL.level_2:
                m_objThunder.transform.localScale = Vector3.one * 2.3f;
                break;
            case TheEnumManager.TOWER_LEVEL.level_3:
                m_objThunder.transform.localScale = Vector3.one * 2.6f;
                break;
            case TheEnumManager.TOWER_LEVEL.level_4:
                m_objThunder.transform.localScale = Vector3.one * 3.0f;
                break;
        }

        m_objThunder.SetActive(true);
        yield return _wait;
        m_objThunder.SetActive(false);
    }

    public override void SetTowerRender(TheEnumManager.TOWER_LEVEL _level)
    {
        int _total = LIST_TOWER_LEVEL.Count;
        for (int i = 0; i < _total; i++)
        {
            if (i == (int)_level)
            {
                LIST_TOWER_LEVEL[i].SetActive(true);
                if (i == 3)
                {
                    m_MainTheElectric1 = LIST_ELECTRIC_LEVEL[3];
                    m_MainTheElectric2 = LIST_ELECTRIC_LEVEL[4];
                }
                else
                {
                    m_MainTheElectric1 = LIST_ELECTRIC_LEVEL[i];
                    m_MainTheElectric2 = null;
                }
            }
            else
                LIST_TOWER_LEVEL[i].SetActive(false);
        }
    }
}
