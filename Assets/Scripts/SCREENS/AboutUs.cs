using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutUs : PopUp
{

    [SerializeField] Button buResetGame, buMoreGame;
    [SerializeField] Image imaLogo;
    [SerializeField] Text txtEmail, txtWebsite;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        buResetGame.onClick.AddListener(() => SetButton(buResetGame));
        buMoreGame.onClick.AddListener(() => SetButton(buMoreGame));
        imaLogo.sprite = ThePlatformManager.Instance.GAME_INFO.sprLogoPNG;


        txtEmail.text = "Email: " + ThePlatformManager.Instance.GAME_INFO.strEmailUser;
        txtWebsite.text = ThePlatformManager.Instance.GAME_INFO.strWebsite;
    }

    protected override void SetButton(Button _bu)
    {
        base.SetButton(_bu);
        if (_bu == buResetGame)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
            Note.Instance.ShowPopupNote(Note.NOTE.ResetGame);

        }
        else if (_bu == buMoreGame)
        {
            ThePopupManager.Instance.OpenLink(ThePlatformManager.Instance.GAME_INFO.strLinkMoreGame);
        }
    }

}
