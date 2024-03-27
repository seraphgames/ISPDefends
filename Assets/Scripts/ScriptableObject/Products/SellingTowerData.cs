using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Selling tower")]
public class SellingTowerData : SellingProduct
{
    [Space(20)]
    public TheEnumManager.TOWER eTower;


    [SerializeField] float _priceCash;
    public float fPriceCash { get { return _priceCash; } }
    [SerializeField] int _priceGem;
    public int fPriceGem { get { return _priceGem; } }




    [Space(20)]
    [SerializeField] string _keyStoreID_Android;
    [SerializeField] string _keyStoreID_IOS;
    public string strKeyStoreId
    {
        get
        {
            if (ThePlatformManager.Instance.GAME_INFO.ePLATFORM == TheEnumManager.PLATFORM.android)
                return _keyStoreID_Android;
            if (ThePlatformManager.Instance.GAME_INFO.ePLATFORM == TheEnumManager.PLATFORM.ios)
                return _keyStoreID_IOS;

            return "";
        }
    }



    public override void Get()
    {
        TowerData _towerdata = TheDataManager.Instance.TOWER_DATA_MANAGER.Get(eTower);
        _towerdata.IsUnlocked = true;
        TheEventManager.PostGameEvent_OnUnlockTower();
    }


    public string GetInfo()
    {
        TowerData _tower = TheDataManager.Instance.TOWER_DATA_MANAGER.Get(eTower);
        string _content =
               _tower.strTowerName + "\n"
               + "______________________ \n"
               + _tower.strContent + "\n"
               + "* DAMAGE: " + _tower.GetDamage(TheEnumManager.TOWER_LEVEL.level_1) + " \n"
               + "* RELOAD: " + _tower.GetFireRate(TheEnumManager.TOWER_LEVEL.level_1) + "s \n"
               + "* RANGE: " + _tower.GetRange(TheEnumManager.TOWER_LEVEL.level_1) + "m \n"
               + "______________________________ \n"
           + "PRICE: " + _tower.iPriceToUnlock_gem + " GEMS \n"
           + "OR FREE FROM LEVEL " + _tower.iLevelToUnlock + ".";

        return _content;
    }
}
