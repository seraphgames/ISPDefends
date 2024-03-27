using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victory : PopUp
{
    public GameObject EFF_STAR_FALL;

    public Sprite sprStar_Normal, sprStar_Hard, sprStar_Nightmate, sprStar_Empty;
    private Sprite sprCurrentStar;


    public Button buContinue, buReplay;
    public Text txtGemGift;
    public List<Image> LIST_STAR;
    int iStar = 0;
    int iGemVitory = 0;
    protected override void Start()
    {
        base.Start();
        buContinue.onClick.AddListener(() => ButtonContinue());
        buReplay.onClick.AddListener(() => ButtonReplay());

    }


    protected override void OnEnable()
    {
        base.OnEnable();

        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.attack_magic); //sound 
        iStar = GetStar();
        iGemVitory = GetGiftGem(iStar);

        if (!TheDataManager.THE_PLAYER_DATA.TESTING_MODE)
        {
            //Save star
            TheDataManager.THE_PLAYER_DATA.SetStar(TheDataManager.THE_PLAYER_DATA.iCurrentLevel, iStar, TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT);
            switch (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT)
            {
                case TheEnumManager.DIFFICUFT.Normal:
                    sprCurrentStar = sprStar_Normal;
                    break;
                case TheEnumManager.DIFFICUFT.Hard:
                    sprCurrentStar = sprStar_Hard;
                    break;
                case TheEnumManager.DIFFICUFT.Nightmate:
                    sprCurrentStar = sprStar_Nightmate;
                    break;
            }

            Debug.Log(TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT.ToString() + ": " + TheDataManager.THE_PLAYER_DATA.iCurrentLevel);

            TheDataManager.THE_PLAYER_DATA.GEM += iGemVitory;//gem colection
            TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event


        }

        //star sprite
        StartCoroutine(AnimationStar(1.5f));


        //rate
        if (TheDataManager.THE_PLAYER_DATA.iCurrentLevel % 4 == 0 && TheDataManager.THE_PLAYER_DATA.iCurrentLevel > 1)
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Rate, 1f);

        //Data Analytics
        TheEventManager.EventGameWinning(iStar);
    }


    //Button Continue
    private void ButtonContinue()
    {

       /* try
        {
            TheAdsManager.Instance.ShowFullAds();// ads
        }
        catch { }*/

        if (TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1 == ThePlatformManager.Instance.TOTAL_LEVEL_IN_GAME)
        {
            ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.EndGame);
        }
        else
        {
            ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.LevelSelection);
        }
    }


    private void ButtonReplay()
    {
       /* try
        {
            TheAdsManager.Instance.ShowFullAds();// ads
        }
        catch { }*/
        ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Gameplay);
    }



    //Cal star
    private int GetStar()
    {
        float _fHeart = TheLevel.Instance.iCurrentHeart * 1.0f / TheLevel.Instance.iOriginalHeart;
        if (_fHeart > 0.8f) return 3;
        else if (_fHeart < 0.5f) return 1;
        else return 2;
    }


    //ANIMATION STARS
    private IEnumerator AnimationStar(float _time)
    {
        int _total = LIST_STAR.Count;
        for (int i = 0; i < _total; i++)
        {

            LIST_STAR[i].sprite = sprStar_Empty;
            LIST_STAR[i].color = Color.white * 0.3f;
        }
        txtGemGift.text = "...";
        yield return new WaitForSecondsRealtime(_time);

        for (int i = 0; i < _total; i++)
        {
            if (i < iStar)
            {
                if (i == 0) TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_star_1);
                else if (i == 1) TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_star_2);
                else if (i == 2) TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_star_3);

                LIST_STAR[i].color = Color.white;
                LIST_STAR[i].sprite = sprCurrentStar;
                yield return new WaitForSecondsRealtime(0.5f);
            }
        }


        yield return new WaitForSecondsRealtime(0.8f);
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_coin);//sound
        txtGemGift.text = "+" + iGemVitory.ToString();
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_winning);///sound
        TheMusic.Instance.Stop();
        Instantiate(EFF_STAR_FALL);//EFF
    }


    //GEM IS GIFT
    private int GetGiftGem(int _star)
    {
        int _gem = 0;
        switch (_star)
        {
            case 1:
                _gem = Random.Range(30, 40);
                break;
            case 2:
                _gem = Random.Range(40, 50);
                break;
            case 3:
                _gem = Random.Range(50, 80);
                break;
        }



        switch (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT)
        {
            case TheEnumManager.DIFFICUFT.Normal:
                _gem = (int)(_gem * 1.0f);
                break;
            case TheEnumManager.DIFFICUFT.Hard:
                _gem = (int)(_gem * 1.3f);
                break;
            case TheEnumManager.DIFFICUFT.Nightmate:
                _gem = (int)(_gem * 1.5f);
                break;

        }

        return _gem;
    }


    protected override void OnDisable()
    {
        base.OnDisable();


        TheMusic.Instance.Play();//play sound
    }
}
