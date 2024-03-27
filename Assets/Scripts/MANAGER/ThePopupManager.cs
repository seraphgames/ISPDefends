using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThePopupManager : MonoBehaviour
{
    public static ThePopupManager Instance;
    [SerializeField] GameObject TEXT_TEST_MODE;
    public MySceneManager SCENE_MANAGER = new MySceneManager();



    [SerializeField] Canvas m_CanvasOfPopup;
    [SerializeField] Camera m_BlackCamera;

    public enum POP_UP
    {
        Setting,
        Note,
        Gameover,
        Victory,
        Shop,
        Quit,
        AboutUs,
        Rate,
        CheckIn,
        Ready,
        RewardedVideo,
        Tutorial,
    }
    [SerializeField] List<PopUp> LIST_POPUP;



    //show
    public bool IsShowing
    {
        get
        {
            int _total = LIST_POPUP.Count;
            for (int i = 0; i < _total; i++)
            {

                if (LIST_POPUP[i].IS_SHOWING)
                    return true;

            }
            return false;
        }
    }
    public void Show(POP_UP epopup)
    {
        if (epopup == POP_UP.Gameover) TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_defeat);//sound
        if (epopup == POP_UP.Victory)
        {
            // TheMusic.Instance.Stop();
            // TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_winning);//sound
        }
        else TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound

        foreach (var item in LIST_POPUP)
        {
            if (item.ePOPUP == epopup && !item.IS_SHOWING)
            {
                item.Active(true);
                item.transform.SetAsLastSibling();
                TheEventManager.PostGameEvent_OnOpenUIPopup();//event
                return;
            }

        }
    }
    public void Show(POP_UP epopup, float _timeDelay)
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
        StartCoroutine(IeShow(epopup, _timeDelay));
    }

    public void Hide(POP_UP epopup)
    { //sound
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);//sound

        foreach (var item in LIST_POPUP)
        {
            if (item.ePOPUP == epopup)
            {
                if (item.IS_SHOWING)
                {
                    item.Active(false);
                    TheEventManager.PostGameEvent_OnCloseUIPopup();//event
                    return;
                }
            }

        }
    }
    public void HideAllPopup()
    {
        foreach (var item in LIST_POPUP)
        {
            if (item.IS_SHOWING)
            {
                item.Active(false);
            }

        }
    }

    public PopUp Get(POP_UP epopup)
    {
        foreach (var item in LIST_POPUP)
        {
            if (item.ePOPUP == epopup)
            {
                return item;
            }
        }
        return null;
    }
    private IEnumerator IeShow(POP_UP epopup, float _timeDelay)
    {
        yield return new WaitForSecondsRealtime(_timeDelay);

        Show(epopup);
    }



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        TEXT_TEST_MODE.SetActive(false);
        Invoke("ShowTextTestMode", 1.0f);
    }


    //emain
    public void SentBugEmail()
    {
        string version = Application.version;
        string email = ThePlatformManager.Instance.GAME_INFO.strEmailReport;
        string subject = "[TheLastRealm -v" + version + "] Bug Report";
        string body = "Hi! \n Continue your email...";
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
     
    }
    public void SentContactEmail()
    {
        string version = Application.version;
        string email = ThePlatformManager.Instance.GAME_INFO.strEmailUser;
        string subject = "[TheLastRealm -v" + version + "] Contact";
        string body = "Hi! \n Continue your email...";
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
    public void OpenLink(string _link)
    {
       
        Application.OpenURL(_link);
    }

    //Load Leaderboard
    public void OpenLeaderboardUI()
    {
        
      
    }
    //Load Achievement
    public void OpenAchievementUI()
    {
        
    }
    private void ShowTextTestMode()
    {
        TEXT_TEST_MODE.SetActive(TheDataManager.THE_PLAYER_DATA.TESTING_MODE);
    }









    //Set camera for canvas of  popup
    private bool bHadBlackCamera = false;
    public void SetCameraForPopupCanvas(Camera _cam)
    {
        m_CanvasOfPopup.renderMode = RenderMode.ScreenSpaceCamera;
        m_CanvasOfPopup.worldCamera = _cam;
        m_CanvasOfPopup.sortingOrder = 200;

        float _screenWidth = Screen.width;
        float _screenHeight = Screen.height;
        float ratioOfScreen = _screenWidth / _screenHeight;
        float targetRotio = 0;


        if (ratioOfScreen > (16f / 9f)) //Ty le 21:9
        {
            targetRotio = ((_screenHeight * 16) / (9)) / _screenWidth;
            _cam.rect = new Rect((1 - targetRotio) / 2.0f, 0, targetRotio, 1);

            CreateBackgroundCamera();

        }
        else if (ratioOfScreen < (16 / 9f)) // ty le 4:3
        {
            targetRotio = ((_screenWidth * 9) / (16)) / _screenHeight;
            _cam.rect = new Rect(0, (1 - targetRotio) / 2.0f, 1, targetRotio);

            CreateBackgroundCamera();

        }
        else // ty le 16:9
        {
            _cam.rect = new Rect(0, 0, 1, 1);
        }

    }

    private void CreateBackgroundCamera()
    {
        if (!bHadBlackCamera)
        {
            bHadBlackCamera = true;
            Instantiate(m_BlackCamera);
        }
    }


    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Show(POP_UP.Setting);
    }
}


public class MySceneManager
{

    public TheEnumManager.SCENE CURRENT_SCENE;


    //LOAD SCENE
    public void LoadScene(TheEnumManager.SCENE eScene, bool _isback = false)
    {
        //sound
        if (_isback) TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);//sound
        else TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound

        //Eff
        Loading.Instance.PlayLoading(true);
        Time.timeScale = 1;
        ThePopupManager.Instance.StartCoroutine(IeLoadScene(eScene));

    }

    private IEnumerator IeLoadScene(TheEnumManager.SCENE eScene)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(eScene.ToString());
        yield return new WaitForSecondsRealtime(0.01f);
        ThePopupManager.Instance.HideAllPopup();
        yield return new WaitForSecondsRealtime(0.05f);
        //Eff
        Loading.Instance.PlayLoading(false);
        CURRENT_SCENE = eScene;
    }


    public bool isLoadingScene(TheEnumManager.SCENE eScene)
    {
        return SceneManager.GetActiveScene().name == eScene.ToString();
    }



}
