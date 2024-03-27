using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Tower
{

    public enum STATUS
    {
        Idiel,
        Moving,
        Attacking,
        Die,

    }

    public STATUS eStatus;
    public AudioClip m_auSoundDie;

    public float fTimelife = 30;

    [Space(20)]
    public Transform m_tranRender;
    public Transform m_tranHp;
    public Transform m_tranOfBullet;




    private Vector2 vDetalPos;
    public Vector2 vOldPos;

    [Space(20)]
    public Animator m_animator;
    public AnimationClip m_aniMove, m_aniAttack, m_aniIdie, m_aniDie;

    private int iCurrentHP;


    //GET DATA
    public void TimeLife()
    {

        //Upgrade system - timelife
        UpgradeConfig _temp1 = TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.Reinforcement_MoreTimelife30Percent);
        UpgradeConfig _temp2 = TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.Reinforcement_MoreTimelife50Percent);
        float _time1 = 0, _time2 = 0;

        if (_temp1.ACTIVED) _time1 = _temp1.GetResuftValueConfig_Percent(fTimelife);
        if (_temp2.ACTIVED) _time2 = _temp2.GetResuftValueConfig_Percent(fTimelife);

        Invoke("Die", fTimelife);

    }


    //SET STATUS
    private void SetStatus(STATUS _status)
    {
        eStatus = _status;
        switch (eStatus)
        {
            case STATUS.Idiel: //IDIE
                m_animator.Play(m_aniIdie.name);

                break;


            case STATUS.Moving: //MOVING
                                // Debug.Log("move");
                m_animator.Play(m_aniMove.name);
                break;


            case STATUS.Attacking: //ATTACK

                m_animator.Play(m_aniAttack.name);
                break;

            case STATUS.Die: //ATTACK
                             // TheObjPoolingManager.Instance.RemoveSoldierFromListSoldierInGameplay(this);
                Die();
                break;
        }
    }



    //INIT
    public override void Init()
    {
        base.Init();
        iCurrentHP = TOWER_DATA.GetHp();
        vOldPos = GetCurrentPos();

        // TheSkillManager.OnAddHpForHero += SkillAddMoreHP;
        //TheObjPoolingManager.Instance.AddToSoldierListGameplay(this);
        if (eTower == TheEnumManager.TOWER.soldier)
        {
            vDetalPos = new Vector2(Random.Range(0.2f, 0.8f), Random.Range(-0.2f, 0.2f));
            SetStatus(STATUS.Moving);
            vTargetPos = vOldPos;
        }
        else
        {
            vOldPos = GetCurrentPos();
            vTargetPos = GetCurrentPos();
            vDetalPos = new Vector2(0.5f, 0.15f);
            SetStatus(STATUS.Idiel);
        }
        m_tranHp.localScale = vHpBar;

        TimeLife();
    }

    // Update is called once per frame
    void Update()
    {
        if (eStatus == STATUS.Die) return;


        MoveToEnemy(CURRENT_ENEMY);

    }


    //ATTACK ENEMY
    //private GameObject _bullet;
    public override void Attack(Enemy _enemy)
    {
        if (TheGameStatusManager.CURRENT_STATUS != TheGameStatusManager.GAME_STATUS.Playing) return;
        if (eStatus == STATUS.Die) return;
        if (!_enemy.isInGameplay()) return;

        // if (_enemy.m_enemyData.bIsAirForece) return;


        SetStatus(STATUS.Attacking);
        if (Vector2.Distance(_enemy.GetCurrentPos(), GetCurrentPos()) < 1.0f)
        {

            TheSound.Instance.PlayerSoundSoldierAttack();//sound
            _enemy.SetSoldierAttack(this);
            _enemy.HEALTH.ReduceHp(TOWER_DATA.GetDamage(eTowerLevel));

        }


    }


    //FIND ENEMY TARGET
    public override void FindCurrentEnemy()
    {
        switch (eTower)
        {
            case TheEnumManager.TOWER.soldier:
                CURRENT_ENEMY = TheEnemyPooling.Instance.FindNearestEnemy(vCurrentPos, fCurrentRange, TheEnumManager.ENEMY_KIND.Infantry);
                break;
        }
    }


    //HIT DAMAGE FROM ENEMY
    private GameObject objBloodEff, objBloodMark;
    public void ReduceHP(int _damage)
    {
        //Eff
        objBloodEff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodOfEnemy).GetItem();
        if (objBloodEff)
        {
            objBloodEff.transform.position = vCurrentPos + new Vector3(0, 0.3f, 0);
            objBloodEff.SetActive(true);
        }

        iCurrentHP -= _damage;
        ShowHpBar(m_tranHp, iCurrentHP, TOWER_DATA.GetHp());
        // Debug.Log("Soldier's HP: " + m_myTowerData.iHP);
        if (iCurrentHP <= 0)
        {
            //eff
            objBloodMark = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodMark).GetItem();
            if (objBloodMark)
            {
                objBloodMark.transform.position = vCurrentPos;
                objBloodMark.SetActive(true);
            }

            // Destroy(this.gameObject);
            SetStatus(STATUS.Die);
        }
    }

    //SHOW HP BAR
    private Vector3 vHpBar = new Vector3(1.0f, 0.9f, 0);
    private void ShowHpBar(Transform _bar, float _currentHp, float _maxHp)
    {
        if (!_bar) return;

        vHpBar.x = _currentHp / _maxHp;
        if (vHpBar.x < 0) vHpBar.x = 0;
        _bar.localScale = vHpBar;

    }

    //MOVE
    Vector2 vTargetPos;
    private void MoveToEnemy(Enemy _enemy)
    {
        // if (eTower == TheEnumManager.TOWER.HeroArcher_Hera) return;

        if (_enemy && _enemy.isInGameplay())
        {

            vTargetPos = _enemy.GetCurrentPos() + vDetalPos;

            if ((Vector2)vCurrentPos == vTargetPos)
            {
                SetStatus(STATUS.Attacking);
            }
            else
            {
                SetStatus(STATUS.Moving);
            }

        }
        else
        {

            if (eStatus == STATUS.Attacking)
            {
                SetStatus(STATUS.Moving);
                vTargetPos = vOldPos;
                CURRENT_ENEMY = null;
            }


            if ((Vector2)vCurrentPos == vTargetPos)
            {
                SetStatus(STATUS.Idiel);
            }
        }

        MoveTo(vTargetPos);
    }



    //SKILL ADD BLOOD
    private void SkillAddMoreHP(Vector2 _pos)
    {
        float _dis = Vector2.Distance(_pos, vCurrentPos);
        if (_dis < 2.0f)
        {
            StartCoroutine(IeAddHp());
        }
    }
    private IEnumerator IeAddHp()
    {
        float _speedToAddHp = 1.0f;

        //if (TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.speed_add_hp_for_solider).isActived)
        //{
        //    _speedToAddHp = 0.5f;
        //}


        for (int i = 0; i < 5; i++)
        {
            if (iCurrentHP < TOWER_DATA.GetHp())
                iCurrentHP += 30;
            else
                iCurrentHP = TOWER_DATA.GetHp();
            ShowHpBar(m_tranHp, iCurrentHP, TOWER_DATA.GetHp());
            yield return new WaitForSeconds(_speedToAddHp);
        }
    }




    //MOVE TO TARGET
    Vector3 vRight = new Vector3(0, 180, 0);
    Vector3 vLeft = Vector3.zero;
    // private Vector2 vInputPos;
    private void MoveTo(Vector2 _target)
    {


        if (_target.x > vCurrentPos.x)
        {
            m_tranRender.eulerAngles = vRight;
        }
        else
        {
            m_tranRender.eulerAngles = vLeft;
        }
        vCurrentPos = m_tranform.position;
        vCurrentPos = Vector2.MoveTowards(vCurrentPos, _target, Time.deltaTime * TOWER_DATA.fSpeedMove);
        vCurrentPos.z = vCurrentPos.y;
        m_tranform.position = vCurrentPos;

        //if ((Vector2)vCurrentPos == _target)
        //{
        //    m_animator.Play(m_aniIdie.name);

        //}

    }
    public void SetMove(Vector2 _pos)
    {
        vTargetPos = _pos;
        vOldPos = _pos;
        SetStatus(STATUS.Moving);

    }

    public Vector2 GetCurrentPos()
    {
        if (m_tranform)
            return m_tranform.position;
        return Vector2.one * 1000;
    }



    //ROTATION
    private void Rotation(float angle)
    {
        m_tranRender.transform.eulerAngles = new Vector3(0, angle, 0);
    }




    //DIE
    private void Die()
    {
        objBloodEff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodOfEnemy).GetItem();
        if (objBloodEff)
        {
            objBloodEff.transform.position = vCurrentPos + new Vector3(0, 0.3f, 0);
            objBloodEff.SetActive(true);
        }

        TheSound.Instance.PlaySound(m_auSoundDie);//sound

        CancelInvoke();
        Destroy(this.gameObject);
    }

}
