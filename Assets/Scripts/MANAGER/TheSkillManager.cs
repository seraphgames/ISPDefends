using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TheSkillManager : MonoBehaviour
{

    public TheEnumManager.POWER_UP eCURRENT_SKILL;

    [System.Serializable]
    public class PowerUpUI
    {
        public TheEnumManager.POWER_UP ePowerUp;
        [SerializeField] private Button buCall;
        private Text txtCurrentValue;
        private Color coGray = new Color(0.8f, 0.8f, 0.8f, 1f);


   
        public int iCurrentValue
        {
            get
            {
                return TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(ePowerUp);
            }
            set
            {
                if (value < 0)
                {
                   value=0;
                }
                TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(ePowerUp, value);
                
            }

        }



        private Sprite sptSprite;
        private CircleTime thisCircleTime;
        [SerializeField] GameObject objEffect;

        private bool isReady
        {
            get
            {
                if (iCurrentValue <= 0) return false;
                return thisCircleTime.IsReady();
            }
        }


        public void Init()
        {
            thisCircleTime = buCall.transform.Find("CircleTime").GetComponent<CircleTime>();
            buCall.onClick.AddListener(() => SetPowerUp());
            txtCurrentValue = buCall.GetComponentInChildren<Text>();

            sptSprite = buCall.image.sprite;
            iCurrentValue = TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(ePowerUp);
            UpdateUIStatus();
        }


        public void UpdateUIStatus()
        {
            txtCurrentValue.text = iCurrentValue.ToString();
            if (iCurrentValue == 0) buCall.image.color = coGray;
            else buCall.image.color = Color.white;
        }

        public void SetPowerUp()
        {

            if (!isReady) return;


            //active
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
            Instance.eCURRENT_SKILL = ePowerUp;
            MainCode_Gameplay.Instance.SetInput(MainCode_Gameplay.INPUT_PLAYER.ReadyToUseSkill);
            Instance.imaMainIcon.sprite = sptSprite;
            Instance.imaMainIcon.transform.GetChild(0).GetComponent<Image>().color = Color.white * 1.0f;
            UIGameplay.Instance.ShowBoardSkill();

            //update ui
            UpdateUIStatus();
        }
        public void Active(Vector2 _pos)
        {
            if (!isReady) return;

            iCurrentValue -= 1;
            thisCircleTime.StartCount();

            //effect
            GameObject _circleLight = Instantiate(objEffect);
            _circleLight.SetActive(false);
            if (_circleLight)
            {
                _circleLight.transform.position = _pos;
                _circleLight.SetActive(true);
            }



            switch (ePowerUp)
            {
                case TheEnumManager.POWER_UP.guardian:
                    //upgrade system
                    if (TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(TheEnumManager.UPGRADE.Reinforcement_2To3Man).ACTIVED)
                        Instantiate(TheObjPoolingManager.Instance.objSkill_Reinforcements_3Mans, _pos, Quaternion.identity);//3 mans
                    else
                        Instantiate(TheObjPoolingManager.Instance.objSkill_Reinforcements_2Mans, _pos, Quaternion.identity); //2 mans

                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.skill_soldier);//sound
                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.soldier_no_fair);//sound
                    break;


                case TheEnumManager.POWER_UP.freeze:

                    Instance.PostPowerUpEvent(TheEnumManager.POWER_UP.freeze, _pos);
                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.skill_freeze);//sound
                    break;
                case TheEnumManager.POWER_UP.fire_of_lord:
                    Instance.StartCoroutine(Instance.IEFireFromSky(7, _pos));
                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.skill_lord_fire);//sound
                    break;

                case TheEnumManager.POWER_UP.mine_on_road:
                    Instantiate(TheObjPoolingManager.Instance.objSkill_MineOfRoad, _pos, Quaternion.identity);
                    break;

                case TheEnumManager.POWER_UP.poison:
                    Instantiate(TheObjPoolingManager.Instance.objSkill_PondOfPoison, _pos, Quaternion.identity);
                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.skill_poison);//sound
                    break;
                case TheEnumManager.POWER_UP.Null:
                    break;
            }


            MainCode_Gameplay.Instance.SetInput(MainCode_Gameplay.INPUT_PLAYER.Normal);
            //TheDataManager.Instance.SerialzerPlayerData();//save

            UpdateUIStatus();
            Instance.imaMainIcon.sprite = Instance.sprMainIcon;
            UIGameplay.Instance.ShowBoardSkill();
            Instance.imaMainIcon.transform.GetChild(0).GetComponent<Image>().color = Color.white * 0.0f;
        }
    }

    [System.Serializable]
    public class PowerUIManager : TheGeneric<PowerUpUI>
    {

    }

    public PowerUIManager POWER_UI_MANAGER;



    [Space(20)]
    public Image imaMainIcon;
    public Sprite sprMainIcon;


    // private MyData m_mydataGuardian, m_mydataFreeze, m_mydataDestroyWithBoom, m_mydataBoom, m_mydataAddBlood;

    public static TheSkillManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        foreach (var item in POWER_UI_MANAGER.MY_LIST)
        {
            item.Init();
        }
    }



    //READY USE SKILL
    private Sprite _tempSprite;


    //FIRE FROM SKY
    private IEnumerator IEFireFromSky(int _num, Vector2 _pos)
    {
        for (int i = 0; i < _num; i++)
        {
            GameObject _fire = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.FireFromSky).GetItem();
            if (_fire)
            {
                _fire.GetComponent<FireMove>().Play(_pos + new Vector2(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1, 1)));
                _fire.SetActive(true);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }


    //EVENT==================================================================
    public delegate void UsedSkill(TheEnumManager.POWER_UP _powerUp, Vector2 _pos);

    //event Freeze
    public event UsedSkill OnUsedPower;
    public void PostPowerUpEvent(TheEnumManager.POWER_UP _powerUp, Vector2 _pos)
    {
        if (OnUsedPower != null)
            OnUsedPower(_powerUp, _pos);
    }



    private void HandleUpdateUIStatus()
    {
        foreach (var item in POWER_UI_MANAGER.MY_LIST)
        {
            item.UpdateUIStatus();
        }
    }


    private void OnEnable()
    {
        TheEventManager.OnUpdateSkillBoard += HandleUpdateUIStatus;
    }



    private void OnDisable()
    {
        TheEventManager.OnUpdateSkillBoard -= HandleUpdateUIStatus;
    }
}

