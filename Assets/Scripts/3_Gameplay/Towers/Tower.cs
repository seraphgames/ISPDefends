using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tower : MonoBehaviour

{

    public TheEnumManager.TOWER eTower;
    public TheEnumManager.TOWER_LEVEL eTowerLevel;
    public TowerData TOWER_DATA;

    [Space(20)]
    protected Transform m_tranform;
    protected Vector3 vCurrentPos;



    [Space(20)]
    // public List<Enemy> LIST_ENEMY;
    // [HideInInspector]
    public Enemy CURRENT_ENEMY;
    Enemy NearestEnemy;//Enemy gan nhat
    private float fDistanceOfNearestEnemy;//khoang cach gan nhat


    //FATOR =========================
    public int iCurrentDamage;
    public float fCurrentFireRate;
    public float fCurrentRange;



    private void Awake()
    {
        //?  GetDataTowerConfig();
        m_tranform = transform;
        vCurrentPos = transform.position;
        vCurrentPos.z = vCurrentPos.y;
        transform.position = vCurrentPos;

    }


    // Use this for initialization
    void Start()
    {
        Init();
        ShowCircle();
        SetTowerRender(eTowerLevel);
    }

    //INIT
    virtual public void Init()
    {
        iCurrentDamage = TOWER_DATA.GetDamage(eTowerLevel);
        fCurrentFireRate = TOWER_DATA.GetFireRate(eTowerLevel);
        fCurrentRange = TOWER_DATA.GetRange(eTowerLevel);


        CancelInvoke("Attack");
        InvokeRepeating("Attack", 0.5f, fCurrentFireRate);

        CancelInvoke("FindCurrentEnemy");
        InvokeRepeating("FindCurrentEnemy", 0.0f, 0.5f);
    }



    //SHOW CIRCLE (Range circle)
    virtual public void ShowCircle()
    {

    }

    //SHOT
    private void Attack()
    {
        if (CURRENT_ENEMY)
            Attack(CURRENT_ENEMY);
    }
    virtual public void Attack(Enemy _enemy)
    {

    }

    public bool CheckEnemyInsideRange(Enemy _enemy)
    {
        if (Vector2.Distance(_enemy.GetCurrentPos(), GetCurrentPos()) < fCurrentRange)
            return true;
        else return false;
    }

    //GET CURRENT POS
    private Vector2 GetCurrentPos()
    {
        return m_tranform.position;
    }

    //FIND CURRENT ENEMY    
    virtual public void FindCurrentEnemy()
    {
        CURRENT_ENEMY = TheEnemyPooling.Instance.FindNearestEnemy(vCurrentPos, fCurrentRange);

    }




    //UPGRADE and SELL
    public void UpgradeTower()
    {
        if (TheLevel.Instance.iOriginalCoin >= TOWER_DATA.GetPriceToUpgrade(eTowerLevel))
        {
            TheLevel.Instance.iOriginalCoin -= TOWER_DATA.GetPriceToUpgrade(eTowerLevel);
            TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event

            #region UPGRADE
            if (eTowerLevel != TheEnumManager.TOWER_LEVEL.level_4)
            {
                eTowerLevel++;
                //sound
                TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.tower_upgrade);
                Init();
                ShowCircle();
                SetTowerRender(eTowerLevel);
            }
            #endregion
        }
        else
        {
            Debug.Log("NOT ENOUGHT COIN");
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
            Note.Instance.ShowPopupNote(Note.NOTE.NotEnoughtCoin);

        }


    }
    public void SellTower()
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);//sound
        //  print("Sell tower");
        TheLevel.Instance.iOriginalCoin += TOWER_DATA.GetPriceToSell(eTowerLevel);
        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event

        Destroy(gameObject);
    }




    //SET RENDER FOLLOW LEVEL
    virtual public void SetTowerRender(TheEnumManager.TOWER_LEVEL _level)
    {

    }


}
