using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedVideo : PopUp
{

    public Button buWatchAds_track1, buWatchAds_track2, buWatchAds_track3;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        buWatchAds_track1.onClick.AddListener(() => GetGift(TheAdsManager.REWARED_VIDEO.GetGift_Pack1));
        buWatchAds_track2.onClick.AddListener(() => GetGift(TheAdsManager.REWARED_VIDEO.GetGift_Pack2));
        buWatchAds_track3.onClick.AddListener(() => GetGift(TheAdsManager.REWARED_VIDEO.GetGift_Pack3));
    }


    private void GetGift(TheAdsManager.REWARED_VIDEO eRewared)
    {
       // if (eRewared != TheAdsManager.Instance.CURRENT_GIFT) return;

        //WATCH ADS
       // TheAdsManager.Instance.WatchRewardedVideo(eRewared);


    }


    private void SetReadyAds()
    {
        /*if (!TheAdsManager.Instance.isReadyRewardedVideoAd())
        {
            buWatchAds_track1.image.color = Color.gray;
            buWatchAds_track2.image.color = Color.gray;
            buWatchAds_track3.image.color = Color.gray;
            return;
        }

        if (TheAdsManager.Instance.CURRENT_GIFT == TheAdsManager.REWARED_VIDEO.GetGift_Pack1 && TheAdsManager.Instance.isReadyRewardedVideoAd())
        {
            buWatchAds_track1.image.color = Color.white;
            buWatchAds_track2.image.color = Color.gray;
            buWatchAds_track3.image.color = Color.gray;
        }
        else if (TheAdsManager.Instance.CURRENT_GIFT == TheAdsManager.REWARED_VIDEO.GetGift_Pack2 && TheAdsManager.Instance.isReadyRewardedVideoAd())
        {
            buWatchAds_track1.image.color = Color.gray;
            buWatchAds_track2.image.color = Color.white;
            buWatchAds_track3.image.color = Color.gray;
        }
        else if (TheAdsManager.Instance.CURRENT_GIFT == TheAdsManager.REWARED_VIDEO.GetGift_Pack3 && TheAdsManager.Instance.isReadyRewardedVideoAd())
        {
            buWatchAds_track1.image.color = Color.gray;
            buWatchAds_track2.image.color = Color.gray;
            buWatchAds_track3.image.color = Color.white;
        }
        else
        {
            buWatchAds_track1.image.color = Color.gray;
            buWatchAds_track2.image.color = Color.gray;
            buWatchAds_track3.image.color = Color.gray;
        }*/
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        SetReadyAds();
    }
}

