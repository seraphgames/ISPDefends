using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New gem pack")]
public class GempackData : SellingProduct
{
    [Space(20)]
    public TheEnumManager.GEM_PACK ePack;


    [SerializeField] float _price;
    public float fPrice { get { return _price; } }



    [Space(20)]
    [SerializeField] string _keyStoreID_Android;
    [SerializeField] string _keyStoreID_IOS;
    public string strKeyStoreId
    {
        get
        {
            if (ThePlatformManager.Instance.GAME_INFO.ePLATFORM == TheEnumManager.PLATFORM.android) return _keyStoreID_Android;
            if (ThePlatformManager.Instance.GAME_INFO.ePLATFORM == TheEnumManager.PLATFORM.ios) return _keyStoreID_IOS;
            return "";
        }
    }



    public override void Get()
    {
        TheDataManager.THE_PLAYER_DATA.GEM += iValueToAdd;
        TheEventManager.PostGameEvent_OnUpdateBoardInfo();
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);

    }
}
