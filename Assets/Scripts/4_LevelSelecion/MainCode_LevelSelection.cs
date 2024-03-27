using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCode_LevelSelection : MonoBehaviour, IButton
{

    public static MainCode_LevelSelection Instance;
    public Transform GroupButtonLevel; // Group button level
    public ScrollRect m_ScrollRectLevel;
    [Space(20)]
    public Button buBack;

    public Button buSetting, buAcheivenment, buLeaderboard;
    public Button buRewardedVideo;

    [Space(20)]
    public Button buShop;
    public Button buUpgrade, buHeroRoom, buBook;

    [Space(20)]
    public Text txtTotalStar;

    [Space(20)]
    public List<Sprite> LIST_SPRITE_OF_BUTTON_LEVEL_NORMAL;
    public List<Sprite> LIST_SPRITE_OF_BUTTON_LEVEL_HARD;
    public List<Sprite> LIST_SPRITE_OF_BUTTON_LEVEL_NIGHTMATE;

    [Space(20)]
    public List<Sprite> LIST_SPRITE_ICON_MAP;


    public GameObject objBoardYouAreHere;


    private void Awake()
    {//set camera for popup canvas
        Application.targetFrameRate = 60;
        ThePopupManager.Instance.SetCameraForPopupCanvas(Camera.main);


        if (Instance == null) Instance = this;

    }


    private void Start()
    {
        m_ScrollRectLevel.verticalNormalizedPosition = 1;
        m_ScrollRectLevel.horizontalNormalizedPosition = 0;
        txtTotalStar.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Normal).ToString();


        buBack.onClick.AddListener(() => SetButton(buBack));
        buSetting.onClick.AddListener(() => SetButton(buSetting));
        buShop.onClick.AddListener(() => SetButton(buShop));
        buUpgrade.onClick.AddListener(() => SetButton(buUpgrade));
        buBook.onClick.AddListener(() => SetButton(buBook));
        buRewardedVideo.onClick.AddListener(() => SetButton(buRewardedVideo));

        Init();


        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event


        //ads
       /* try
        {
            TheAdsManager.Instance.ShowFullAdsOneTimeInGame();
        }
        catch { }*/
    }

    public void SetButton(Button _bu)
    {
        if (_bu == buBack)
        {
            ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Menu, true);
        }
        else if (_bu == buSetting)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Setting);
        }
        else if (_bu == buShop)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Shop);
            Shop.Instane.ShowPanel(Shop.PANEL.PowerUps);

        }
        else if (_bu == buUpgrade)
        {
            ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Upgrade);
        }
        else if (_bu == buBook)
        {
            ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Encyclopedia);
        }
        else if (_bu == buRewardedVideo)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.RewardedVideo);
        }
    }






    //INIT
    private void Init()
    {
        int _totalLevel = GroupButtonLevel.childCount;
        for (int i = 0; i < _totalLevel; i++)
        {
            ButtonLevel _buttonLevel = GroupButtonLevel.GetChild(i).GetComponent<ButtonLevel>();


            int _star_normal = TheDataManager.THE_PLAYER_DATA.GetStar(i, TheEnumManager.DIFFICUFT.Normal);
            int _star_hard = TheDataManager.THE_PLAYER_DATA.GetStar(i, TheEnumManager.DIFFICUFT.Hard);
            int _star_nightmate = TheDataManager.THE_PLAYER_DATA.GetStar(i, TheEnumManager.DIFFICUFT.Nightmate);


            if (i == 0)
            {

                if (_star_normal < 0)
                {
                    _buttonLevel.Init(i, _star_normal, TheEnumManager.DIFFICUFT.Normal, false);

                    objBoardYouAreHere.transform.GetChild(0).GetComponentInChildren<Text>().text = (i + 1).ToString();
                    objBoardYouAreHere.GetComponent<RectTransform>().transform.position =
                        _buttonLevel.GetComponent<RectTransform>().transform.position + new Vector3(0, 0.3f, 0);
                }

                else
                {
                    if (_star_nightmate > 0)
                    {
                        _buttonLevel.Init(i, _star_nightmate, TheEnumManager.DIFFICUFT.Nightmate, false);
                    }
                    else if (_star_hard > 0)
                    {

                        _buttonLevel.Init(i, _star_hard, TheEnumManager.DIFFICUFT.Hard, false);
                    }
                    else if (_star_normal > 0)
                    {
                        _buttonLevel.Init(i, _star_normal, TheEnumManager.DIFFICUFT.Normal, false);
                    }

                }
            }
            else // i>=1
            {
                if (_star_normal > 0)
                {

                    if (_star_nightmate > 0)
                    {
                        _buttonLevel.Init(i, _star_nightmate, TheEnumManager.DIFFICUFT.Nightmate, false);

                    }
                    else if (_star_hard > 0)
                    {
                        _buttonLevel.Init(i, _star_hard, TheEnumManager.DIFFICUFT.Hard, false);

                    }
                    else if (_star_normal > 0)
                    {
                        _buttonLevel.Init(i, _star_normal, TheEnumManager.DIFFICUFT.Normal, false);
                    }
                }
                else if (_star_normal == 0 || TheDataManager.THE_PLAYER_DATA.GetStar(i - 1, TheEnumManager.DIFFICUFT.Normal) > 0)
                {
                    _buttonLevel.Init(i, _star_normal, TheEnumManager.DIFFICUFT.Normal, false);

                    objBoardYouAreHere.transform.GetChild(0).GetComponentInChildren<Text>().text = (i + 1).ToString();
                    objBoardYouAreHere.transform.position = _buttonLevel.transform.position + new Vector3(0, 0.55f, 0);


                }
                else
                {
                    _buttonLevel.Hide(i);
                }
            }


        }
    }

    //SHOW TEXT
    [ContextMenu("Show text number for level button")]
    public void ShowNumberOfLevelButton()
    {
        for (int i = 0; i < GroupButtonLevel.childCount; i++)
        {
            Text _text = GroupButtonLevel.GetChild(i).GetComponentInChildren<Text>();
            _text.text = (i + 1).ToString();
            GroupButtonLevel.GetChild(i).gameObject.name = (i + 1).ToString();
        }
    }

    public Sprite GetSpriteIconMap(int _level)
    {
        if (_level < LIST_SPRITE_ICON_MAP.Count)
            return LIST_SPRITE_ICON_MAP[_level];
        else return null;
    }


}
