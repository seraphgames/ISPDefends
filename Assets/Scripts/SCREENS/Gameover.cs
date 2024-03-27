using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameover : PopUp
{
    public Button buReplay, buNext;
    // Use this for initialization


    protected override void Start()
    {
        base.Start();
        buReplay.onClick.AddListener(() => ButtonReplay());
        buNext.onClick.AddListener(() => ButtonNext());
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // TheAdsManager.Instance.ShowFullAds();//admob ads

        //Data Analytics
        TheEventManager.EventGameDefeat();
    }

    private void ButtonReplay()
    {
        /*try
        {
            TheAdsManager.Instance.ShowFullAds();// ads
        }
        catch { }

        ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Gameplay);
        */
    }


    private void ButtonNext()
    {
        //TheAdsManager.Instance.ShowFullAds();// ads
        //ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.LevelSelection);
    }

}
