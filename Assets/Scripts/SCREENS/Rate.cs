using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rate : PopUp {

    public enum TYLE
    {
        EnjoyingThisGame,
        WouldYouMindGivingUsSomeFeedback,
        HowAboutARating,
    }

    public TYLE eTyle;

    public Button buYes, buNot;
    public Text txtContent;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        buYes.onClick.AddListener(() => ButtonYes());
        buNot.onClick.AddListener(() => ButtonNot());

    }

    private void ButtonYes()
    {
        switch (eTyle)
        {
            case TYLE.EnjoyingThisGame:
                eTyle = TYLE.HowAboutARating;
                ShowText();
                break;
            case TYLE.WouldYouMindGivingUsSomeFeedback:
                ThePopupManager.Instance.SentBugEmail();
                ThePopupManager.Instance.Hide(ThePopupManager.POP_UP.Rate);
                break;
            case TYLE.HowAboutARating:
                PlayerPrefs.SetString("like", "done");
                PlayerPrefs.Save();

                ThePopupManager.Instance.OpenLink(ThePlatformManager.Instance.GAME_INFO.strLinkLike);
                ThePopupManager.Instance.Hide(ThePopupManager.POP_UP.Rate);
                break;
        }
    }

    private void ButtonNot()
    {
        switch (eTyle)
        {
            case TYLE.EnjoyingThisGame:
                eTyle = TYLE.WouldYouMindGivingUsSomeFeedback;
                ShowText();
                break;
            case TYLE.WouldYouMindGivingUsSomeFeedback:
                ThePopupManager.Instance.Hide(ThePopupManager.POP_UP.Rate);
                break;
            case TYLE.HowAboutARating:
                ThePopupManager.Instance.Hide(ThePopupManager.POP_UP.Rate);
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        eTyle = TYLE.EnjoyingThisGame;
        ShowText();
    }

    private void ShowText()
    {
        switch (eTyle)
        {
            case TYLE.EnjoyingThisGame:
                txtContent.text = "Enjoying this game?";
                buYes.GetComponentInChildren<Text>().text = "YES";
                buNot.GetComponentInChildren<Text>().text = "NO REALLY";
                break;
            case TYLE.WouldYouMindGivingUsSomeFeedback:
                txtContent.text = "Would you mind giving us some feedback?";
                buYes.GetComponentInChildren<Text>().text = "OK, SURE";
                buNot.GetComponentInChildren<Text>().text = "NO, THANKS";
                break;
            case TYLE.HowAboutARating:
                txtContent.text = "How about a rating on store, then?";
                buYes.GetComponentInChildren<Text>().text = "OK, SURE";
                buNot.GetComponentInChildren<Text>().text = "NO, THANKS";
                break;

        }
    }

}
