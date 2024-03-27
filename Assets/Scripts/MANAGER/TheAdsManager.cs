using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;
//using UnityEngine.Advertisements;
using System;

public class TheAdsManager : MonoBehaviour
{
   // public static TheAdsManager Instance;
    public enum REWARED_VIDEO
    {
        Null,
        FreeGem,
        GetGift_Pack1,
        GetGift_Pack2,
        GetGift_Pack3,
    }

    //LOCK
    public bool bShowAdmobAds, bShowUnityAds, bTestAdsAdmob;



    //ID ADMOB
    private string strTestDeviceAdmob;
    private string strAdmobAppID;
    private string strAdmobBannderID;
    private string strAdmobInterstitialID;
    private string strAdmobRewardedVideoID;
    private string strUnityAdsID = "";


    //private BannerView m_BannerView;
   // private InterstitialAd m_InterstitialAd;
    //private RewardBasedVideoAd m_RewardedVideoAdmob;


    private bool bIsInit = false;
    // private 

    private void Awake()
    {
        /*if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);*/
    }


    private void Start()
    {
        //Invoke("Init", 0.2f);
    }

    public void Init()
    {
        /*if (!bIsInit)
        {
             bIsInit = true;
            if (bShowAdmobAds)
            {
                //Init
                strTestDeviceAdmob = ThePlatformManager.Instance.GAME_INFO.strTestDevicesId;
                strAdmobAppID = ThePlatformManager.Instance.GAME_INFO.strAdmob_IdPublisher;
                strAdmobBannderID = ThePlatformManager.Instance.GAME_INFO.strAdmob_Id_Banner;
                strAdmobInterstitialID = ThePlatformManager.Instance.GAME_INFO.strAdmob_Id_Interstitital;
                strAdmobRewardedVideoID = ThePlatformManager.Instance.GAME_INFO.strAdmob_Id_VideoReward;

                // Debug.Log("strAdmobBannderID: " + strAdmobBannderID);

                //ADMOB
                MobileAds.Initialize(initStatus => {});

               

                this.RequestBanner();

                //admob institial

                RequestInterstitial();
                m_InterstitialAd.OnAdClosed += HandleOnAdClosed;



                //admob video
                m_RewardedVideoAdmob = RewardBasedVideoAd.Instance;
                RequestRewardedVideo();
                m_RewardedVideoAdmob.OnAdRewarded += HandleRewardBasedVideoRewarded;
                m_RewardedVideoAdmob.OnAdClosed += HandleRewardBasedVideoClosed;

            }

            if (bShowUnityAds)
            {
                //UNITY
                //strUnityAdsID = ThePlatformManager.Instance.GAME_INFO.strUnityAdsID;
                //Advertisement.Initialize(strUnityAdsID);
            }
        }*/
    }



    #region ADMOB

    //READY ADS
   /* private bool isReadyInterstitial_Admob()
    {
        if (m_InterstitialAd == null) return false;
        return m_InterstitialAd.IsLoaded();
    }
    private bool isReadyRewardedVideo_Admob()
    {
        if (m_RewardedVideoAdmob == null) return false;
        return m_RewardedVideoAdmob.IsLoaded();
    }


    #region BANNER
    private bool isShowingBannger;
    public void RequestBanner()
    {
        
        m_BannerView = new BannerView(strAdmobBannderID, AdSize.Banner, AdPosition.Bottom);


        if ( bTestAdsAdmob)
        {
            AdRequest request = new AdRequest.Builder()
                .AddTestDevice(strTestDeviceAdmob)
                .Build();
            m_BannerView.LoadAd(request);
        }
        else
        {
            AdRequest request = new AdRequest.Builder()
               .Build();
            m_BannerView.LoadAd(request);
        }



        HideBanner();
    }
    public void ShowBanner()
    {
       
        if (!bShowAdmobAds) return;

        if (!isShowingBannger)
        {
            m_BannerView.Show();
            isShowingBannger = true;
        }
    }
    public void HideBanner()
    {
        if (m_BannerView == null) return;
        m_BannerView.Hide();
        isShowingBannger = false;
    }
    #endregion



    #region ADMOB - INTERSTITIAL 
    private void RequestInterstitial()
    {
        m_InterstitialAd = new InterstitialAd(strAdmobInterstitialID);
        if (bTestAdsAdmob)
        {
            AdRequest request = new AdRequest.Builder().AddTestDevice(strTestDeviceAdmob).Build();
            m_InterstitialAd.LoadAd(request);

        }
        else
        {
            AdRequest request = new AdRequest.Builder().Build();
            m_InterstitialAd.LoadAd(request);
        }

    }



    private void ShowInterstitial()
    {
        if (!bShowAdmobAds) return;

        if (m_InterstitialAd.IsLoaded())
            m_InterstitialAd.Show();

    }

    private void ShowInterstitiall(float _timeDelay)
    {
        StartCoroutine(IEShowInterstitial(_timeDelay));
    }

    private IEnumerator IEShowInterstitial(float _delay)
    {
        yield return new WaitForSecondsRealtime(_delay);
        ShowInterstitial();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }
    #endregion



    //ADMOB - REWAREDED VIDEO   
    private void RequestRewardedVideo()
    {

        if (bTestAdsAdmob)
        {
            AdRequest request = new AdRequest.Builder().AddTestDevice(strTestDeviceAdmob).Build();
            // Load the rewarded video ad with the request.
            m_RewardedVideoAdmob.LoadAd(request, strAdmobRewardedVideoID);
        }
        else
        {
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded video ad with the request.
            m_RewardedVideoAdmob.LoadAd(request, strAdmobRewardedVideoID);
        }
    }


    private void ShowRewardedVideo_Admob(REWARED_VIDEO eRewared)
    {
        eRewaredVideo = eRewared;
        m_RewardedVideoAdmob.Show();

    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        StartCoroutine(IeVideoAdsSeccefull(eRewaredVideo));
    }
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        RequestRewardedVideo();
    }
*/
    #endregion



    #region UNITY    
   /* private REWARED_VIDEO eRewaredVideo;

    private void ShowUnityAds()
    {
        if (!bShowUnityAds) return;

        //Advertisement.Show();
    }

    private bool isReady_UnityAds()
    {
        return false;// Advertisement.IsReady();
    }

    private void ShowRewardedVideo_Unity(REWARED_VIDEO eRewared)
    {
        if (!bShowUnityAds) return;

        eRewaredVideo = eRewared;
        //ShowOptions options = new ShowOptions();
        //options.resultCallback = HandleShowResult;

        //Advertisement.Show("rewardedVideo", options);
    }

    void HandleShowResult()//ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            // Reward your player here.
            //---------------------------------------------------------------

            StartCoroutine(IeVideoAdsSeccefull(eRewaredVideo));

            //---------------------------------------------------------------------

        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }


    #endregion



    //SHOW INTERTITAL ===============================================
    public void ShowFullAds()
    {
       
#if UNITY_EDITOR

            if (isReady_UnityAds())//UNITY
            {
                ShowUnityAds();
                return;
            }
#else

        if (isReadyInterstitial_Admob()) //ADMOB
        {
            ShowInterstitial();
            return;
        }
        else RequestInterstitial();


        if (isReady_UnityAds())//UNITY
        {
            ShowUnityAds();
            return;
        }
#endif
       
    }
    private bool bShowOneTime = false;
    public void ShowFullAdsOneTimeInGame() // show quang cao dung 1 lan trong game, phù hợp đặt khi vào menu, levelselection đầu tiên
    {

        if (!bShowOneTime)
        {
            bShowOneTime = true;
            ShowFullAds();
        }

    }





    // REWARDED VIDEO ==============================================
    public bool isReadyRewardedVideoAd()
    {
        try
        {
            if (isReadyRewardedVideo_Admob()) return true;
        }
        catch
        {
            if (isReady_UnityAds()) return true;
        }
       
        

        return false;
    }



    public void WatchRewardedVideo(REWARED_VIDEO eRewared)
    {
        try
        {
#if UNITY_EDITOR
            //UNITY
            if (isReady_UnityAds())
            {
                ShowRewardedVideo_Unity(eRewared);
                return;
            }
#else


        //ADMOB
        if (isReadyRewardedVideo_Admob())
        {
            ShowRewardedVideo_Admob(eRewared);
   
            return;
        }
        else
        {
            RequestRewardedVideo();
        }




        //UNITY
        if (isReady_UnityAds())
        {
            ShowRewardedVideo_Unity(eRewared);
       
            return;
        }

#endif
        }
        catch { }
    }




    #region SUCCESSFUL
    public REWARED_VIDEO CURRENT_GIFT = REWARED_VIDEO.GetGift_Pack1;
    private void GotGift(REWARED_VIDEO eRewared)
    {

        if (eRewared == REWARED_VIDEO.GetGift_Pack1)
        {
            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.POWER_UP.freeze,
                TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.POWER_UP.freeze) + 1);//freeze
            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.POWER_UP.fire_of_lord,
                TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.POWER_UP.fire_of_lord) + 1);//freeze
            CURRENT_GIFT = REWARED_VIDEO.GetGift_Pack2;
        }
        else if (eRewared == REWARED_VIDEO.GetGift_Pack2)
        {
            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.POWER_UP.freeze,
                TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.POWER_UP.freeze) + 1);//freeze

            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.POWER_UP.fire_of_lord,
                TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.POWER_UP.fire_of_lord) + 1);//freeze

            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.POWER_UP.guardian,
                TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.POWER_UP.guardian) + 1);//freeze
            CURRENT_GIFT = REWARED_VIDEO.GetGift_Pack3;
        }
        else if (eRewared == REWARED_VIDEO.GetGift_Pack3)
        {
            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.POWER_UP.freeze,
                TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.POWER_UP.freeze) + 1);//freeze

            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.POWER_UP.fire_of_lord,
                TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.POWER_UP.fire_of_lord) + 1);//freeze

            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.POWER_UP.guardian,
                TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.POWER_UP.guardian) + 1);//freeze

            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(TheEnumManager.POWER_UP.poison,
                TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(TheEnumManager.POWER_UP.poison) + 1);//freeze
            CURRENT_GIFT = REWARED_VIDEO.Null;
        }


        Debug.Log("DONE: " + eRewared);


        ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
        Note.Instance.ShowPopupNote(Note.NOTE.RewardedVideo);
        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
    }

    private IEnumerator IeVideoAdsSeccefull(REWARED_VIDEO eRewared)
    {
        yield return new WaitForSeconds(1.0f);
        switch (eRewared)
        {
            case REWARED_VIDEO.Null:
                break;
            case REWARED_VIDEO.FreeGem:
                eRewaredVideo = REWARED_VIDEO.Null;
                ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
                Note.Instance.ShowPopupNote(Note.NOTE.GetFreeGem);
                break;
            case REWARED_VIDEO.GetGift_Pack1:
                eRewaredVideo = REWARED_VIDEO.Null;

                GotGift(REWARED_VIDEO.GetGift_Pack1);
                break;
            case REWARED_VIDEO.GetGift_Pack2:
                eRewaredVideo = REWARED_VIDEO.Null;
                GotGift(REWARED_VIDEO.GetGift_Pack2);

                break;
            case REWARED_VIDEO.GetGift_Pack3:
                eRewaredVideo = REWARED_VIDEO.Null;
                GotGift(REWARED_VIDEO.GetGift_Pack3);

                break;
        }

    }
*/
    #endregion



}

