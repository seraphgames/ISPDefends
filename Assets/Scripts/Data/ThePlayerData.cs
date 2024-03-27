using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ThePlayerData 
{


    //DIFFICUFT
    public TheEnumManager.DIFFICUFT GAME_DIFFICUFT;

    //TESING MODE
    public bool TESTING_MODE;




    #region UNLOCK TOWER
    public bool UnlockTower_RocketLaucher = false;
    public bool UnlockTower_Poison = false;
    public bool UnlockTower_Thunder = false;
    public bool UnlockTower_Archer = false;
    public bool UnlockTower_Mage = false;
    public bool UnlockTower_Cannonner = false;
    public bool UnlockTower_Gunner = false;

    public bool CheckUnlockTower(TheEnumManager.TOWER _tower)
    {
        switch (_tower)
        {
            case TheEnumManager.TOWER.tower_magic:
                return UnlockTower_Mage;
            case TheEnumManager.TOWER.tower_archer:
                return UnlockTower_Archer;
            case TheEnumManager.TOWER.tower_cannonner:
                return UnlockTower_Cannonner;
            case TheEnumManager.TOWER.tower_gunmen:
                return UnlockTower_Gunner;
            case TheEnumManager.TOWER.tower_poison:
                return UnlockTower_Poison;
            case TheEnumManager.TOWER.tower_rocket_laucher:
                return UnlockTower_RocketLaucher;
            case TheEnumManager.TOWER.tower_thunder:
                return UnlockTower_Thunder;
        }
        return false;
    }
    public void SetUnlockTower(TheEnumManager.TOWER _tower, bool _unlock)
    {
        switch (_tower)
        {
            case TheEnumManager.TOWER.tower_magic:
                UnlockTower_Mage = _unlock;
                break;
            case TheEnumManager.TOWER.tower_archer:
                UnlockTower_Archer = _unlock;
                break;
            case TheEnumManager.TOWER.tower_cannonner:
                UnlockTower_Cannonner = _unlock;
                break;
            case TheEnumManager.TOWER.tower_gunmen:
                UnlockTower_Gunner = _unlock;
                break;
            case TheEnumManager.TOWER.tower_poison:
                UnlockTower_Poison = _unlock;
                break;
            case TheEnumManager.TOWER.tower_rocket_laucher:
                UnlockTower_RocketLaucher = _unlock;
                break;
            case TheEnumManager.TOWER.tower_thunder:
                UnlockTower_Thunder = _unlock;
                break;
        }

    }


    #endregion




    #region PLAYER DATA [Default]
    private int iPlayerGem = 80;
    public int GEM
    {
        get
        {
            return iPlayerGem;
        }
        set
        {
            iPlayerGem = value;
            TheDataManager.Instance.SerialzerPlayerData();//save
        }
    }


    public int iCurrentLevel = 0; //dem tu 0


    #endregion



    #region LEVEL STAR
    public List<int> LIST_STAR_NORMAL = new List<int>();
    public List<int> LIST_STAR_HARD = new List<int>();
    public List<int> LIST_STAR_NIGHTMATE = new List<int>();
    public int GetCurrentLevelOfPlayer()
    {
        return LIST_STAR_NORMAL.Count;
    }
    public int GetTotalStar(TheEnumManager.DIFFICUFT _difficuft)
    {
        List<int> _list = new List<int>();
        switch (_difficuft)
        {
            case TheEnumManager.DIFFICUFT.Normal:
                _list = LIST_STAR_NORMAL;
                break;
            case TheEnumManager.DIFFICUFT.Hard:
                _list = LIST_STAR_HARD;
                break;
            case TheEnumManager.DIFFICUFT.Nightmate:
                _list = LIST_STAR_NIGHTMATE;
                break;
        }


        int _total = 0;
        int length = _list.Count;
        for (int i = 0; i < length; i++)
        {
            _total += _list[i];
        }
        return _total;
    }
    public void SetStar(int _indexLevel, int _star, TheEnumManager.DIFFICUFT _difficuft) //_indexLevel tinh tu 0
    {

        switch (_difficuft)
        {
            //NORMAL
            case TheEnumManager.DIFFICUFT.Normal:
                if (_indexLevel < LIST_STAR_NORMAL.Count)
                {
                    if (LIST_STAR_NORMAL[_indexLevel] < _star)
                        LIST_STAR_NORMAL[_indexLevel] = _star;
                }
                else
                {
                    LIST_STAR_NORMAL.Add(_star);
                    if (LIST_STAR_NORMAL.Count > LIST_STAR_HARD.Count)
                        LIST_STAR_HARD.Add(0);
                    if (LIST_STAR_NORMAL.Count > LIST_STAR_NIGHTMATE.Count)
                        LIST_STAR_NIGHTMATE.Add(0);
                }
                break;

            //HARD
            case TheEnumManager.DIFFICUFT.Hard:
                if (_indexLevel < LIST_STAR_HARD.Count)
                {
                    if (LIST_STAR_HARD[_indexLevel] < _star)
                        LIST_STAR_HARD[_indexLevel] = _star;
                }


                break;

            //NIGHTMARE
            case TheEnumManager.DIFFICUFT.Nightmate:
                if (_indexLevel < LIST_STAR_NIGHTMATE.Count)
                {
                    if (LIST_STAR_NIGHTMATE[_indexLevel] < _star)
                        LIST_STAR_NIGHTMATE[_indexLevel] = _star;
                }

                break;

        }


    }
    public int GetStar(int _indexoflevle, TheEnumManager.DIFFICUFT _difficuft)
    {

        switch (_difficuft)
        {
            case TheEnumManager.DIFFICUFT.Normal:
                if (_indexoflevle < LIST_STAR_NORMAL.Count)
                    return LIST_STAR_NORMAL[_indexoflevle];
                else
                    return -1;

            case TheEnumManager.DIFFICUFT.Hard:
                if (_indexoflevle < LIST_STAR_HARD.Count)
                    return LIST_STAR_HARD[_indexoflevle];
                else
                    return -1;

            case TheEnumManager.DIFFICUFT.Nightmate:
                if (_indexoflevle < LIST_STAR_NIGHTMATE.Count)
                    return LIST_STAR_NIGHTMATE[_indexoflevle];
                else
                    return -1;


        }

        return -1;

    }
    #endregion



    #region NUMBER OF SKILL

    public int iSkillGuardian = 2;

    public int iSkillFreeze = 2;

    public int iSkillBoomFromSky = 2;

    public int iSkillMineOnRoad = 2;

    public int iSkillPoison = 2;


    public int GetNumberOfSkill(TheEnumManager.POWER_UP e)
    {
        switch (e)
        {
            case TheEnumManager.POWER_UP.guardian:
                return iSkillGuardian;
            case TheEnumManager.POWER_UP.freeze:
                return iSkillFreeze;
            case TheEnumManager.POWER_UP.fire_of_lord:
                return iSkillBoomFromSky;
            case TheEnumManager.POWER_UP.mine_on_road:
                return iSkillMineOnRoad;
            case TheEnumManager.POWER_UP.poison:
                return iSkillPoison;
        }
        return -1;
    }
    public void SetNumberOfSkill(TheEnumManager.POWER_UP e, int _number)
    {
        switch (e)
        {
            case TheEnumManager.POWER_UP.guardian:
                iSkillGuardian = _number;
                break;
            case TheEnumManager.POWER_UP.freeze:
                iSkillFreeze = _number;
                break;
            case TheEnumManager.POWER_UP.fire_of_lord:
                iSkillBoomFromSky = _number;
                break;
            case TheEnumManager.POWER_UP.mine_on_road:
                iSkillMineOnRoad = _number;
                break;
            case TheEnumManager.POWER_UP.poison:
                iSkillPoison = _number;
                break;
        }
        TheDataManager.Instance.SerialzerPlayerData();//save
    }

    #endregion




    #region UPGRADE SYSTEM
    public List<string> LIST_ID_OF_UPGRADE_SYSTEM = new List<string>();
    public List<bool> LIST_VALUE_OF_UPGRADE_SYSTEM = new List<bool>();

    public void SetActiveOfUpgradeSystem(string _id, bool _value)
    {
        int _total = LIST_ID_OF_UPGRADE_SYSTEM.Count;
        if (_total == 0)
        {
            LIST_ID_OF_UPGRADE_SYSTEM.Add(_id);
            LIST_VALUE_OF_UPGRADE_SYSTEM.Add(_value);
        }
        else
        {
            for (int i = 0; i < _total; i++)
            {
                if (LIST_ID_OF_UPGRADE_SYSTEM[i] == _id)
                {
                    LIST_VALUE_OF_UPGRADE_SYSTEM[i] = _value;
                    return;
                }
            }
            LIST_ID_OF_UPGRADE_SYSTEM.Add(_id);
            LIST_VALUE_OF_UPGRADE_SYSTEM.Add(_value);

        }
    }

    public bool GetActiveOfUpgradeSystem(string _id)
    {
        int _total = LIST_ID_OF_UPGRADE_SYSTEM.Count;
        if (_total == 0) return false;
        else
        {
            for (int i = 0; i < _total; i++)
            {
                if (LIST_ID_OF_UPGRADE_SYSTEM[i] == _id)
                {
                    return LIST_VALUE_OF_UPGRADE_SYSTEM[i];

                }
            }
            return false;
        }

    }
    #endregion



    #region NUMBER OF GIFTS RECEIVED & CURRENT TIME
    public int iNumberOfGiftsReceived;
    public int iCurrentYear;
    public int iCurrentDayOfYear;
    #endregion



    #region DATA ANALYTICS
    public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_MODE_NORMAL;
    public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_MODE_HARD;
    public TheEnumManager.LEVEL_DIFFICIFT CURRENT_LEVEL_DIFFICUFT_MODE_NIGHTMATE;



    public int iNumberOfTimePlayed_ModeNormal; // tong so lan choi trong mode normal (-----------> NEED SAVE)
    public int iNumberOfTimePlayed_ModeHard; // tong so lan choi trong mode hard (-----------> NEED SAVE)
    public int iNumberOfTimePlayed_ModeNightmate; // tong so lan choi trong mode nightmate (-----------> NEED SAVE)

    public int iNumberOfWinning_1star_ModeNormal; //(-----------> NEED SAVE)
    public int iNumberOfWinning_2star_ModeNormal; //(-----------> NEED SAVE)
    public int iNumberOfWinning_3star_ModeNormal;//(-----------> NEED SAVE)


    public int iNumberOfWinning_1star_ModeHard;//(-----------> NEED SAVE)
    public int iNumberOfWinning_2star_ModeHard;//(-----------> NEED SAVE)
    public int iNumberOfWinning_3star_ModeHard;//(-----------> NEED SAVE)


    public int iNumberOfWinning_1star_ModeNightmate;//(-----------> NEED SAVE)
    public int iNumberOfWinning_2star_ModeNightmate;//(-----------> NEED SAVE)
    public int iNumberOfWinning_3star_ModeNightmate;//(-----------> NEED SAVE)


    public int iNumberOfDefeat_ModeNormal; // Tong so lan that bai//(-----------> NEED SAVE)
    public int iNumberOfDefeat_ModeHard; // Tong so lan that bai//(-----------> NEED SAVE)
    public int iNumberOfDefeat_ModeNightmate; // Tong so lan that bai//(-----------> NEED SAVE)
    #endregion



    #region ACHIEVEMENT + LEADBOARD
    public List<string> LIST_ID_OF_ARCHIVEMENT = new List<string>();
    public List<bool> LIST_VALUE_OF_ARCHIVEMENT = new List<bool>();

    public void SetActiveOfArchivement(string _id, bool _value)
    {
        int _total = LIST_ID_OF_UPGRADE_SYSTEM.Count;
        if (_total == 0)
        {
            LIST_ID_OF_UPGRADE_SYSTEM.Add(_id);
            LIST_VALUE_OF_UPGRADE_SYSTEM.Add(_value);
        }
        else
        {
            for (int i = 0; i < _total; i++)
            {
                if (LIST_ID_OF_UPGRADE_SYSTEM[i] == _id)
                {
                    LIST_VALUE_OF_UPGRADE_SYSTEM[i] = _value;
                    return;
                }
            }
            LIST_ID_OF_UPGRADE_SYSTEM.Add(_id);
            LIST_VALUE_OF_UPGRADE_SYSTEM.Add(_value);

        }
    }

    public bool GetActiveOfArchivement(string _id)
    {
        int _total = LIST_ID_OF_UPGRADE_SYSTEM.Count;
        if (_total == 0) return false;
        else
        {
            for (int i = 0; i < _total; i++)
            {
                if (LIST_ID_OF_UPGRADE_SYSTEM[i] == _id)
                {
                    return LIST_VALUE_OF_UPGRADE_SYSTEM[i];

                }
            }
            return false;
        }

    }




    //HIGHSCORE
    public int iPlayerScore;
    public void AddPlayerScore(int _score)
    {
        iPlayerScore += _score;
    }

    #endregion
}
