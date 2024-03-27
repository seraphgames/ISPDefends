using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckIn : PopUp
{
    public List<Button> LIST_BUTTON_WEEK;
    public List<CheckIn_ButtonDay> LIST_BUTTON_DAY;

    private int iIndexOfWeek;
    private int iIndexOfDay;

    public int iNumberOfGiftsReceived = 0;
    public Text txtTextResuft;

    //private void Awake()
    //{
    //    Debug.Log("iNumberOfGiftsReceived: " + PlayerPrefs.GetInt("iNumberOfGiftsReceived"));
    //    iNumberOfGiftsReceived = PlayerPrefs.GetInt("iNumberOfGiftsReceived");

    //}
    protected override void Start()
    {
        base.Start();



        LIST_BUTTON_WEEK[0].onClick.AddListener(() => ButtonWeek(1));
        LIST_BUTTON_WEEK[1].onClick.AddListener(() => ButtonWeek(2));
        LIST_BUTTON_WEEK[2].onClick.AddListener(() => ButtonWeek(3));
        LIST_BUTTON_WEEK[3].onClick.AddListener(() => ButtonWeek(4));
        LIST_BUTTON_WEEK[4].onClick.AddListener(() => ButtonWeek(5));



    }


    protected override void OnEnable()
    {
        base.OnEnable();

        ButtonWeek((int)(iNumberOfGiftsReceived * 1.0f / 7) + 1);
        ShowRedNote();

    }


    private void ButtonWeek(int _index)
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
        iIndexOfWeek = _index;

        //Set index for button day
        int _total = LIST_BUTTON_DAY.Count;
        for (int i = 0; i < _total; i++)
        {
            int _day = (i + (_index - 1) * 7);
            LIST_BUTTON_DAY[i].iIndex = _day;
            LIST_BUTTON_DAY[i].Init();
        }

        //Set color for button week
        for (int i = 0; i < LIST_BUTTON_WEEK.Count; i++)
        {
            if (i == _index - 1)
            {
                LIST_BUTTON_WEEK[i].image.color = Color.white;
                // LIST_BUTTON_WEEK[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                LIST_BUTTON_WEEK[i].image.color = Color.gray;
                // LIST_BUTTON_WEEK[i].transform.GetChild(1).gameObject.SetActive(false );
            }
        }

    }

    //SHOW RED NOTE
    private void ShowRedNote()
    {
        iNumberOfGiftsReceived = TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived;
        int _index = (int)(iNumberOfGiftsReceived * 1.0f / 7);
        for (int i = 0; i < LIST_BUTTON_WEEK.Count; i++)
        {
            if (i == _index)
            {
                LIST_BUTTON_WEEK[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else LIST_BUTTON_WEEK[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }



    //GET TO DAY
    private int iCurrentDayOfYear = 0;
    private int iCurrentYear = 0;
    public DateTime GetToday()
    {
        System.DateTime today = DateTime.Today;
        return today;
    }


    //CHECK TO GET GIFT
    public bool CheckToShowCheckInPopup()
    {
        iNumberOfGiftsReceived = TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived;
        iCurrentYear = TheDataManager.THE_PLAYER_DATA.iCurrentYear;
        iCurrentDayOfYear = TheDataManager.THE_PLAYER_DATA.iCurrentDayOfYear;

        //debug:
        Debug.Log("TIME HE THONG: " + GetToday().Year + "/DayOfYear: " + GetToday().DayOfYear);
        Debug.Log("CURRENT: " + iCurrentYear + "/DayOfYear: " + iCurrentDayOfYear);





        if (GetToday().Year > iCurrentYear)
            return true;

        if (GetToday().DayOfYear > iCurrentDayOfYear)
            return true;



        return false;
    }


    //GET GIFT
    public void GetGiftCheckIn(int _indexOfGift)
    {
        //Get today

        iCurrentYear = TheDataManager.THE_PLAYER_DATA.iCurrentYear;
        iCurrentDayOfYear = TheDataManager.THE_PLAYER_DATA.iCurrentDayOfYear;
        Debug.Log("_indexOfGift - iNumberOfGiftsReceived: " + _indexOfGift.ToString() + "/" + iNumberOfGiftsReceived.ToString());
        if ((_indexOfGift - iNumberOfGiftsReceived) != 0)
        {
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_can_not);//sound
            Debug.Log("Chưa dến ngày!");
            return;
        }



        if (GetToday().Year > iCurrentYear || GetToday().DayOfYear > iCurrentDayOfYear)
        {
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);//sound
            //Save         
            TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived = iNumberOfGiftsReceived;
            TheDataManager.THE_PLAYER_DATA.iCurrentYear = GetToday().Year;
            TheDataManager.THE_PLAYER_DATA.iCurrentDayOfYear = GetToday().DayOfYear;
            TheDataManager.Instance.SerialzerPlayerData();


            // iNumberOfGiftsReceived
            TheCheckInGiftManager.Instance.ReturnGift((TheCheckInGiftManager.Gift)iNumberOfGiftsReceived);
            //show text
            ShowTextAnimation("+" + TheCheckInGiftManager.Instance.GetGiftValue((TheCheckInGiftManager.Gift)iNumberOfGiftsReceived).ToString());


            iNumberOfGiftsReceived += 1;

        }
    }

    //SHOW TEXT-ANIMATION
    public void ShowTextAnimation(string _text)
    {
        txtTextResuft.text = _text;
        txtTextResuft.gameObject.SetActive(false);
        txtTextResuft.gameObject.SetActive(true);
    }
}
