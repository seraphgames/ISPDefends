using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : PopUp
{
    [SerializeField] private Button buMusic, buSound, buInfo, buBugReport, buFacebook, buMenu, buReplay, buShop, buLevelSelection, buTutorial, buRateUs;

    protected override void Start()
    {
        base.Start();

        buMusic.onClick.AddListener(() => SetButton(buMusic));
        buSound.onClick.AddListener(() => SetButton(buSound));
        buInfo.onClick.AddListener(() => SetButton(buInfo));
        buBugReport.onClick.AddListener(() => SetButton(buBugReport));
        buFacebook.onClick.AddListener(() => SetButton(buFacebook));
        buMenu.onClick.AddListener(() => SetButton(buMenu));
        buShop.onClick.AddListener(() => SetButton(buShop));
        buReplay.onClick.AddListener(() => SetButton(buReplay));

        buLevelSelection.onClick.AddListener(() => SetButton(buLevelSelection));
        buTutorial.onClick.AddListener(() => SetButton(buTutorial));
        buRateUs.onClick.AddListener(() => SetButton(buRateUs));

    }

    #region AUDIO

    private void SetStatusAudioUI()
    {
        if (TheMusic.Instance.Mute)
        {
            buMusic.GetComponentInChildren<Text>().text = "MUSIC OFF";
            buMusic.image.color = Color.gray;
        }
        else
        {
            buMusic.GetComponentInChildren<Text>().text = "MUSIC ON";
            buMusic.image.color = Color.white;
        }

        if (TheSound.Instance.Mute)
        {
            buSound.GetComponentInChildren<Text>().text = "SOUND OFF";
            buSound.image.color = Color.gray;
        }
        else
        {
            buSound.GetComponentInChildren<Text>().text = "SOUND ON";
            buSound.image.color = Color.white;
        }
    }
    #endregion



    protected override void SetButton(Button _bu)
    {
        base.SetButton(_bu);

        if (_bu == buMusic)
        {
            TheMusic.Instance.Mute = !TheMusic.Instance.Mute;
            SetStatusAudioUI();
        }
        else if (_bu == buSound)
        {
            TheSound.Instance.Mute = !TheSound.Instance.Mute;
            SetStatusAudioUI();

        }
        else if (_bu == buInfo)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.AboutUs);
        }
        else if (_bu == buBugReport)
        {
            ThePopupManager.Instance.SentBugEmail();
        }
        else if (_bu == buFacebook)
        {
            ThePopupManager.Instance.OpenLink(ThePlatformManager.Instance.GAME_INFO.strFacebook);
        }
        else if (_bu == buMenu)
        {
            ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Menu);
        }
        else if (_bu == buShop)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Shop);
            Shop.Instane.ShowPanel(Shop.PANEL.PowerUps);
        }
        else if (_bu == buReplay)
        {
            if (ThePopupManager.Instance.SCENE_MANAGER.isLoadingScene(TheEnumManager.SCENE.Gameplay))
                ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Gameplay);
        }
        else if (_bu == buLevelSelection)
        {
            ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.LevelSelection);
        }
        else if (_bu == buTutorial)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Tutorial);
        }
        else if (_bu == buRateUs)
        {
            ThePopupManager.Instance.OpenLink(ThePlatformManager.Instance.GAME_INFO.strLinkLike);
        }
    }




    protected override void OnEnable()
    {
        base.OnEnable();

        SetStatusAudioUI();
        //ads
        //TheAdsManager.Instance.ShowBanner();

        //buttone replay
        if (!ThePopupManager.Instance.SCENE_MANAGER.isLoadingScene(TheEnumManager.SCENE.Gameplay))
        {
            buReplay.image.color = Color.gray;
            buReplay.transform.GetChild(0).GetComponent<Text>().color = new Vector4(1, 1f, 1f, 0.8f);
        }
        else
        {
            buReplay.image.color = Color.white;
            buReplay.transform.GetChild(0).GetComponent<Text>().color = Color.white;
            //TheAdsManager.Instance.ShowFullAds();// ads
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //ads
        //TheAdsManager.Instance.HideBanner();
    }
}
