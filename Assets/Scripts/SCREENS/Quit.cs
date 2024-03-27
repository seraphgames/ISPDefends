using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quit : PopUp
{
    public Button buOk;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        buOk.onClick.AddListener(() => StartCoroutine(IeQuit()));

    }

    private IEnumerator IeQuit()
    {
        //ads
        /*try
        {
            TheAdsManager.Instance.ShowFullAds();//Ads
            
        }
        catch { }*/


        yield return new WaitForSecondsRealtime(0.5f);
        Application.Quit();
    }


}
