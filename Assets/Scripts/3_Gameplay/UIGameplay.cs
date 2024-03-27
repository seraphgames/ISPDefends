using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGameplay : MonoBehaviour
{
    public static UIGameplay Instance;

    [SerializeField]
    private Button buFaster, buSetting, buAddCoin, buShowBoardSkill;


    [Space(30)]
    [SerializeField]
    private Text txtHeart;
    [SerializeField]
    private Text txtCoin, txtWave;


    [SerializeField]
    private GameObject objBoardSkill;
    private Vector3 vBoardSkillPos;



    // public GameObject txtPause;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    [Space(20)]
    //public Sprite sprPause;
    //public Sprite sprPlay;
    public float fCurrentTimeScale;


    private void Start()
    {
        GetBoardSkillPos();

        //color button faster
        buFaster.image.color = Color.gray;
        objLastWave.SetActive(false);
        ShowBoardSkill();


        buFaster.onClick.AddListener(() => SetButton(buFaster));
        buSetting.onClick.AddListener(() => SetButton(buSetting));
        buAddCoin.onClick.AddListener(() => SetButton(buAddCoin));
        buShowBoardSkill.onClick.AddListener(() => SetButton(buShowBoardSkill));


        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetButton(buSetting);
        }
    }

    //SET BUTTON
    private void SetButton(Button _bu)
    {
        if (_bu == buFaster)
        {
            if (Time.timeScale == 1)
            {
                // buFaster.image.sprite = sprFaster2x;
                buFaster.image.color = Color.white;

                Time.timeScale = 2.0f;
            }
            else if (Time.timeScale == 2)
            {
                // buFaster.image.sprite = sprFaster1x;
                buFaster.image.color = Color.gray;
                Time.timeScale = 1;
            }
        }
        else if (_bu == buSetting)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Setting);
        }
        else if (_bu == buAddCoin)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Shop);
            Shop.Instane.ShowPanel(Shop.PANEL.Coins);
        }
        else if (_bu == buShowBoardSkill)
        {
            if (TheSkillManager.Instance.eCURRENT_SKILL != TheEnumManager.POWER_UP.Null)
            {
                buShowBoardSkill.transform.GetChild(0).GetComponent<Image>().color = Color.white * 0.0f;
                TheSkillManager.Instance.eCURRENT_SKILL = TheEnumManager.POWER_UP.Null;
                MainCode_Gameplay.Instance.eCURRENT_INPUT = MainCode_Gameplay.INPUT_PLAYER.Normal;
                buShowBoardSkill.image.sprite = TheSkillManager.Instance.sprMainIcon;
            }
            ShowBoardSkill();
        }
    }


    public void ShowBoardSkill()
    {
        if (objBoardSkill.transform.position == vBoardSkillPos)
        {
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);//sound
            objBoardSkill.transform.position = Vector3.one * 1000;
        }
        else
        {
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
            objBoardSkill.transform.position = vBoardSkillPos;
        }

    }

    private void GetBoardSkillPos()
    {
        vBoardSkillPos = objBoardSkill.transform.position;
    }






    //EVNT OPEN-CLOSE UI
    private void OpenUiPopup()
    {
        if (TheGameStatusManager.CURRENT_STATUS == TheGameStatusManager.GAME_STATUS.Playing)
        {
            if (Time.timeScale != 0)
                fCurrentTimeScale = Time.timeScale;

            Time.timeScale = 0.0f;
        }
    }

    private void CloseUIPopup()
    {
        // Debug.Log("SSSSS");
        if (TheGameStatusManager.CURRENT_STATUS == TheGameStatusManager.GAME_STATUS.Playing)
        {
            if (!ThePopupManager.Instance.IsShowing)
                Time.timeScale = fCurrentTimeScale;
        }
    }



    //Last Wave
    public GameObject objLastWave;
    public void ShowLastWaveText()
    {
        objLastWave.SetActive(true);
    }


    //UPDATE BOARD
    private void UpdateBoardLevelInfo()
    {
        txtCoin.text = TheLevel.Instance.iOriginalCoin.ToString();
        txtWave.text = "WAVE:    " + (TheLevel.Instance.iCurrentWave+1).ToString() + "/" + TheLevel.Instance.iMAX_WAVE_CONFIG;
        if (TheLevel.Instance.iCurrentHeart >= 0)
            txtHeart.text = TheLevel.Instance.iCurrentHeart.ToString();
        else
        {
            txtHeart.text = "0";
        }

    }

    private void OnEnable()
    {
        TheEventManager.OnUpdateBoardInfo += UpdateBoardLevelInfo;
        TheEventManager.OnOpenUIPopup += OpenUiPopup;
        TheEventManager.OnCloseUIPopup += CloseUIPopup;
      
    }
    private void OnDisable()
    {
        TheEventManager.OnUpdateBoardInfo -= UpdateBoardLevelInfo;
        TheEventManager.OnOpenUIPopup -= OpenUiPopup;
        TheEventManager.OnCloseUIPopup -= CloseUIPopup;
    }
}
