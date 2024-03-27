using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : PopUp
{
    public static Note Instance;
    public enum NOTE
    {
        NotEnoughtCoin,
        NotEnoughtGem,
        WatchAdsToGetFreeGem,
        GetFreeGem,
        Need3StarToUnlock,
        // UnlockHero,
        RewardedVideo,
        ResetGame,
        TowerIsReady,
        AddContent,
    }


    private static NOTE eNote;



    public Button buOk;
    public Text txtContent;
    private static string m_strContent;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    protected override void Start()
    {
        base.Start();
        buOk.onClick.AddListener(() => SetButton(buOk));
    }


    private void TextContent()
    {
        switch (eNote)
        {
            case NOTE.NotEnoughtCoin:
                txtContent.text = "Woop! Not enghout coin! \n Do you wan't more?";
                break;
            case NOTE.NotEnoughtGem:
                txtContent.text = "Woop! Not enghout gem! \n Do you wan't more?";
                break;
            case NOTE.WatchAdsToGetFreeGem:
                txtContent.text = "Watch Ads to get +" + TheDataManager.Instance.iGemFormWatchingAds + " GEMS now!";
                break;
            case NOTE.GetFreeGem:
                txtContent.text = "Perfect! You get " + TheDataManager.Instance.iGemFormWatchingAds + " GEMS!";
                break;
            case NOTE.Need3StarToUnlock:
                txtContent.text = "Need 3 stars to unlock !";
                break;
            //case NOTE.UnlockHero:
            //    txtContent.text = "Do you want to unlock Hero?";
            // break;
            case NOTE.RewardedVideo:
                txtContent.text = "Congratulations, you got the gift!";

                break;
            case NOTE.ResetGame:
                txtContent.text = "You will lose all data. \n Are you sure?";
                break;
            case NOTE.TowerIsReady:
                txtContent.text = "This Tower is ready to use!";
                break;
            case NOTE.AddContent:
                txtContent.text = m_strContent;
                break;
            default:
                break;
        }
    }

    public void ShowPopupNote(NOTE _note)
    {
        eNote = _note;
        TextContent();
    }
    public void ShowPopupNote(string _content)
    {
        eNote = NOTE.AddContent;
        m_strContent = _content;
        TextContent();

    }

    protected override void SetButton(Button _bu)
    {
        base.SetButton(_bu);
        if (_bu == buOk)
        {
            switch (eNote)
            {
                case NOTE.NotEnoughtCoin:
                    ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Shop);
                    Shop.Instane.ShowPanel(Shop.PANEL.Coins);
                    break;
                case NOTE.NotEnoughtGem:
                    ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Shop);
                    Shop.Instane.ShowPanel(Shop.PANEL.Gems);
                    break;
                case NOTE.WatchAdsToGetFreeGem:
                   // TheAdsManager.Instance.WatchRewardedVideo(TheAdsManager.REWARED_VIDEO.FreeGem);
                    break;
                case NOTE.GetFreeGem:

                    break;

                case NOTE.Need3StarToUnlock:
                    break;

                //case NOTE.UnlockHero:
                //    TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.HeroRoom);
                // break;
                case NOTE.RewardedVideo:
                    ThePopupManager.Instance.Hide(ThePopupManager.POP_UP.Note);
                    ThePopupManager.Instance.Hide(ThePopupManager.POP_UP.RewardedVideo);
                    break;
                case NOTE.ResetGame:
                    TheDataManager.Instance.ResetGame();
                    break;
            }
            ThePopupManager.Instance.Hide(ThePopupManager.POP_UP.Note);
        }
    }


    protected override void OnDisable()
    {
        base.OnDisable();


        if (eNote == NOTE.GetFreeGem)
        {
            TheDataManager.THE_PLAYER_DATA.GEM += TheDataManager.Instance.iGemFormWatchingAds;
            TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
    }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        TextContent();
    }
}
