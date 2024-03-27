using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheFreeGem : MonoBehaviour
{
    [SerializeField] Button buThis;
    [SerializeField] Text txtValue;

    // Start is called before the first frame update
    void Start()
    {
        //buThis.onClick.AddListener(() => WatchAds());
    }

    private void WatchAds()
    {
       // TheAdsManager.Instance.WatchRewardedVideo(TheAdsManager.REWARED_VIDEO.FreeGem);
    }

    private void OnEnable()
    {

       /* txtValue.text = "+" + TheDataManager.Instance.iGemFormWatchingAds;
        //----------------------------
        if (!TheAdsManager.Instance.isReadyRewardedVideoAd())
            gameObject.SetActive(false);*/
    }


}
