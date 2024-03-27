using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheButtonTower : MonoBehaviour
{
    public static TheButtonTower Instance;


    [System.Serializable]
    public class ClassBuyTowerUI
    {
        public Button buButtonBuyTower;
        public TheEnumManager.TOWER eTowerKind;
        private TowerData TOWER_DATA;

        //public GameObject objBackgroundColliderForUI;

        public void Init()
        {
            TOWER_DATA = TheDataManager.Instance.TOWER_DATA_MANAGER.Get(eTowerKind);
            TOWER_DATA.SetUnlockWithLevel(TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1);
            buButtonBuyTower.onClick.AddListener(() => BuyTower());
            ShowInfo();

        }

        public void ShowInfo()
        {
            if (!TOWER_DATA.bUNLOCK) // chua unlock
            {
                buButtonBuyTower.image.color = Color.gray;
                buButtonBuyTower.GetComponentInChildren<Text>().text = "";
                buButtonBuyTower.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            }
            else
            {
                buButtonBuyTower.image.color = Color.white;
                buButtonBuyTower.GetComponentInChildren<Text>().text = TOWER_DATA.GetPriceToBuild().ToString();
                buButtonBuyTower.transform.GetChild(1).GetComponent<Image>().color = Color.white * 0.0f;
            }
        }

        public void BuyTower()
        {
            if (!TheDataManager.THE_PLAYER_DATA.CheckUnlockTower(eTowerKind))
            {
                TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);//sound
                return;
            }


            if (TheLevel.Instance.iOriginalCoin >= TOWER_DATA.GetPriceToBuild())
            {
                TheLevel.Instance.iOriginalCoin -= TOWER_DATA.GetPriceToBuild();

                TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event


                GameObject _tower = TheObjPoolingManager.Instance.GetTowerPrefab(eTowerKind);
                _tower = Instantiate(_tower, OBJ_BUILD_POS.transform.position, Quaternion.identity);

                //eff smock
                GameObject _eff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Smock).GetItem();
                if (_eff)
                {
                    _eff.transform.position = _tower.transform.position;
                    _eff.SetActive(true);
                }
                Destroy(OBJ_BUILD_POS.gameObject);

                TheButtonTower.Instance.ShowBoard(Board.HideAll);
                BACKGROUND_COLLIDER.SetActive(true);
                ARROW.transform.position = Vector2.one * 1000;

                //sound
                TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.tower_build);
                TheEventManager.PostGameEvent_OnUpdateBoardInfo();
            }
            else
            {
                ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
                Note.Instance.ShowPopupNote(Note.NOTE.NotEnoughtCoin);
            }

        }

        public void CheckEnoughtCoinToBuy()
        {
            if (TheLevel.Instance.iOriginalCoin >= TOWER_DATA.GetPriceToBuild())
                buButtonBuyTower.image.color = Color.white;
            else buButtonBuyTower.image.color = Color.gray;
        }


    }
    public List<ClassBuyTowerUI> LIST_BUY_TOWER_UI;
    private void InitBuyTowerUi()
    {
        int _total = LIST_BUY_TOWER_UI.Count;
        for (int i = 0; i < _total; i++)
        {
            LIST_BUY_TOWER_UI[i].Init();
        }
    }




    public enum Properties
    {
        Upgrade,
        Sell,
    }


    public enum Board
    {
        BuyTower,
        UpgradeOfSell,
        HideAll,
    }


    public GameObject AREA_NOT_ALLOW_PLAYER_INPUT;
    [Space(20)]
    public Button buUpgradeTower, buSellTower;

    public GameObject objBoardBuyTower, objBoardUpradeTower;
    public GameObject objBackgroundColliderForUI;


    private static GameObject BACKGROUND_COLLIDER, BOARD_BUY_TOWER, ARROW;
    private static Tower SELECTED_TOWER;
    private static GameObject OBJ_BUILD_POS;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start()
    {
        BOARD_BUY_TOWER = objBoardBuyTower;
        ARROW = MainCode_Gameplay.Instance.m_BoardMark;
        BACKGROUND_COLLIDER = objBackgroundColliderForUI;

        //buying tower ui
        InitBuyTowerUi();


        buUpgradeTower.onClick.AddListener(() => UpgradeTower(Properties.Upgrade));
        buSellTower.onClick.AddListener(() => UpgradeTower(Properties.Sell));


        ShowBoard(TheButtonTower.Board.HideAll);
        AREA_NOT_ALLOW_PLAYER_INPUT.SetActive(false);
    }


    public void ShowBoard(Board _board)
    {
        switch (_board)
        {
            case Board.BuyTower:

                AREA_NOT_ALLOW_PLAYER_INPUT.SetActive(true);
                objBoardBuyTower.SetActive(true);
                objBoardUpradeTower.SetActive(false);
                objBackgroundColliderForUI.gameObject.SetActive(false);
                break;


            case Board.UpgradeOfSell:


                AREA_NOT_ALLOW_PLAYER_INPUT.SetActive(true);
                objBoardBuyTower.SetActive(false);
                objBoardUpradeTower.SetActive(true);
                objBackgroundColliderForUI.gameObject.SetActive(false);

                //show price
                buUpgradeTower.GetComponentInChildren<Text>().text = SELECTED_TOWER.TOWER_DATA.GetPriceToUpgrade(SELECTED_TOWER.eTowerLevel).ToString();
                buSellTower.GetComponentInChildren<Text>().text = SELECTED_TOWER.TOWER_DATA.GetPriceToSell(SELECTED_TOWER.eTowerLevel).ToString();


                //info
                if (SELECTED_TOWER.eTowerLevel == TheEnumManager.TOWER_LEVEL.level_4)
                {
                    buUpgradeTower.image.color = Color.gray;
                    buUpgradeTower.GetComponentInChildren<Text>().text = "";
                }
                else buUpgradeTower.image.color = Color.white;


                break;
            case Board.HideAll:

                AREA_NOT_ALLOW_PLAYER_INPUT.SetActive(false);
                objBoardBuyTower.SetActive(false);
                objBoardUpradeTower.SetActive(false);
                objBackgroundColliderForUI.gameObject.SetActive(true);
                MainCode_Gameplay.Instance.m_BoardMark.transform.position = Vector2.one * 1000;
                break;

        }
    }


    public bool IsShowing()
    {
        if (objBoardBuyTower.activeInHierarchy || objBoardUpradeTower.activeInHierarchy) return true;
        else return false;
    }




    public void SetSelectedTower(Tower _tower)
    {
        SELECTED_TOWER = _tower;
    }



    public void SetTowerPosToBuild(GameObject _objBuildPos)
    {
        OBJ_BUILD_POS = _objBuildPos;
    }


    private void UpgradeTower(Properties _properties)
    {
        switch (_properties)
        {
            case Properties.Upgrade:
                if (SELECTED_TOWER.eTowerLevel != TheEnumManager.TOWER_LEVEL.level_4)
                { 
                    //eff smock
                    GameObject _eff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Smock).GetItem();
                    if (_eff)
                    {
                        _eff.transform.position = SELECTED_TOWER.transform.position;
                        _eff.SetActive(true);
                    }

                    SELECTED_TOWER.UpgradeTower();
                    //price
                    buUpgradeTower.GetComponentInChildren<Text>().text
                        = SELECTED_TOWER.TOWER_DATA.GetPriceToUpgrade(SELECTED_TOWER.eTowerLevel).ToString();
                    buSellTower.GetComponentInChildren<Text>().text
                       = SELECTED_TOWER.TOWER_DATA.GetPriceToSell(SELECTED_TOWER.eTowerLevel).ToString();

                }

                //Color
                if (SELECTED_TOWER.eTowerLevel == TheEnumManager.TOWER_LEVEL.level_4)
                {
                    buUpgradeTower.image.color = Color.gray;
                    buUpgradeTower.GetComponentInChildren<Text>().text = "";
                }
                else
                    buUpgradeTower.image.color = Color.white;

                //info
                MainCode_Gameplay.Instance.m_boardInfoTower.ShowContent(SELECTED_TOWER);
                break;
            case Properties.Sell:
                Instantiate(TheObjPoolingManager.Instance.GetTowerPosObj(TheLevel.Instance.eKindOfMap), SELECTED_TOWER.transform.position, Quaternion.identity);
                SELECTED_TOWER.SellTower();
                TheButtonTower.Instance.ShowBoard(Board.HideAll);//hide board: upgrade and sell
                MainCode_Gameplay.Instance.m_boardInfoTower.Hide();//
                break;

        }
    }


    //-------------------------
    public void UpdateBuyTowerUiInfo()
    {
        foreach (var item in LIST_BUY_TOWER_UI)
        {
            item.ShowInfo();
        }
    }


    private void OnEnable()
    {     
        TheEventManager.OnUpdateBoardInfo += UpdateBuyTowerUiInfo;
       // UpdateBuyTowerUiInfo();
    }

    private void OnDisable()
    {       
        TheEventManager.OnUpdateBoardInfo -= UpdateBuyTowerUiInfo;
    }
}
