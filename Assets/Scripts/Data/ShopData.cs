using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopData
{
    public string strId;
   // public TheEnumManager.KIND_OF_PRICE eKindOfShop;
    public string strPackName;
    public int iGemValueToAdd;
    public int iCoinValueToAdd;
    public int iValueToAdd;
    public int iPriceGem;
    public float fPriceDollar;
    public string strIdKeyStore;
    public string strContent;



    public void BuySkill()
    {
        TheEnumManager.POWER_UP _skill = TheEnumManager.ConverStringToEnum_Skill(strId);
        TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(_skill,
        TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(_skill) + iValueToAdd);
       
    }


    public void BuyGem()
    {
        TheDataManager.THE_PLAYER_DATA.GEM += iGemValueToAdd;
    }

    public void BuyCoin()
    {
        TheLevel.Instance.iOriginalCoin += iCoinValueToAdd;
    }

}
