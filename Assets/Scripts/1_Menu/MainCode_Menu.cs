using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainCode_Menu : MonoBehaviour
{

    public static bool START_GAME = false;


    [Space(20)]
    public Text txtPlayerName;
    public Button buQuit;
    public Button buSetting, buMoreGame, buStart, buArchivement, buLeaderboard;
    public string[] GRAPHISC;

    // Use this for initialization
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        txtPlayerName.text = "";
    }

    void Start()
    {
        //set camera for popup canvas
        ThePopupManager.Instance.SetCameraForPopupCanvas(GameObject.Find("Main Camera").GetComponent<Camera>());

        buQuit.onClick.AddListener(() => ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Quit));

        buSetting.onClick.AddListener(() => ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Setting));
        buMoreGame.onClick.AddListener(() => ThePopupManager.Instance.OpenLink(ThePlatformManager.Instance.GAME_INFO.strLinkMoreGame));


        buStart.onClick.AddListener(() => ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.LevelSelection));

        buArchivement.onClick.AddListener(() => ThePopupManager.Instance.OpenAchievementUI());

        buLeaderboard.onClick.AddListener(() => ThePopupManager.Instance.OpenLeaderboardUI());

        //Eff
        Loading.Instance.PlayLoading(false);

        StartCoroutine(CheckIn());

#if UNITY_IOS || UNITY_IPHONE
        buArchivement.gameObject.SetActive(false);
        buLeaderboard.gameObject.SetActive(false);
#elif UNITY_ANDROID || UNITY_EDITOR
       

        buArchivement.gameObject.SetActive(false);
        buLeaderboard.gameObject.SetActive(false);
#endif

    }



    private IEnumerator CheckIn()
    {
        yield return new WaitUntil(() => ThePopupManager.Instance != null);
        yield return new WaitUntil(() => TheCheckInGiftManager.Instance != null);
        yield return new WaitForSecondsRealtime(1.0f);


        if (ThePopupManager.Instance.Get(ThePopupManager.POP_UP.CheckIn)
            .GetComponent<CheckIn>().CheckToShowCheckInPopup())
        {
            if (TheDataManager.THE_PLAYER_DATA.iNumberOfGiftsReceived < TheCheckInGiftManager.Instance.LIST_GIFT.Count) // kiểm tra xem đã nhận hết quà chưa!
                ThePopupManager.Instance.Show(ThePopupManager.POP_UP.CheckIn);
        }
    }



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Quit);
        }
    }

    private void OnEnable()
    {
        //TheAdsManager.Instance.ShowInterstitial();//admob ads
    }


    [ContextMenu("Reset all Player data")]
    public void DoSomething()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("DONE");
    }





}
