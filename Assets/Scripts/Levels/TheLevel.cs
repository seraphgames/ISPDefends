using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLevel : MonoBehaviour
{
    public static TheLevel Instance;


    [Header("*** Config ***")]
    public TheEnumManager.KIND_OF_MAPS eKindOfMap;
    public int iOriginalHeart = 10;
    public int iOriginalCoin = 220;// Coin ban dau
    public int iMAX_WAVE_CONFIG;
    public int iBasePriceForTowerPos = 25; // Giá cơ bản cho mỗi TowerPos - Dùng để tính toán coin ban đầu cho các level;

    [Space(20)]
    [Header("Enemies and Boss in level")]
    public GameObject OBJ_BOSS; // BOSS
    public List<TheEnumManager.ENEMY> LIST_CONFIG_ENEMY_FOR_LEVEL;




    [Space(20)]
    [Header("===============================")]
    public Transform GROUP_ROADS;
    public int iCurrentHeart;
    public int iCurrentWave = -1;
    public List<TheRoad> LIST_THE_ROAD;

    [Space(20)]
    [Header("Starting mark in level")]
    public Transform GROUP_STARTING_MARK;
    public List<TheStartingWaveMark> LIST_STARTING_MARK;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        LIST_THE_ROAD = new List<TheRoad>(); //all road in game
        LIST_STARTING_MARK = new List<TheStartingWaveMark>();

    }

    private void Start()
    {
        //Create zombie pool
        TheEnemyPooling.Instance.Init(LIST_CONFIG_ENEMY_FOR_LEVEL);//Create zombie pool


        iCurrentHeart = iOriginalHeart;
        iOriginalCoin = CalculationCoinForLevel();// TÍNH COIND BAN ĐẦU CHO LEVEL

        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
        TheEventManager.PostGameEvent_OnStartWave();//event
        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event

        //Data Analytics
        TheEventManager.EventGameStart();
        TheGameStatusManager.SetGameStatus(TheGameStatusManager.GAME_STATUS.Playing);

        Invoke("ConfigAllWave", 0.5f);
    }

    //TÍNH TOÁN COIN BAN ĐẦU CHO LEVEL
    private int CalculationCoinForLevel()
    {
        //********   Tính theo số lượng Tower Pos có trong level     ******************
        //int _totalTowerPos = this.transform.Find("TowerPos").childCount;
        //if (_totalTowerPos == 0) _totalTowerPos = 6;
        //return iBasePriceForTowerPos * _totalTowerPos;



        //****************** Tính theo max wave ******************
        return iMAX_WAVE_CONFIG * iBasePriceForTowerPos;
    }


    [System.Serializable]
    public class UnitWave
    {
        public TheEnumManager.ENEMY eEnemy;
        public int iNumberOfGroup; // Có m bao nhiêu nhóm đi ra,
        public int iNumberZombieOfEachGroup;//Mỗi nhóm có n zombie

        public TheRoad m_TheRoad;
        public TheStartingWaveMark m_WaveMark;

        public void Init(TheEnumManager.ENEMY _Enemy, TheRoad _theRoad, int _numberOfGroup, int _numberZombieOfEachGroup)
        {
            eEnemy = _Enemy;
            m_TheRoad = _theRoad;
            iNumberOfGroup = _numberOfGroup;
            iNumberZombieOfEachGroup = _numberZombieOfEachGroup;
            m_WaveMark = Instance.FindNeasestWaveMark(_theRoad.LIST_POS[0]);
        }

        public int GetTotalZombie()
        {
            return iNumberOfGroup * iNumberZombieOfEachGroup;
        }

        public void ShowWaveMark()
        {
            m_WaveMark.gameObject.SetActive(true);
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int iIndexOfWave;
        public int iTotalRoadSameTime;

        public List<UnitWave> LIST_UNIT_WAVE;

        public void Init(int _totalRoadSameTime, int _totalGroupOfEachKindOfZombie, int _numberZombieOffEachGroup)
        {
            iTotalRoadSameTime = _totalRoadSameTime;
            LIST_UNIT_WAVE = new List<UnitWave>();
            for (int i = 0; i < _totalRoadSameTime; i++)
            {
                UnitWave _newEnemy = new UnitWave();

                //config road              
                TheRoad _TheRoad = Instance.LIST_THE_ROAD[UnityEngine.Random.Range(0, Instance.LIST_THE_ROAD.Count)];
                TheEnumManager.ENEMY _enemy = Instance.LIST_CONFIG_ENEMY_FOR_LEVEL[UnityEngine.Random.Range(0, Instance.LIST_CONFIG_ENEMY_FOR_LEVEL.Count)];
                _newEnemy.Init(_enemy, _TheRoad, _totalGroupOfEachKindOfZombie, _numberZombieOffEachGroup);


                LIST_UNIT_WAVE.Add(_newEnemy);
            }
        }

        //Count total zombie in wave
        public int GetTotalZombieInWave()
        {
            int _total = 0;
            int length = LIST_UNIT_WAVE.Count;
            for (int i = 0; i < length; i++)
            {
                _total += LIST_UNIT_WAVE[i].GetTotalZombie();
            }
            return _total;
        }


        //Show wave mark
        public void ShowWaveMark()
        {
            for (int i = 0; i < LIST_UNIT_WAVE.Count; i++)
            {
                LIST_UNIT_WAVE[i].ShowWaveMark();
            }
        }


        //is last wave
        public bool IsLastWave()
        {
            return iIndexOfWave == Instance.LIST_WAVE.Count - 1;
        }

    }

    [Header("_____WAVE___________")]
    [Space(30)]
    public List<Wave> LIST_WAVE;
    public void ConfigAllWave()
    {
        LIST_WAVE = new List<Wave>();
        for (int _waveIndex = 0; _waveIndex < iMAX_WAVE_CONFIG; _waveIndex++)
        {
            Wave _newWave = new Wave();
            _newWave.iIndexOfWave = _waveIndex;
            int _totalRoadSameTime = 0;//So road có enemy cùng 1 wave;
            int _totalGroupOfEachKindOfZombie = 0;//mỗi lần đi có n nhóm.
            int _numberZombieOffEachGroup = 0; //Mỗi nhóm có bn enemy?


            #region CONFIG WAVE
            if (_waveIndex <= 4)//1
            {
                _totalRoadSameTime = 1;
                _totalGroupOfEachKindOfZombie = 4;
                _numberZombieOffEachGroup = 5;
            }
            else if (_waveIndex > 4 && _waveIndex <= 8)//2
            {
                _totalRoadSameTime = 2;
                _totalGroupOfEachKindOfZombie = 4;
                _numberZombieOffEachGroup = 5;
            }
            else if (_waveIndex > 8 && _waveIndex <= 18)//3
            {
                _totalRoadSameTime = 3;
                _totalGroupOfEachKindOfZombie = 4;
                _numberZombieOffEachGroup = 5;
            }
            else
            {
                _totalRoadSameTime = 3;
                _totalGroupOfEachKindOfZombie = 5;
                _numberZombieOffEachGroup = 6;
            }


            _newWave.Init(_totalRoadSameTime, _totalGroupOfEachKindOfZombie, _numberZombieOffEachGroup);
            LIST_WAVE.Add(_newWave);
        }

        //wave 1
        CheckNextWave();
    }
    public Wave GetWave(int _currentWave)
    {
        if (_currentWave < LIST_WAVE.Count)
            return LIST_WAVE[_currentWave];
        return null;
    }




    // THUẬT TOÁN SINH ENEMY
    public int iTotalNumberEnemyOfWave = 0;
    private IEnumerator IeStartUnitWave(UnitWave _unitwave)
    {
        Enemy _enemy;
        for (int k = 0; k < _unitwave.iNumberOfGroup; k++)
        {
            for (int i = 0; i < _unitwave.iNumberZombieOfEachGroup; i++)
            {
                _enemy = TheEnemyPooling.Instance.GetZombiePool(_unitwave.eEnemy).GetEnemy();
                _enemy.gameObject.SetActive(true);
                _enemy.GetComponent<EnemyMove>().SetRoad(_unitwave.m_TheRoad);
                _enemy.GetComponent<Enemy>().SetStatus(Enemy.STATUS.Init);//Init

                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2.0f));
            }
            yield return new WaitForSeconds(4.0f);
        }
    }




    //CHECK TO START NEW WAVE.
    private void CheckNextWave()
    {
        if (iTotalNumberEnemyOfWave <= 0)
        {
            iTotalNumberEnemyOfWave = 0;



            Wave _nextWave = GetWave(iCurrentWave + 1);
            if (_nextWave != null)
            {
                _nextWave.ShowWaveMark();
            }
            else
            {
                //victory
                if (iCurrentHeart > 0)
                {
                    //WIN
                    TheGameStatusManager.SetGameStatus(TheGameStatusManager.GAME_STATUS.Victory);
                }
                else
                {
                    //GAME OVER
                    TheGameStatusManager.SetGameStatus(TheGameStatusManager.GAME_STATUS.Gameover);
                }
            }
        }
    }
    private void StarNewWave()
    {

        iCurrentWave++;

        Wave _wave = GetWave(iCurrentWave);
        if (_wave != null)
        {
            TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
            HideAllRoadMark();

            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.battle_last_wave);//sound
            Instance.iTotalNumberEnemyOfWave = _wave.GetTotalZombieInWave();

            //-----------------------------------
            if (_wave.IsLastWave())
            {
                //last wave             
                if (UIGameplay.Instance)
                    UIGameplay.Instance.ShowLastWaveText();
            }


            //START WAVE
            for (int j = 0; j < _wave.iTotalRoadSameTime; j++)
            {
                StartCoroutine(IeStartUnitWave(_wave.LIST_UNIT_WAVE[j]));
            }

            #region BOSS
            if (OBJ_BOSS != null && _wave.IsLastWave())
            {
                iTotalNumberEnemyOfWave += 1; //BOSS 
                if (OBJ_BOSS)
                {
                    GameObject _boss = Instantiate(OBJ_BOSS);
                    _boss.GetComponent<EnemyMove>().SetRoad(_wave.LIST_UNIT_WAVE[0].m_TheRoad);
                }
            }
            #endregion

        }


    }




    //HIDE ALL ROAD-MARK
    private void HideAllRoadMark()
    {
        for (int i = 0; i < GROUP_STARTING_MARK.childCount; i++)//ADD STARTING MARK
        {
            LIST_STARTING_MARK[i].gameObject.SetActive(false);
        }
    }
    #endregion


    //EVENT================================
    private void HandleTouchWaveMark(TheStartingWaveMark _wavemark)
    {
        if (!_wavemark.IsCountComplete)
        {
            //free coin
            iOriginalCoin += 30;
            Instantiate(TheObjPoolingManager.Instance.objCoinGift, _wavemark.transform.position, Quaternion.identity);
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);//sound
            TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
        }

        HideAllRoadMark();
        Invoke("StarNewWave", 0.5f);
    }


    private void HandleEnemyCompleted(Enemy _enemy)
    {
        //attack home
        iCurrentHeart -= _enemy.DATA.iHeart;
        if (iCurrentHeart <= 0)
        {
            iCurrentHeart = 0;
            TheGameStatusManager.SetGameStatus(TheGameStatusManager.GAME_STATUS.Gameover);//game over
        }

        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event

        iTotalNumberEnemyOfWave--;
        CheckNextWave();
    }

    private void HandleEnemyWasDetroyed(Enemy _enemy)
    {
        //get coin from enemy
        iOriginalCoin += _enemy.DATA.iConfigCoin;

        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event

        iTotalNumberEnemyOfWave--;
        CheckNextWave();
    }



    private void OnEnable()
    {
        TheEventManager.OnTouchWaveMark += HandleTouchWaveMark;
        TheEventManager.OnEnemyIsDetroyOnRoad += HandleEnemyWasDetroyed;
        TheEventManager.OnEnemyCompletedRoad += HandleEnemyCompleted;
    }



    private void OnDisable()
    {
        Instance = null;
        StopAllCoroutines();

        TheEventManager.OnTouchWaveMark -= HandleTouchWaveMark;
        TheEventManager.OnEnemyIsDetroyOnRoad -= HandleEnemyWasDetroyed;
        TheEventManager.OnEnemyCompletedRoad -= HandleEnemyCompleted;

    }




    //STARTING MARK ----------------------------------------------
    private TheStartingWaveMark FindNeasestWaveMark(Vector2 _fromPos)
    {
        float _dis = 5;
        int _total = LIST_STARTING_MARK.Count;
        TheStartingWaveMark _target = new TheStartingWaveMark();

        for (int i = 0; i < _total; i++)
        {
            if (Vector2.Distance(_fromPos, LIST_STARTING_MARK[i].transform.position) < _dis)
            {
                _dis = Vector2.Distance(_fromPos, LIST_STARTING_MARK[i].transform.position);
                _target = LIST_STARTING_MARK[i];
            }
        }
        return _target;
    }

}

