using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCode_Upgrade : MonoBehaviour
{
    public static MainCode_Upgrade Instance;
    public Button buBack;
    public Button buReset, buUpgrade;
    private ButtonUpgrade m_ButtonUpgrade;



    public GameObject objMark;//danh dau
    public Text txtName, txtContent;
    public Image imaPriceStar;
    public Image imaMainIcon;

    [Space(20)]
    public Sprite sprStar_Normal;
    public Sprite sprStar_Hard;
    public Sprite sprStar_Nightmate;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        //set camera for popup canvas
        ThePopupManager.Instance.SetCameraForPopupCanvas(GameObject.Find("Main Camera").GetComponent<Camera>());

        if (Instance == null)
            Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        buBack.onClick.AddListener(() => ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.LevelSelection, true));
        buReset.onClick.AddListener(() => ButtonReset());
        buUpgrade.onClick.AddListener(() => ButtonUpgrade());

        txtName.text = "";
        txtContent.text = "";
        imaPriceStar.GetComponentInChildren<Text>().text = "";


        SetStatus();

        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event


    }


    private void ButtonReset()
    {
        if (m_ButtonUpgrade.m_UpgradeConfig.ACTIVED)
        {
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_upgrade_reset);//sound

            m_ButtonUpgrade.Upgrade(false);
           // TheDataManager.Instance.ReadFileCSV_TowerConfig();//Load defaul

            SetStatus();


            //SAVE
            TheDataManager.Instance.SerialzerPlayerData();//save

            TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
        }
    }

    private void ButtonUpgrade()
    {
        int _startNotUsed = 0;
        switch (m_ButtonUpgrade.m_UpgradeConfig.eStarType)
        {
            case TheEnumManager.STAR_TYPE.white:
                _startNotUsed = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Normal) - 
                    TheDataManager.Instance.UPGRADE_DATA_MANAGER.GetTotalStarWasUsed(TheEnumManager.STAR_TYPE .white);
                break;
            case TheEnumManager.STAR_TYPE.blue:
                _startNotUsed = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Hard) - 
                    TheDataManager.Instance.UPGRADE_DATA_MANAGER.GetTotalStarWasUsed(TheEnumManager.STAR_TYPE.blue);
                break;
            case TheEnumManager.STAR_TYPE.yellow:
                _startNotUsed = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Nightmate) - 
                    TheDataManager.Instance.UPGRADE_DATA_MANAGER.GetTotalStarWasUsed(TheEnumManager.STAR_TYPE.yellow);
                break;

        }


        if (_startNotUsed >= m_ButtonUpgrade.m_UpgradeConfig.iStarPrice || ThePlatformManager.Instance.CHOOSING_MODE== ThePlatformManager.MODE.Testting)
        {
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_upgrade_upgrade);//sound
            //SAVE
            m_ButtonUpgrade.m_UpgradeConfig.ACTIVED=true;
           // TheDataManager.Instance.SerialzerPlayerData();//save


            //data
           //? TheDataManager.Instance.ReadFileCSV_TowerConfig();//Load defaul
            m_ButtonUpgrade.Upgrade(true);
            SetStatus();


            TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
        }
    }

    private void SetStatus()
    {
        if (!m_ButtonUpgrade)
        {
            buReset.image.color = Color.white * 0.5f;
            buUpgrade.image.color = Color.white * 0.5f;
        }
        else
        {
            if (m_ButtonUpgrade.m_UpgradeConfig.ACTIVED )
            {
                buReset.image.color = Color.white * 1.0f;
                buUpgrade.image.color = Color.white * 0.5f;
            }
            else
            {
                buReset.image.color = Color.white * 0.5f;
                buUpgrade.image.color = Color.white * 1.0f;
            }
            imaMainIcon.sprite = m_ButtonUpgrade.GetButton().image.sprite;
        }
    }

    public void SetUpgrade(ButtonUpgrade _buttonUpgrade)
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound

        m_ButtonUpgrade = _buttonUpgrade;
        SetStatus();
        objMark.transform.position = _buttonUpgrade.transform.position + new Vector3(0, 0.05f, 0);


        txtName.text = _buttonUpgrade.m_UpgradeConfig.strName.ToUpper();
        txtContent.text = _buttonUpgrade.m_UpgradeConfig.strContent;
        imaPriceStar.GetComponentInChildren<Text>().text = _buttonUpgrade.m_UpgradeConfig.iStarPrice.ToString();
        switch (_buttonUpgrade.m_UpgradeConfig.eStarType )
        {
            case TheEnumManager.STAR_TYPE.white:
                imaPriceStar.sprite = sprStar_Normal;
                break;
            case TheEnumManager.STAR_TYPE.blue:
                imaPriceStar.sprite = sprStar_Hard;
                break;
            case TheEnumManager.STAR_TYPE.yellow:
                imaPriceStar.sprite = sprStar_Nightmate;
                break;

        }


    }
}
