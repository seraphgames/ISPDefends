using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Upgrade Data", menuName = "Upgrade Data")]
public class UpgradeConfig : ScriptableObject
{
    public TheEnumManager.UPGRADE eUpgrade;
    public TheEnumManager.FACTOR_TYPE eFactorType;

    public string strName;
    public string strContent;
    private bool bActived;
    public bool ACTIVED
    {
        get
        {
            return bActived;
        }
        set
        {
            bActived = value;
            TheDataManager.THE_PLAYER_DATA.SetActiveOfUpgradeSystem(eUpgrade.ToString(), bActived);
            TheDataManager.Instance.SerialzerPlayerData();
        }
    }


    [Header("****Config****")]
    [Space(30)]
    public int fValueConfig;
    public int fValueDefaul;
    public string strUnit;//met, sec ...

    public float GetValueConfig_Standard()
    {
        return fValueConfig;
    }
    public float GetResuftValueConfig_Percent(float _OriginalValue)
    {
        switch (eFactorType)
        {
            case TheEnumManager.FACTOR_TYPE.up:
                return _OriginalValue * (1 + fValueConfig * 1.0f / 100);

            case TheEnumManager.FACTOR_TYPE.down:
                return _OriginalValue * (1 - fValueConfig * 1.0f / 100);


        }
        return 0;
    }




    [Header("****Price Star****")]
    [Space(30)]
    public TheEnumManager.STAR_TYPE eStarType;
    public int iStarPrice;

    //INIT
    public void Init()
    {
        ACTIVED = TheDataManager.THE_PLAYER_DATA.GetActiveOfUpgradeSystem(eUpgrade.ToString());
    }

    //RESET
    public void Reset()
    {
        ACTIVED = false;
    }
}
