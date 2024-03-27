using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum STATUS
    {
        Init,
        Moving,
        Attacking,
        Die,
        CompleteMission, // finish road
    }



    public TheEnumManager.ENEMY eEnemyID;
    public STATUS eStatus;
    private bool ALIVE;


    public AudioClip m_auEnemyDie;
    // public bool bIsBoss;
    //private bool bIsSlowdown;


    public Health HEALTH = new Health();
    private EnemyMove m_EnemyMove;
    private Transform m_tranform;
    private GameObject m_gameobject;

    public Transform m_tranBodyPoint; //pos to shoot
    public Transform tranHPBar;// Blood Bar

    private Soldier CURRENT_SOLDIER_TARGET; //Target to attack
    //Animation
    private EnemyAnimation m_enemyAnimation;


    //CONFIG
    [HideInInspector]
    public EnemiesData DATA;




    private void Awake()
    {
        m_gameobject = gameObject;
        m_tranform = transform;
        m_EnemyMove = GetComponent<EnemyMove>();
        m_enemyAnimation = GetComponent<EnemyAnimation>();
        DATA = TheDataManager.Instance.ENEMY_DATA_MANAGER.Get(eEnemyID);
    }

    private void Start()
    {
        SetStatus(STATUS.Init);//init
    }




    //STATUS  

    public void SetStatus(STATUS _status)
    {
        eStatus = _status;
        switch (eStatus)
        {
            case STATUS.Init://INIT               
                HEALTH.Init(this);
                m_EnemyMove.Init();
                ALIVE = true;

                TheEventManager.PostEvent_OnEnemyIsBorn(this);//event
                SetStatus(STATUS.Moving);
                break;


            case STATUS.Moving://MOVING
                if (DATA.bIsBoss)
                    m_enemyAnimation.Play(EnemyAnimation.ANI.Move, 0.5f);
                else
                    m_enemyAnimation.Play(EnemyAnimation.ANI.Move);
                break;


            case STATUS.Attacking://ATTACK
                m_EnemyMove.Rotation(0);
                m_enemyAnimation.Play(EnemyAnimation.ANI.Attack);
                break;


            case STATUS.Die: //DIE
                ALIVE = false;

                //sound
                if (m_auEnemyDie)
                    TheSound.Instance.PlaySound(m_auEnemyDie);//sound die

                TheEventManager.PostEvent_OnEnemyIsDetroyOnRoad(this);//event


                m_tranform.position = Vector2.one * 1000;
                m_gameobject.SetActive(false);
                break;


            case STATUS.CompleteMission: //COMPLETE
                ALIVE = false;
                TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.heart);//sound             
                TheEventManager.PostEvent_OnEnemyCompletedRoad(this);//event


                m_tranform.position = Vector2.one * 1000;
                m_gameobject.SetActive(false);
                break;


        }
    }
    public bool isStatus(STATUS _status)
    {
        return eStatus == _status;
    }



    //CURRENT POS
    public Vector2 GetCurrentPos()
    {
        return m_tranform.position;
    }

    public bool isInGameplay()
    {
        if (!ALIVE) return false;
        if (GetCurrentPos().y > 4.8f) return false;
        if (GetCurrentPos().y < -4.8f) return false;
        if (GetCurrentPos().x < -8.7f) return false;
        if (GetCurrentPos().x > 8.7f) return false;

        return true;
    }



    //ATTACK SOLDIER
    public void SetSoldierAttack(Soldier _soldier)
    {
        CURRENT_SOLDIER_TARGET = _soldier;
        if (!IsInvoking("AttackSoldier"))
        {
            InvokeRepeating("AttackSoldier", 0.05f, DATA.fConfig_AttackSpeed);
        }
    }
    private void AttackSoldier()
    {
        if (Vector2.Distance(m_tranform.position, CURRENT_SOLDIER_TARGET.GetCurrentPos()) < DATA.fConfig_RangeToAttack)
        {
            if (!bIsFreeze)
            {
                SetStatus(STATUS.Attacking);
                if (CURRENT_SOLDIER_TARGET)
                    CURRENT_SOLDIER_TARGET.ReduceHP(DATA.iConfig_Damage);
            }


            //ROTATION
            if (CURRENT_SOLDIER_TARGET.GetCurrentPos().x > GetCurrentPos().x)
            {
                m_EnemyMove.Rotation(0);
            }
            else
            {
                m_EnemyMove.Rotation(180);
            }
        }
        else
        {
            // CancelInvoke("AttackSoldier");
            if (eStatus != STATUS.Moving)
            {
                SetStatus(STATUS.Moving);
                CancelInvoke("AttackSoldier");
            }
        }
    }



    #region SKILL EFFECT
    //SKILL FREEZE
    private bool bIsFreeze = false;//dang bi dong bang

    private void ExitFreezingMode()
    {
        bIsFreeze = false;
        m_EnemyMove.fMoveSpeed = DATA.GetSpeed();
        SetStatus(STATUS.Moving);
    }



    //SKILL: POND OF WATER
    WaitForSeconds _wait = new WaitForSeconds(1.0f);
    public void HitPondOfPoison()
    {
        StartCoroutine(IeHitPondOfPoison());
    }
    private IEnumerator IeHitPondOfPoison()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return _wait;
            HEALTH.ReduceHp(10 + (int)(HEALTH.iOriginalHp * 0.05f));
        }
    }


    //SKILL: HIT ROCKET
    private void HitRocket(Vector2 _pos, int _damage)
    {
        float _distance = Vector2.Distance(m_tranBodyPoint.position, _pos);

        if (_distance < 0.65f)
            HEALTH.ReduceHp(_damage);

    }

    private void HandlePowerUp(TheEnumManager.POWER_UP _powerUp, Vector2 _pos)
    {
        float _distance = Vector2.Distance(GetCurrentPos(), _pos);
        UpgradeConfig _temp = new UpgradeConfig();
        switch (_powerUp)
        {
            case TheEnumManager.POWER_UP.guardian:
                break;
            case TheEnumManager.POWER_UP.freeze:
                if (_distance <= 3.0f)
                {
                    bIsFreeze = true;
                    m_enemyAnimation.Stop();
                    m_EnemyMove.fMoveSpeed = 0;

                    //Upgrade system
                    //  UpgradeData _temp = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Skill_MoreTimeForFreeze);
                    _temp = TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.Skill_MoreTimeForFreeze);
                    float _time = _temp.fValueDefaul;
                    if (_temp.ACTIVED)
                        _time = _temp.GetResuftValueConfig_Percent(_time);


                    Invoke("ExitFreezingMode", _time);

                    //eff
                    GameObject _eff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.FreezeEff).GetItem();
                    if (_eff)
                    {
                        _eff.transform.position = m_tranBodyPoint.position - new Vector3(0, 0, 0.01f);
                        if (DATA.bIsBoss)
                            _eff.transform.localScale = Vector3.one * 2.0f;
                        else
                            _eff.transform.localScale = Vector3.one;

                        _eff.GetComponent<TimeLife>().fTimelife = _time;
                        _eff.SetActive(true);
                    }
                }
                break;

            case TheEnumManager.POWER_UP.fire_of_lord:

                //upgrade system
                // UpgradeData _temp = TheDataManager.Instance.GetUpgradeData(TheEnumManager.UPGRADE.Skill_MoreRangeForFireFromSky);
                _temp = TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.Skill_MoreRangeForFireFromSky);
                float _distanceOfFireFormSky = _temp.fValueDefaul;
                if (_temp.ACTIVED)
                    _distanceOfFireFormSky = _temp.GetResuftValueConfig_Percent(_distanceOfFireFormSky);


                if (_distance < _distanceOfFireFormSky)
                    HEALTH.ReduceHp(100);
                break;


            case TheEnumManager.POWER_UP.mine_on_road:

                _temp = TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.Skill_MoreRangeForFireFromSky);
                float _distanceOfMine = _temp.fValueDefaul;
                if (_temp.ACTIVED)
                    _distanceOfMine = _temp.GetResuftValueConfig_Percent(_distanceOfMine);


                if (_distance < _distanceOfMine)
                    HEALTH.ReduceHp(100);
                break;

            case TheEnumManager.POWER_UP.poison:
                break;
            case TheEnumManager.POWER_UP.Null:
                break;
        }
    }


    private void OnEnable()
    {
        TheSkillManager.Instance.OnUsedPower += HandlePowerUp;


        TheEventManager.OnRocketHit += HitRocket;
    }



    private void OnDisable()
    {
        TheSkillManager.Instance.OnUsedPower -= HandlePowerUp;
        TheEventManager.OnRocketHit -= HitRocket;
    }
}


public class Health
{
    private Enemy m_Enemy;
    private Transform TranHpBar;
    public int iOriginalHp;
    private int iCurrentHp;

    public void Init(Enemy _ene)
    {
        m_Enemy = _ene;
        TranHpBar = m_Enemy.tranHPBar;


        int _temp = m_Enemy.DATA.GetHp(TheLevel.Instance.iCurrentWave + 1, TheLevel.Instance.iMAX_WAVE_CONFIG);
        iOriginalHp = _temp;
        iCurrentHp = _temp;

        //show bar
        ShowHpBar(1.0f);//show Hp Bar  
    }


    public float GetFactor()
    {
        return iCurrentHp * 1.0f / iOriginalHp;
    }


    public void ReduceHp(int _hp)
    {
        if (!m_Enemy.isInGameplay()) return;
        if (!m_Enemy.m_tranBodyPoint) return;

        //Blood eff
        GameObject objBloodEff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodOfEnemy).GetItem();
        if (objBloodEff)
        {
            objBloodEff.transform.position = m_Enemy.m_tranBodyPoint.position;
            objBloodEff.SetActive(true);
        }


        iCurrentHp -= _hp;
        if (iCurrentHp < 0)
        {
            iCurrentHp = 0;
            ShowMarkOfBlood();//blood mark
            m_Enemy.SetStatus(Enemy.STATUS.Die);
        }


        //show bar
        ShowHpBar(GetFactor());//show Hp Bar  


    }


    //EFFECT BLOOD MARK
    private GameObject objBloodMark;
    private void ShowMarkOfBlood()
    {
        if (m_Enemy.DATA.bIsBoss)
        {
            //blood mark
            objBloodMark = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_of_mine).GetItem();
            if (objBloodMark)
            {
                objBloodMark.transform.position = m_Enemy.GetCurrentPos();
                objBloodMark.SetActive(true);
            }
        }
        else
        {

            //text effect
            objBloodMark = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.EffEnemyText).GetItem();
            if (objBloodMark)
            {
                objBloodMark.transform.position = m_Enemy.GetCurrentPos();
                objBloodMark.SetActive(true);
            }

            //blood mark
            if (m_Enemy.DATA.bIsInfantry)
            {
                objBloodMark = null;
                objBloodMark = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.BloodMark).GetItem();
                if (objBloodMark)
                {
                    objBloodMark.transform.position = m_Enemy.GetCurrentPos();
                    objBloodMark.SetActive(true);
                }
            }
        }
    }


    //HP BAR
    private Vector3 vHpBar = new Vector3(1.0f, 0.9f, 0);
    private void ShowHpBar(float _factorHp)
    {
        vHpBar.x = _factorHp;
        TranHpBar.localScale = vHpBar;
    }
    #endregion

}



