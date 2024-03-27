using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ready : PopUp
{

    public Button buPlay;
    public Sprite m_sprNormalButton, m_sprChoosedButton;

    [Space(20)]
    public Button buMode_Normal;
    public Button buMode_Hard, buMore_Nightmate;

    [Space(20)]
    public Text txtLevelTitle;
    public Text txtContentOfTips;

    [Space(20)]
    public Sprite sprStar_empty;
    public Sprite sprStar_Normal, sprStar_Hard, sprStar_Difficuft;

    [Space(20)]
    public Image imaIconMap;

    [Space(20)]
    public List<Image> LIST_IMAGE_STAR;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        buPlay.onClick.AddListener(() => ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Gameplay));


        buMode_Normal.onClick.AddListener(() => GameMode(TheEnumManager.DIFFICUFT.Normal));
        buMode_Hard.onClick.AddListener(() => GameMode(TheEnumManager.DIFFICUFT.Hard));
        buMore_Nightmate.onClick.AddListener(() => GameMode(TheEnumManager.DIFFICUFT.Nightmate));


    }


    int _currentStar_Normal;
    int _currentStar_Hard;
    int _currentStar_Nightmate;
    private void GameMode(TheEnumManager.DIFFICUFT eDifficuft)
    {

        switch (eDifficuft)
        {
            case TheEnumManager.DIFFICUFT.Normal:
                TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
                buMode_Normal.image.sprite = m_sprChoosedButton;
                buMore_Nightmate.image.sprite = m_sprNormalButton;
                buMode_Hard.image.sprite = m_sprNormalButton;

                TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT = TheEnumManager.DIFFICUFT.Normal;

                SetShowStar(_currentStar_Normal, sprStar_Normal);
                break;
            case TheEnumManager.DIFFICUFT.Hard:

                if (_currentStar_Normal == 3 || TheDataManager.THE_PLAYER_DATA.TESTING_MODE)
                {
                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
                    buMode_Hard.image.sprite = m_sprChoosedButton;
                    buMore_Nightmate.image.sprite = m_sprNormalButton;
                    buMode_Normal.image.sprite = m_sprNormalButton;

                    TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT = TheEnumManager.DIFFICUFT.Hard;
                    SetShowStar(_currentStar_Hard, sprStar_Hard);
                }
                else
                {
                    ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
                    Note.Instance.ShowPopupNote(Note.NOTE.Need3StarToUnlock);
                }


                break;
            case TheEnumManager.DIFFICUFT.Nightmate:
                if (_currentStar_Hard == 3 || TheDataManager.THE_PLAYER_DATA.TESTING_MODE)
                {


                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
                    buMore_Nightmate.image.sprite = m_sprChoosedButton;
                    buMode_Hard.image.sprite = m_sprNormalButton;
                    buMode_Normal.image.sprite = m_sprNormalButton;

                    TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT = TheEnumManager.DIFFICUFT.Nightmate;
                    SetShowStar(_currentStar_Nightmate, sprStar_Difficuft);
                }
                else
                {
                    ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
                    Note.Instance.ShowPopupNote(Note.NOTE.Need3StarToUnlock);
                }



                break;
        }

    }


    private void SetShowStar(int _star, Sprite _sprite)
    {
        int _total = LIST_IMAGE_STAR.Count;

        for (int i = 0; i < _total; i++)
        {
            if (i < _star)
            {
                LIST_IMAGE_STAR[i].sprite = _sprite;
            }
            else
                LIST_IMAGE_STAR[i].sprite = sprStar_empty;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //text
        txtLevelTitle.text = "LEVEL " + (TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1);
        //tips
        txtContentOfTips.text = TheDataManager.Instance.TIP_MANAGER.GetRandomTips();


        _currentStar_Normal = TheDataManager.THE_PLAYER_DATA.GetStar(TheDataManager.THE_PLAYER_DATA.iCurrentLevel, TheEnumManager.DIFFICUFT.Normal); ;
        _currentStar_Hard = TheDataManager.THE_PLAYER_DATA.GetStar(TheDataManager.THE_PLAYER_DATA.iCurrentLevel, TheEnumManager.DIFFICUFT.Hard);
        _currentStar_Nightmate = TheDataManager.THE_PLAYER_DATA.GetStar(TheDataManager.THE_PLAYER_DATA.iCurrentLevel, TheEnumManager.DIFFICUFT.Nightmate);

        //color for button-difficuft
        buMode_Normal.image.color = Color.white;

        if (_currentStar_Normal == 3)
            buMode_Hard.image.color = Color.white;
        else buMode_Hard.image.color = Color.gray;

        if (_currentStar_Hard == 3)
            buMore_Nightmate.image.color = Color.white;
        else buMore_Nightmate.image.color = Color.gray;

        //icon map
        imaIconMap.sprite = MainCode_LevelSelection.Instance.GetSpriteIconMap(TheDataManager.THE_PLAYER_DATA.iCurrentLevel);

        GameMode(TheEnumManager.DIFFICUFT.Normal);
    }
}
