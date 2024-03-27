using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Selling skill")]
public class SellingSkillData : SellingProduct
{
    [Space(20)]
    public TheEnumManager.POWER_UP eSkill;


    [SerializeField] float _priceGem;
    public float fPriceGem { get { return _priceGem; } }



    public override void Get()
    {
        int _value = TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(eSkill);
        TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(eSkill, _value + iValueToAdd);
    }
}
