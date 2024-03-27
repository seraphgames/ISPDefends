using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Coin pack")]
public class CoinpackData : SellingProduct
{
    [Space(20)]
    public TheEnumManager.COIN_PACK eCoin;

    [SerializeField] int _priceGem;
    public int fPriceGem { get { return _priceGem; } }


    public override void Get()
    {
        TheLevel.Instance.iOriginalCoin += iValueToAdd;
        TheEventManager.PostGameEvent_OnUpdateBoardInfo();
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);
    }
}
