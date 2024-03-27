using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : PopUp
{
    public Button buFreeGem;
    public GameObject objBoardCoin;
    public static Shop Instane;
    public enum PANEL
    {
        Gems,
        PowerUps,
        Coins,
        Towers,
    }



    [Space(20)]
    public SHOP.PanelManager PANEL_MANAGER;
    public SHOP.GempackUIManager GEMPACK_UI_MANAGER;
    public SHOP.PowerUpUIManager POWERUP_UI_MANAGER;
    public SHOP.CoinpackUIManager COINPACK_UI_MANAGER;
    public SHOP.TowerUIManager TOWER_UI_MANAGER;






    private void Awake()
    {
        if (Instane == null)
            Instane = this;

        PANEL_MANAGER.Init();
        GEMPACK_UI_MANAGER.Init();
        POWERUP_UI_MANAGER.Init();
        COINPACK_UI_MANAGER.Init();
        TOWER_UI_MANAGER.Init();
    }




    public void ShowPanel(PANEL _panel)
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound


        foreach (var item in PANEL_MANAGER.MY_LIST)
        {
            if (item.ePanel == _panel) item.Active(true);
            else item.Active(false);
        }

        if (_panel == PANEL.Coins)
        {
            buFreeGem.gameObject.SetActive(false);
            objBoardCoin.SetActive(true);
        }
        else
        {
            buFreeGem.gameObject.SetActive(true);
            objBoardCoin.SetActive(false);
        }
    }


    protected override void OnEnable()
    {
        base.OnEnable();

        //Ads
        /*if (!TheAdsManager.Instance.isReadyRewardedVideoAd())
        {
            buFreeGem.image.color = Color.gray;
        }
        else
        {
            buFreeGem.image.color = Color.white;
        }*/

        TheEventManager.PostGameEvent_OnUpdateBoardInfo();//event
        PANEL_MANAGER.Get((int)PANEL.Coins).ShowCallButton(ThePopupManager.Instance.SCENE_MANAGER.CURRENT_SCENE == TheEnumManager.SCENE.Gameplay);

        POWERUP_UI_MANAGER.OnEnable();
        TOWER_UI_MANAGER.OnEnable();
    }


    protected override void OnDisable()
    {
        base.OnDisable();

        TOWER_UI_MANAGER.OnDisable();
    }
}

namespace SHOP
{
    [System.Serializable]
    public class PanelElement
    {
        public Shop.PANEL ePanel;
        [SerializeField] Button buButton;
        [SerializeField] GameObject objPanel;


        public void Init()
        {
            buButton.onClick.AddListener(() => Shop.Instane.ShowPanel(ePanel));
        }

        public void Active(bool _active)
        {
            objPanel.SetActive(_active);
            if (_active) buButton.image.color = Color.white;
            else buButton.image.color = new Color(0.8f, 0.8f, 0.8f, 1f);
        }

        public void ShowCallButton(bool _active)
        {
            buButton.gameObject.SetActive(_active);
        }

    }


    [System.Serializable]
    public class GemTrackUI
    {
        [SerializeField] TheEnumManager.GEM_PACK eGemPack;
        private Text txtName;
        private Text txtValueToAdd;
        private Text txtPrice;
        private Button buBuy;
        [SerializeField] private Transform tranTrack;
        private GempackData thisGemPackData;

        public void Init()
        {
            /*txtName = tranTrack.Find("Text_Name").GetComponent<Text>();
            txtValueToAdd = tranTrack.Find("Text_Gem").GetComponent<Text>();
            buBuy = tranTrack.Find("ButtonBuy").GetComponent<Button>();
            txtPrice = buBuy.GetComponentInChildren<Text>();

            thisGemPackData = TheDataManager.Instance.SELLING_GEMPACK_MANAGER.Get(eGemPack);

            txtName.text = thisGemPackData.strName;
            txtValueToAdd.text = thisGemPackData.iValueToAdd.ToString();

            try
            {
                string _localPrice = InAppPurchaseManager.Instance.m_StoreController.products
                    .WithID(thisGemPackData.strKeyStoreId).metadata.localizedPriceString;
                txtPrice.text = _localPrice;
            }
            catch
            {
                txtPrice.text = thisGemPackData.fPrice.ToString() + " $";
            }

            buBuy.onClick.AddListener(() => InAppPurchaseManager.Instance.BuyProductID(
                thisGemPackData.strKeyStoreId));*/
        }
    }


    [System.Serializable]
    public class PowerUpTrackUI
    {
        public TheEnumManager.POWER_UP eSkill;
        [SerializeField] Transform tranTrack;
        private Text txtName;
        private Text txtCurrentValue;
        private Text txtContent;
        private Button buBuy;
        private Text txtValueToAdd;
        private Text txtPrice;
        private SellingSkillData thisData;

        public void Init()
        {
            thisData = TheDataManager.Instance.SELLING_SKILL_MANAGER.Get(eSkill);

            txtName = tranTrack.Find("Text_Name").GetComponent<Text>();
            txtCurrentValue = tranTrack.Find("Text_Number").GetComponent<Text>();
            txtContent = tranTrack.Find("Text_Content").GetComponent<Text>();
            buBuy = tranTrack.Find("ButtonBuy").GetComponent<Button>();
            txtValueToAdd = tranTrack.Find("Text_NumberToAdd").GetComponent<Text>();
            txtPrice = buBuy.GetComponentInChildren<Text>();


            buBuy.onClick.AddListener(() => Buy());
            UpdateUIStatus();

        }

        public void UpdateUIStatus()
        {
            txtName.text = thisData.strName;
            txtCurrentValue.text = TheDataManager.THE_PLAYER_DATA.GetNumberOfSkill(eSkill).ToString();
            txtContent.text = thisData.strContent;
            txtValueToAdd.text = "+" + thisData.iValueToAdd;
            txtPrice.text = thisData.fPriceGem.ToString();
        }



        private void Buy()
        {
            if (TheDataManager.THE_PLAYER_DATA.GEM < thisData.fPriceGem)
            {
                ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
                Note.Instance.ShowPopupNote(Note.NOTE.NotEnoughtGem);
                return;
            }

            TheDataManager.THE_PLAYER_DATA.GEM -= (int)thisData.fPriceGem;


            thisData.Get();
            UpdateUIStatus();
            TheEventManager.PostGameEvent_OnUpdateBoardInfo();

            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_buy_something);//sound

        }


    }


    [System.Serializable]
    public class CoinTrackUI
    {
        [SerializeField] TheEnumManager.COIN_PACK eCoinPack;
        private Text txtName;
        private Text txtValueToAdd;
        private Text txtPrice;
        private Button buBuy;
        [SerializeField] private Transform tranTrack;
        private CoinpackData thisCoinPackData;

        public void Init()
        {
            txtName = tranTrack.Find("Text_Name").GetComponent<Text>();
            txtValueToAdd = tranTrack.Find("Text_Coin").GetComponent<Text>();
            buBuy = tranTrack.Find("ButtonBuy").GetComponent<Button>();
            txtPrice = buBuy.GetComponentInChildren<Text>();

            thisCoinPackData = TheDataManager.Instance.SELLING_COINPACK_MANAGER.Get(eCoinPack);

            txtName.text = thisCoinPackData.strName;
            txtValueToAdd.text = thisCoinPackData.iValueToAdd.ToString();


            txtPrice.text = thisCoinPackData.fPriceGem.ToString();
            buBuy.onClick.AddListener(() => Buy());
        }

        private void Buy()
        {
            if (TheDataManager.THE_PLAYER_DATA.GEM < thisCoinPackData.fPriceGem)
            {
                ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
                Note.Instance.ShowPopupNote(Note.NOTE.NotEnoughtGem);
                return;
            }

            TheDataManager.THE_PLAYER_DATA.GEM -= thisCoinPackData.fPriceGem;
            thisCoinPackData.Get();
        }

    }


    [System.Serializable]
    public class TowerTrackUI
    {
        [SerializeField] TheEnumManager.TOWER eTower;
        private Text txtName;
        private Text txtContent;
        private Text txtPrice;
        [SerializeField] private GameObject imaReadyMask;
        private Button buBuy, buContent;
        [SerializeField] private Transform tranTrack;
        private SellingTowerData thisSellingTowerData;
        private TowerData thisTowerData;

        public void Init()
        {
            txtName = tranTrack.Find("Text_Name").GetComponent<Text>();
            txtContent = tranTrack.Find("Text_Content").GetComponent<Text>();
            // imaReadyMask = tranTrack.Find("Ready").gameObject;
            buContent = tranTrack.Find("Button_ShowInfo").GetComponent<Button>();
            buBuy = tranTrack.Find("ButtonBuy").GetComponent<Button>();
            txtPrice = buBuy.GetComponentInChildren<Text>();

            thisSellingTowerData = TheDataManager.Instance.SELLING_TOWER_MANAGER.Get(eTower);
            thisTowerData = TheDataManager.Instance.TOWER_DATA_MANAGER.Get(eTower);

            txtName.text = thisSellingTowerData.strName;
            txtContent.text = "UNLOCK AT LEVEL: " + thisTowerData.iLevelToUnlock.ToString();


            buBuy.onClick.AddListener(() => Buy());
            buContent.onClick.AddListener(() => ShowTowerInfo());

            UpdateUIState();
        }




        public void UpdateUIState()
        {
            imaReadyMask.SetActive(thisTowerData.IsUnlocked);
        }




        private void Buy()
        {
            if (thisTowerData == null) Debug.Log("thisTowerData==null");
            if (thisTowerData.IsUnlocked)
            {

                ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
                Note.Instance.ShowPopupNote(Note.NOTE.TowerIsReady);//
                return;
            }

            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound


            if (thisSellingTowerData == null) Debug.Log("thisSellingTowerData==null");
            Shop.Instane.TOWER_UI_MANAGER.BUYING_POPUP.SetInfo(thisSellingTowerData);

        }


        private void ShowTowerInfo()
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
            Note.Instance.ShowPopupNote(thisSellingTowerData.GetInfo());
        }

    }




    //Manager
    [System.Serializable]
    public class PanelManager : TheGeneric<PanelElement>
    {
        public void Init()
        {
            foreach (var item in MY_LIST)
            {
                item.Init();
            }
        }
    }
    [System.Serializable]
    public class GempackUIManager : TheGeneric<GemTrackUI>
    {
        public void Init()
        {
            foreach (var item in MY_LIST)
            {
                item.Init();
            }
        }
    }
    [System.Serializable]
    public class PowerUpUIManager : TheGeneric<PowerUpTrackUI>
    {
        public void Init()
        {
            foreach (var item in MY_LIST)
            {
                item.Init();
            }
        }

        public void OnEnable()
        {
            foreach (var item in MY_LIST)
            {
                item.UpdateUIStatus();
            }
        }
    }

    [System.Serializable]
    public class CoinpackUIManager : TheGeneric<CoinTrackUI>
    {
        public void Init()
        {
            foreach (var item in MY_LIST)
            {
                item.Init();
            }
        }
    }

    [System.Serializable]
    public class TowerUIManager : TheGeneric<TowerTrackUI>
    {

        [Space(20)]
        public BuyingPopup BUYING_POPUP = new BuyingPopup();

        public void Init()
        {
            BUYING_POPUP.Init();
            foreach (var item in MY_LIST)
            {
                item.Init();
            }


        }


        [System.Serializable]
        public class BuyingPopup
        {
            [SerializeField] GameObject objBoardToBuy;
            [SerializeField] Button buBuyWithCash, buBuyWithGem, buClose;
            private SellingTowerData CHOOSING_TOWER;

            public void Init()
            {
                CHOOSING_TOWER = new SellingTowerData();
                buBuyWithCash.onClick.AddListener(() => SetButton(buBuyWithCash));
                buBuyWithGem.onClick.AddListener(() => SetButton(buBuyWithGem));
                buClose.onClick.AddListener(() => SetButton(buClose));
            }
            private void SetButton(Button _bu)
            {
                if (_bu == buClose)
                {
                    TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);//
                    Active(false);

                }
                else if (_bu == buBuyWithCash)
                {

                   // InAppPurchaseManager.Instance.BuyProductID(CHOOSING_TOWER.strKeyStoreId);

                }
                else if (_bu == buBuyWithGem)
                {
                    Active(false);

                    if (TheDataManager.THE_PLAYER_DATA.GEM < CHOOSING_TOWER.fPriceGem)
                    {
                        ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Note);
                        Note.Instance.ShowPopupNote(Note.NOTE.NotEnoughtGem);
                        return;
                    }

                    TheDataManager.THE_PLAYER_DATA.GEM -= CHOOSING_TOWER.fPriceGem;
                    CHOOSING_TOWER.Get();


                    TheDataManager.Instance.SerialzerPlayerData();
                    TheEventManager.PostGameEvent_OnUpdateBoardInfo();

                }
            }


            public void SetInfo(SellingTowerData _data)
            {
                Debug.Log("HERE 1");
                CHOOSING_TOWER = _data;
                Debug.Log("HERE 2");

                //show price text 
                buBuyWithGem.GetComponentInChildren<Text>().text = CHOOSING_TOWER.fPriceGem.ToString();

                Debug.Log("HERE 3");
                //price
                string _localString = "";

               /* try
                {
                    _localString = InAppPurchaseManager.Instance.m_StoreController.products
                    .WithID(CHOOSING_TOWER.strKeyStoreId).metadata.localizedPriceString;
                }
                catch { }*/

                if (_localString != "")
                {
                    Debug.Log("_localString: " + _localString);
                    buBuyWithCash.GetComponentInChildren<Text>().text = _localString;
                }
                else
                {
                    Debug.Log("HERE 5");
                    buBuyWithCash.GetComponentInChildren<Text>().text = CHOOSING_TOWER.fPriceCash.ToString();
                }


                Active(true);
            }

            public void Active(bool _active)
            {
                objBoardToBuy.SetActive(_active);
            }

        }


        private void HandleUnlockTower()
        {
            BUYING_POPUP.Active(false);
            foreach (var item in MY_LIST)
            {
                item.UpdateUIState();
            }
        }


        public void OnEnable()
        {
            TheEventManager.OnUnlockTower += HandleUnlockTower;
        }
        public void OnDisable()
        {
            TheEventManager.OnUnlockTower -= HandleUnlockTower;
        }


    }
}
