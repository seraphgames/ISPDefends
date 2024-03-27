using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "TowerData")]
[System.Serializable]
public class TowerData : ScriptableObject
{
    public TheEnumManager.TOWER eTower;
    public Sprite sprIcon;
    public GameObject objPrefab;

    [Space(20)]
    public string strTowerName;
    public string strContent;

    [Header("*** CONFIG ***")]
    [Space(30)]
    public List<int> LIST_DAMAGE;
    public List<float> LIST_FIRE_RATE;
    public List<float> LIST_RANGE;
    public List<int> NUMBER_OF_BULLET;
    public int iBasePriceToBuild;

    public bool IsUnlocked
    {
        get
        {
            return TheDataManager.THE_PLAYER_DATA.CheckUnlockTower(eTower);
        }
        set
        {
            TheDataManager.THE_PLAYER_DATA.SetUnlockTower(eTower, value);
            TheDataManager.Instance.SerialzerPlayerData();//save
        }
    }

    //Get damage
    public int GetDamage(TheEnumManager.TOWER_LEVEL _level)
    {
        int _damage = LIST_DAMAGE[(int)_level];

        UpgradeConfig _upgrade = GetUpgradeConfig_Damage(); //upgrade
        if (_upgrade && _upgrade.ACTIVED) _damage = (int)_upgrade.GetResuftValueConfig_Percent(_damage * 1.0f);

        return _damage;
    }

    //Get fire rate
    public float GetFireRate(TheEnumManager.TOWER_LEVEL _level)
    {
        float _firerate = LIST_FIRE_RATE[(int)_level];
        UpgradeConfig _upgradeconfig = TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.Other_MoreAttackSpeedForAllTower);
        if (_upgradeconfig.ACTIVED)
            _firerate = _upgradeconfig.GetResuftValueConfig_Percent(_firerate);
        return _firerate;
    }

    //Get range
    public float GetRange(TheEnumManager.TOWER_LEVEL _level)
    {
        float _range = LIST_RANGE[(int)_level];

        UpgradeConfig _upgrade = GetUpgradeConfig_Range(); //upgrade
        if (_upgrade && _upgrade.ACTIVED) _range = _upgrade.GetResuftValueConfig_Percent(_range);

        return _range;
    }

    //Get number of bullet
    public int GetNumberOfBullet(TheEnumManager.TOWER_LEVEL _level)
    {
        return NUMBER_OF_BULLET[(int)_level];
    }



    [Space(30)]
    public bool bCanKillInfantry;
    public bool bCanKillAirforce;


    [Header("*** PRICE ***")]
    //Get price to build
    private int _unitPrice_Damage = 0;
    private int _unitPrice_Range = 0;
    private int _unitPrice_FireRate = 0;



    public int GetPriceToBuild(TheEnumManager.TOWER_LEVEL _level = TheEnumManager.TOWER_LEVEL.level_1)
    {

        int _priceOfDamage = LIST_DAMAGE[(int)_level] * NUMBER_OF_BULLET[(int)_level] * _unitPrice_Damage;
        int _priceOfRange = (int)(LIST_RANGE[(int)_level] * _unitPrice_Range * 1.0f);
        int _priceOfFireRate = (int)((1.0f / LIST_FIRE_RATE[(int)_level]) * _unitPrice_FireRate * 1.0f);

        // Debug.Log(eTower.ToString() + " DAM: " + _priceOfDamage);
        // Debug.Log(eTower.ToString() + "RANGE:" + _priceOfRange);
        //Debug.Log(eTower.ToString() + "FIRE RATE: " + _priceOfFireRate);


        int _price = _priceOfDamage + _priceOfRange + _priceOfFireRate;

        UpgradeConfig _upgrade = GetUpgradeConfig_PriceBuild(); //upgrade
        if (_upgrade && _upgrade.ACTIVED) _price = (int)_upgrade.GetResuftValueConfig_Percent(_price * 1.0f);

        _upgrade = TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.Other_ReducePriceTowerToBuildForAllTower); //upgrade
        if (_upgrade && _upgrade.ACTIVED) _price = (int)_upgrade.GetResuftValueConfig_Percent(_price * 1.0f);

        //====================================
        return _price + iBasePriceToBuild;
    }

    //Get price to build
    public int GetPriceToUpgrade(TheEnumManager.TOWER_LEVEL _level)
    {
        switch (_level)
        {
            case TheEnumManager.TOWER_LEVEL.level_1:
                return (int)(GetPriceToBuild(_level) * 0.9f);
            case TheEnumManager.TOWER_LEVEL.level_2:
                return (int)(GetPriceToBuild(_level) * 1.5f);
            case TheEnumManager.TOWER_LEVEL.level_3:
                return (int)(GetPriceToBuild(_level) * 2.0f);

        }
        return (int)(GetPriceToBuild(_level) * 1.2f);

    }

    //Get price to sell
    public int GetPriceToSell(TheEnumManager.TOWER_LEVEL _level)
    {
        return (int)(GetPriceToBuild(_level) * 0.45f);
        // return PRICE_TO_SELL[(int)_level];
    }



    public bool bUNLOCK
    {
        get
        {
            return TheDataManager.THE_PLAYER_DATA.CheckUnlockTower(eTower);
        }
        set
        {
            if (TheDataManager.THE_PLAYER_DATA.CheckUnlockTower(eTower) == value) return;

            TheDataManager.THE_PLAYER_DATA.SetUnlockTower(eTower, value);
            TheDataManager.Instance.SerialzerPlayerData();//save
            TheEventManager.PostGameEvent_OnUpdateBoardInfo();
        }
    }

    public int iLevelToUnlock;
    public int iPriceToUnlock_gem;
    public float iPriceToUnlock_dollar;


    [Header("*** FOR SOLDIER ***")]
    [Space(30)]
    [SerializeField]
    private int iHp;
    public int GetHp()
    {
        int _hp = iHp;

        UpgradeConfig _upgrad = TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.Reinforcement_MoreHp);
        if (_upgrad.ACTIVED)
        {
            _hp = (int)_upgrad.GetResuftValueConfig_Percent(_hp * 1.0f);
        }

        return _hp;

    }
    public float fSpeedMove;





    //UPGRADE CONFIG
    private UpgradeConfig GetUpgradeConfig_Damage()
    {
        switch (eTower)
        {
            case TheEnumManager.TOWER.tower_magic:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerMagic_MoreDamage);
            case TheEnumManager.TOWER.tower_archer:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerArcher_MoreDamage);
            case TheEnumManager.TOWER.tower_cannonner:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerCannonner_MoreDamage);
            case TheEnumManager.TOWER.tower_gunmen:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerGunmen_MoreDamage);
            case TheEnumManager.TOWER.tower_poison:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerPoison_MoreDamage);
            case TheEnumManager.TOWER.tower_rocket_laucher:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerRocketLaucher_MoreDamage);
            case TheEnumManager.TOWER.tower_thunder:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerThunder_MoreDamage);
        }
        return null;
    }
    private UpgradeConfig GetUpgradeConfig_Range()
    {
        switch (eTower)
        {
            case TheEnumManager.TOWER.tower_magic:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerMagic_MoreRange);
            case TheEnumManager.TOWER.tower_archer:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerArcher_MoreRange);
            case TheEnumManager.TOWER.tower_cannonner:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerCannonner_MoreRange);
            case TheEnumManager.TOWER.tower_gunmen:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerGunmen_MoreRange);
            case TheEnumManager.TOWER.tower_poison:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerPoison_MoreRange);
            case TheEnumManager.TOWER.tower_rocket_laucher:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerRocketLaucher_MoreRange);
            case TheEnumManager.TOWER.tower_thunder:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerThunder_MoreRange);
        }
        return null;
    }
    private UpgradeConfig GetUpgradeConfig_PriceBuild()
    {
        switch (eTower)
        {
            case TheEnumManager.TOWER.tower_magic:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerMagic_ReducePriceToBuild);
            case TheEnumManager.TOWER.tower_archer:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerArcher_ReducePriceToBuild);
            case TheEnumManager.TOWER.tower_cannonner:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerCannonner_ReducePriceToBuild);
            case TheEnumManager.TOWER.tower_gunmen:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerGunmen_ReducePriceToBuild);
            case TheEnumManager.TOWER.tower_poison:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerPoison_ReducePriceToBuild);
            case TheEnumManager.TOWER.tower_rocket_laucher:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerRocketLaucher_ReducePriceToBuild);
            case TheEnumManager.TOWER.tower_thunder:
                return TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.TowerThunder_ReducePriceToBuild);
        }
        return null;
    }

    //CHECK UNLOCK TOWER WITH LEVEL
    public void SetUnlockWithLevel(int _level)
    {
        if (bUNLOCK) return;

        if (_level >= iLevelToUnlock)
            bUNLOCK = true;
    }

    //INIT - LOAD DATA FROM STOTAGE
    public void Init()
    {
        bUNLOCK = TheDataManager.THE_PLAYER_DATA.CheckUnlockTower(eTower);
        SetUnlockWithLevel(TheDataManager.THE_PLAYER_DATA.GetCurrentLevelOfPlayer());

        //config unit price
        _unitPrice_Damage = 1;
        _unitPrice_Range = 15;
        _unitPrice_FireRate = 20;
    }

    public void Reset()
    {
        bUNLOCK = false;
    }
}
