using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThePlatformManager : MonoBehaviour
{
    public static ThePlatformManager Instance;
    [SerializeField] private MyGeneric_Platform INFO_MANAGER;





    [Space(20)]
    private InfoGame thisGameInfo;
    public InfoGame GAME_INFO
    {
        get
        {
            return thisGameInfo;
        }
    }





    public enum MODE
    {
        Testting,
        Release,
    }

    public MODE CHOOSING_MODE;



    public int TOTAL_LEVEL_IN_GAME;



    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

#if UNITY_ANDROID || UNITY_EDITOR
        thisGameInfo = INFO_MANAGER.Get(TheEnumManager.PLATFORM.android);

#elif UNITY_IOS || UNITY_IPHONE
        thisGameInfo = INFO_MANAGER.Get(TheEnumManager.PLATFORM.ios);

#elif UNITY_WEBGL
        thisGameInfo = INFO_MANAGER.Get(TheEnumManager.PLATFORM.webgl);
#else
        thisGameInfo = INFO_MANAGER.Get(TheEnumManager.PLATFORM.standalone);
#endif
    }


    private void Start()
    {
        Invoke("Init", 0.2f);
        CountTotalLevel();
    }


    private void Init()
    {
        switch (CHOOSING_MODE)
        {
            case MODE.Testting:
                TheDataManager.THE_PLAYER_DATA.TESTING_MODE = true;
                break;
            case MODE.Release:
                TheDataManager.THE_PLAYER_DATA.TESTING_MODE = false;
                break;

        }
    }




    private void CountTotalLevel()
    {
        for (int i = 1; i < 1000; i++)
        {
            if (Resources.Load<GameObject>("Levels/LEVEL_" + i))
            {
                TOTAL_LEVEL_IN_GAME = i;
            }
            else
                break;
        }
    }










}
