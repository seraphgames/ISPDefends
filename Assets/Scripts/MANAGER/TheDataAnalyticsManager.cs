using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TheDataAnalyticsManager : MonoBehaviour
{
    public static TheDataAnalyticsManager Instance;
    public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_NORMAL;
    public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_HARD;
    public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_NIGHTMATE;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }


    // Use this for initialization
    void Start()
    {
        
        Invoke("Init", 2.0f);
    }

    private void Init()
    {
        Calculation();
    }



    #region AUTO BALLANCE ----  TIMES AND RATIO PLAYED
    [Space(20)]
    [Header("TIMES PLAYED---------------------")]
    //TIMES PLAYED
    private int iNumberOfTimePlayed_Total; // Tong so lan choi
    [Space(20)]
    [Header("TIMES PLAYED---------------------")]
    public int iNumberOfTimePlayed_ModeNormal; // tong so lan choi trong mode normal (-----------> NEED SAVE)
    public int iNumberOfTimePlayed_ModeHard; // tong so lan choi trong mode hard (-----------> NEED SAVE)
    public int iNumberOfTimePlayed_ModeNightmate; // tong so lan choi trong mode nightmate (-----------> NEED SAVE)

    [Space(20)]
    [Header("TIMES OF WINNING---------------------")]
    //VICTORY
    private int iNumberOfWin_Total; // Tong so lan thang
    private int iNumberOfWin_ModeNormal; // Tong so lan thang
    private int iNumberOfWin_ModeHard; // Tong so lan thang
    private int iNumberOfWin_ModeNightmate; // Tong so lan thang
    [Space(20)]
    [Header("TIMES WINNING AND DEFEAT---------------------")]
    public int iNumberOfWinning_1star_ModeNormal; //(-----------> NEED SAVE)
    public int iNumberOfWinning_2star_ModeNormal; //(-----------> NEED SAVE)
    public int iNumberOfWinning_3star_ModeNormal;//(-----------> NEED SAVE)

    [Space(10)]
    public int iNumberOfWinning_1star_ModeHard;//(-----------> NEED SAVE)
    public int iNumberOfWinning_2star_ModeHard;//(-----------> NEED SAVE)
    public int iNumberOfWinning_3star_ModeHard;//(-----------> NEED SAVE)

    [Space(10)]
    public int iNumberOfWinning_1star_ModeNightmate;//(-----------> NEED SAVE)
    public int iNumberOfWinning_2star_ModeNightmate;//(-----------> NEED SAVE)
    public int iNumberOfWinning_3star_ModeNightmate;//(-----------> NEED SAVE)

    [Space(20)]
    [Header("TIMES OF DEFEAT---------------------")]
    //GAME OVER
    private int iNumberOfDefeat_Total; // Tong so lan that bai//(-----------> NEED SAVE)
    public int iNumberOfDefeat_ModeNormal; // Tong so lan that bai//(-----------> NEED SAVE)
    public int iNumberOfDefeat_ModeHard; // Tong so lan that bai//(-----------> NEED SAVE)
    public int iNumberOfDefeat_ModeNightmate; // Tong so lan that bai//(-----------> NEED SAVE)


    [Space(20)]
    [Header("RATION OF WINNINER---------------------")]
    //RATIO VICTORY / TY LE CHIEN THANG
    public float fRatioOfVictory_Total; // tong so ty le chien thang
    public float fRatioOfVictory_ModeNormal;
    public float fRatioOfVictory_ModeHard;
    public float fRatioOfVictory_ModeNightmate;
    [Space(20)]
    public float fRatioOfVictory_1star_ModeNormal;// ty le thang 1 sao - mode NORMAL
    public float fRatioOfVictory_2star_ModeNormal;// ty le thang 1 sao - mode NORMAL
    public float fRatioOfVictory_3star_ModeNormal;// ty le thang 1 sao - mode NORMAL
    [Space(20)]
    public float fRatioOfVictory_1star_ModeHard;// ty le thang 1 sao - mode HARD
    public float fRatioOfVictory_2star_ModeHard;// ty le thang 1 sao - mode HARD
    public float fRatioOfVictory_3star_ModeHard;// ty le thang 1 sao - mode HARD
    [Space(20)]
    public float fRatioOfVictory_1star_ModeNightmate;// ty le thang 1 sao - mode NIGHTMATE
    public float fRatioOfVictory_2star_ModeNightmate;// ty le thang 1 sao - mode NIGHTMATE
    public float fRatioOfVictory_3star_ModeNightmate;// ty le thang 1 sao - mode NIGHTMATE



    [Space(20)]
    [Header("RATION OF DEFEAT---------------------")]
    //RATIO DEFEAT / TY LE THAT BAI
    public float fRatioOfDefeat_Total;
    public float fRatioOfDefeat_ModeNormal;
    public float fRatioOfDefeat_ModeHard;
    public float fRatioOfDefeat_ModeNightmate;


    private void GetDataFromStorage()
    {
        iNumberOfTimePlayed_ModeNormal = TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNormal;
        iNumberOfTimePlayed_ModeHard = TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeHard;
        iNumberOfTimePlayed_ModeNightmate = TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNightmate;


        iNumberOfWinning_1star_ModeNormal = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeNormal;
        iNumberOfWinning_2star_ModeNormal = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeNormal;
        iNumberOfWinning_3star_ModeNormal = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeNormal;


        iNumberOfWinning_1star_ModeHard = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeHard;
        iNumberOfWinning_2star_ModeHard = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeHard;
        iNumberOfWinning_3star_ModeHard = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeHard;

        iNumberOfWinning_1star_ModeNightmate = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeNightmate;
        iNumberOfWinning_2star_ModeNightmate = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeNightmate;
        iNumberOfWinning_3star_ModeNightmate = TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeNightmate;


        CURRENT_LEVEL_DIFFICUFT_NORMAL = TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NORMAL;
        CURRENT_LEVEL_DIFFICUFT_HARD = TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_HARD;
        CURRENT_LEVEL_DIFFICUFT_NIGHTMATE = TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NIGHTMATE;
    }

    public void Calculation()
    {
        GetDataFromStorage(); //Get data from storage


        iNumberOfWin_ModeNormal = iNumberOfWinning_1star_ModeNormal + iNumberOfWinning_2star_ModeNormal + iNumberOfWinning_3star_ModeNormal;
        iNumberOfWin_ModeHard = iNumberOfWinning_1star_ModeHard + iNumberOfWinning_2star_ModeHard + iNumberOfWinning_3star_ModeHard;
        iNumberOfWin_ModeNightmate = iNumberOfWinning_1star_ModeNightmate + iNumberOfWinning_2star_ModeNightmate + iNumberOfWinning_3star_ModeNightmate;


        iNumberOfTimePlayed_Total = iNumberOfTimePlayed_ModeNormal + iNumberOfTimePlayed_ModeHard + iNumberOfTimePlayed_ModeNightmate; // Tong so lan choi
        iNumberOfWin_Total = iNumberOfWin_ModeNormal + iNumberOfWin_ModeHard + iNumberOfWin_ModeNightmate; // tong so lan chien thang ---- VICTORY
        iNumberOfDefeat_Total = iNumberOfDefeat_ModeNormal + iNumberOfDefeat_ModeHard + iNumberOfDefeat_ModeNightmate; // tong so lan that bai --- GAME OVER


        fRatioOfVictory_1star_ModeNormal = iNumberOfWinning_1star_ModeNormal * 1.0f / iNumberOfTimePlayed_Total; // ty le thang 1 sao - NORMAL
        fRatioOfVictory_2star_ModeNormal = iNumberOfWinning_2star_ModeNormal * 1.0f / iNumberOfTimePlayed_Total; // ty le thang 2 sao - NORMAL
        fRatioOfVictory_3star_ModeNormal = iNumberOfWinning_3star_ModeNormal * 1.0f / iNumberOfTimePlayed_Total;// ty le thang 3 sao - NORMAL

        fRatioOfVictory_1star_ModeHard = iNumberOfWinning_1star_ModeHard * 1.0f / iNumberOfTimePlayed_Total; // ty le thang 1 sao - HARD
        fRatioOfVictory_2star_ModeHard = iNumberOfWinning_2star_ModeHard * 1.0f / iNumberOfTimePlayed_Total; // ty le thang 2 sao - HARD
        fRatioOfVictory_3star_ModeHard = iNumberOfWinning_3star_ModeHard * 1.0f / iNumberOfTimePlayed_Total;// ty le thang 3 sao - HARD

        fRatioOfVictory_1star_ModeNightmate = iNumberOfWinning_1star_ModeNightmate * 1.0f / iNumberOfTimePlayed_Total; // ty le thang 1 sao - NIGHTMATE
        fRatioOfVictory_2star_ModeNightmate = iNumberOfWinning_2star_ModeNightmate * 1.0f / iNumberOfTimePlayed_Total; // ty le thang 2 sao - NIGHTMATE
        fRatioOfVictory_3star_ModeNightmate = iNumberOfWinning_3star_ModeNightmate * 1.0f / iNumberOfTimePlayed_Total;// ty le thang 3 sao - NIGHTMATE


        fRatioOfVictory_Total = iNumberOfWin_Total * 1.0f / iNumberOfTimePlayed_Total; //ty le thang
        fRatioOfVictory_ModeNormal = iNumberOfWin_ModeNormal * 1.0f / iNumberOfWin_ModeNormal; // ty le thang -- NORMAL
        fRatioOfVictory_ModeHard = iNumberOfWin_ModeHard * 1.0f / iNumberOfWin_ModeHard;// ty le thang -- HARD
        fRatioOfVictory_ModeNightmate = iNumberOfWin_ModeNightmate * 1.0f / iNumberOfWin_ModeNightmate;// ty le thang -- NIGHTMATE


        fRatioOfDefeat_Total = iNumberOfDefeat_Total * 1.0f / iNumberOfTimePlayed_Total; // ty le thua
        fRatioOfDefeat_ModeNormal = iNumberOfDefeat_ModeNormal * 1.0f / iNumberOfWin_ModeNormal;// ty le thua--NORMAL
        fRatioOfDefeat_ModeHard = iNumberOfDefeat_ModeHard * 1.0f / iNumberOfWin_ModeHard;// ty le thua--HARD
        fRatioOfDefeat_ModeNightmate = iNumberOfDefeat_ModeNightmate * 1.0f / iNumberOfWin_ModeNightmate;// ty le thua--NIGHTMATE


        AutoBalance();
    }



    private void AutoBalance() // <<<======================== CÂN BẰNG TỰ ĐỘNG
    {
        //if (TheDataManager.THE_PLAYER_DATA.GetCurrentLevelOfPlayer() < 3) return;

        //NORMAL------------------------------
        if (fRatioOfVictory_ModeNormal > 0.5f)
        {
            if (CURRENT_LEVEL_DIFFICUFT_NORMAL != TheEnumManager.LEVEL_DIFFICIFT.Level_10)
                CURRENT_LEVEL_DIFFICUFT_NORMAL++;
        }
        else if (fRatioOfVictory_ModeNormal < 0.25f)
        {
            if (CURRENT_LEVEL_DIFFICUFT_NORMAL != TheEnumManager.LEVEL_DIFFICIFT.Level_1)
                CURRENT_LEVEL_DIFFICUFT_NORMAL--;
        }


        //HARD------------------------------
        if (fRatioOfVictory_ModeHard > 0.4f)
        {
            if (CURRENT_LEVEL_DIFFICUFT_HARD != TheEnumManager.LEVEL_DIFFICIFT.Level_10)
                CURRENT_LEVEL_DIFFICUFT_HARD++;
        }
        else if (fRatioOfVictory_ModeHard < 0.2f)
        {
            if (CURRENT_LEVEL_DIFFICUFT_HARD != TheEnumManager.LEVEL_DIFFICIFT.Level_1)
                CURRENT_LEVEL_DIFFICUFT_HARD--;
        }


        //NIGHTMATE------------------------------
        if (fRatioOfVictory_ModeNightmate > 0.3f)
        {
            if (CURRENT_LEVEL_DIFFICUFT_NIGHTMATE != TheEnumManager.LEVEL_DIFFICIFT.Level_10)
                CURRENT_LEVEL_DIFFICUFT_NIGHTMATE++;
        }
        else if (fRatioOfVictory_ModeNightmate < 0.2f)
        {
            if (CURRENT_LEVEL_DIFFICUFT_NIGHTMATE != TheEnumManager.LEVEL_DIFFICIFT.Level_1)
                CURRENT_LEVEL_DIFFICUFT_NIGHTMATE--;
        }

        TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NORMAL = CURRENT_LEVEL_DIFFICUFT_NORMAL;
        TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_HARD = CURRENT_LEVEL_DIFFICUFT_HARD;
        TheDataManager.THE_PLAYER_DATA.CURRENT_LEVEL_DIFFICUFT_MODE_NIGHTMATE = CURRENT_LEVEL_DIFFICUFT_NIGHTMATE;
        TheDataManager.Instance.SerialzerPlayerData();//save
    }



    //EVENT
    private void GameWinning(int _star)
    {
        switch (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT)
        {
            case TheEnumManager.DIFFICUFT.Normal:
                TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNormal++;
                break;
            case TheEnumManager.DIFFICUFT.Hard:
                TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeHard++;
                break;
            case TheEnumManager.DIFFICUFT.Nightmate:
                TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNightmate++;
                break;
        }
        switch (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT)
        {
            case TheEnumManager.DIFFICUFT.Normal:
                if (_star == 1) TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeNormal++;
                else if (_star == 2) TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeNormal++;
                else if (_star == 3) TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeNormal++;
                break;
            case TheEnumManager.DIFFICUFT.Hard:
                if (_star == 1) TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeHard++;
                else if (_star == 2) TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeHard++;
                else if (_star == 3) TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeHard++;
                break;
            case TheEnumManager.DIFFICUFT.Nightmate:
                if (_star == 1) TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_1star_ModeNightmate++;
                else if (_star == 2) TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_2star_ModeNightmate++;
                else if (_star == 3) TheDataManager.THE_PLAYER_DATA.iNumberOfWinning_3star_ModeNightmate++;
                break;

        }


        TheDataManager.Instance.SerialzerPlayerData();//save
        Calculation();
    }
    private void GameDefeat(int _star)
    {
        switch (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT)
        {
            case TheEnumManager.DIFFICUFT.Normal:
                TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNormal++;
                break;
            case TheEnumManager.DIFFICUFT.Hard:
                TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeHard++;
                break;
            case TheEnumManager.DIFFICUFT.Nightmate:
                TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNightmate++;
                break;
        }

        switch (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT)
        {
            case TheEnumManager.DIFFICUFT.Normal:
                TheDataManager.THE_PLAYER_DATA.iNumberOfDefeat_ModeNormal++;
                break;
            case TheEnumManager.DIFFICUFT.Hard:
                TheDataManager.THE_PLAYER_DATA.iNumberOfDefeat_ModeHard++;
                break;
            case TheEnumManager.DIFFICUFT.Nightmate:
                TheDataManager.THE_PLAYER_DATA.iNumberOfDefeat_ModeNightmate++;
                break;

        }
        TheDataManager.Instance.SerialzerPlayerData();//save
        Calculation();
    }

    private void GameStart(int _star)
    {
        //switch (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT)
        //{
        //    case TheEnumManager.DIFFICUFT.Normal:
        //        TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNormal++;
        //        break;
        //    case TheEnumManager.DIFFICUFT.Hard:
        //        TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeHard++;
        //        break;
        //    case TheEnumManager.DIFFICUFT.Nightmate:
        //        TheDataManager.THE_PLAYER_DATA.iNumberOfTimePlayed_ModeNightmate++;
        //        break;

        //}
        //TheDataManager.Instance.SerialzerPlayerData();//save
        //Calculation();
    }

    #endregion











    private void OnEnable()
    {
        TheEventManager.OnGameWinning += GameWinning;
        TheEventManager.OnGameDefeat += GameDefeat;
        TheEventManager.OnGameStart += GameStart;
    }

    private void OnDisable()
    {
        TheEventManager.OnGameWinning -= GameWinning;
        TheEventManager.OnGameDefeat -= GameDefeat;
        TheEventManager.OnGameStart -= GameStart;
    }
}
