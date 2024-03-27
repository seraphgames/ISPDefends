using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCheckInGiftManager : MonoBehaviour
{
    public static TheCheckInGiftManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Invoke("Init", 0.2f);
    }

    private void Init()
    {
        // int iNumberOfGiftReceied = TheDataManager.Instance.GetPlayerData("NumberOfGiftsReceived").iValue;

        int _total = LIST_GIFT.Count;
        GIFT_ELE _gift;
        for (int i = 0; i < _total; i++)
        {
            _gift = LIST_GIFT[i];
            if (i < TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived)
            {
                _gift.bReceied = true;
                LIST_GIFT[i] = _gift;
                Debug.Log("AAA: " + TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived);
            }
            else
                break;

        }
    }


    //DESING GIFT
    public enum Gift
    {
        Gift_BoomFromSkySkill_2_1,
        Gift_AddGem_20,
        Gift_FreezeSkill_2_1,
        Gift_AddGem_30,
        Gift_AddBloodSkill_2,
        Gift_AddGem_40,
        Gift_AddGem_50,
        Gift_FreezeSkill_2_2,
        Gift_GuardiaSkill_2_1,
        Gift_AddGem_60,
        Gift_MineOnRoadSkill_2_1,
        Gift_FreezeSkill_2_3,
        Gift_BoomFromSkySkill_2_2,
        Gift_AddGem_70,
        Gift_GuardiaSkill_2_2,
        Gift_FreezeSkill_2_4,
        Gift_MineOnRoadSkill_2_2,
        Gift_AddGem_75,
        Gift_AddBlood_2_1,
        Gift_BoomFromSkySkill_2_3,
        Gift_FreezeSkill_2_5,
        Gift_AddGem_80,
        Gift_AddBlood_2_2,
        Gift_GuardiaSkill_2_3,
        Gift_BoomFromSkySkill_2_4,
        Gift_MineOnRoadSkill_2_3,
        Gift_AddGem_90,
        Gift_AddBlood_2_3,
        Gift_AddBlood_2_4,
        Gift_MineOnRoadSkill_2_4,
        Gift_AddGem_120,
        Gift_AddBlood_2_5,
        Gift_FreezeSkill_2_6,
        Gift_BoomFromSkySkill_2_5,
        Gift_AddGem_200,

    }

    public enum KIND_OF_GIFT
    {
        Gifl_Is_Skill,
        Gift_Is_Gem,
    }
    [System.Serializable]
    public struct GIFT_ELE
    {
        public Gift eGift;
        public KIND_OF_GIFT eKindOfGift;
        public TheEnumManager.POWER_UP eSkill;

        [Space(20)]
        public Sprite sprIcon;
        public int iValue;
        public bool bReceied;
    }
    public List<GIFT_ELE> LIST_GIFT;

    [ContextMenu("Soft List Gift now")]
    public void SoftListGift()
    {
        int _totalGift = System.Enum.GetNames(typeof(Gift)).Length;
        for (int i = 0; i < _totalGift; i++)
        {
            GIFT_ELE _myGift = LIST_GIFT[i];
            _myGift.eGift = (Gift)i;
            //kind of gift
            string strGift = _myGift.eGift.ToString();
            if (strGift.Contains("AddGem")) _myGift.eKindOfGift = KIND_OF_GIFT.Gift_Is_Gem;
            else
            {
                _myGift.eKindOfGift = KIND_OF_GIFT.Gifl_Is_Skill;
                if (_myGift.eGift.ToString().Contains("BoomFromSkySkill")) _myGift.eSkill = TheEnumManager.POWER_UP.fire_of_lord;
                if (_myGift.eGift.ToString().Contains("FreezeSkill")) _myGift.eSkill = TheEnumManager.POWER_UP.freeze;
                if (_myGift.eGift.ToString().Contains("Poison")) _myGift.eSkill = TheEnumManager.POWER_UP.poison;
                if (_myGift.eGift.ToString().Contains("MineOnRoadSkill")) _myGift.eSkill = TheEnumManager.POWER_UP.mine_on_road;
                if (_myGift.eGift.ToString().Contains("GuardiaSkill")) _myGift.eSkill = TheEnumManager.POWER_UP.guardian;
                //if (_myGift.eGift.ToString().Contains("")) _myGift.eSkill = TheSkillManager.SKILL.Guardian;

            }


            LIST_GIFT[i] = _myGift;
        }
        Debug.Log("DONE!");
    }




    public GIFT_ELE GetGift(Gift _Gift)
    {
        return LIST_GIFT[(int)_Gift];
    }


    //DESIGN GIFT
    public void ReturnGift(Gift _gift)
    {
        GIFT_ELE _mygift = LIST_GIFT[(int)_gift];

        if (_mygift.eKindOfGift == KIND_OF_GIFT.Gift_Is_Gem)//gift is Gem
        {
            TheDataManager.THE_PLAYER_DATA.GEM += _mygift.iValue;
            Debug.Log("GIFT GEM NOW!");
        }
        else if (_mygift.eKindOfGift == KIND_OF_GIFT.Gifl_Is_Skill)//gift if Skill
        {
            int _number = 0;
            _number = TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(_mygift.eSkill) + _mygift.iValue;
            TheDataManager.THE_PLAYER_DATA.SetNumberOfSkill(_mygift.eSkill, _number);

            Debug.Log("GIFT SKILL NOW!");
        }

        TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived++;
        
        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
    }
    public int GetGiftValue(Gift _gift)
    {
        GIFT_ELE _mygift = LIST_GIFT[(int)_gift];
        return _mygift.iValue;
    }
}
