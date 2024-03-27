using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;



public class TheDataManager : MonoBehaviour
{
    public static TheDataManager Instance;


    private string PATH_OF_PLAYER_DATA_XML;
    private bool TESING_MODE;
    public int iGemFormWatchingAds = 50;


    [Space(20)]
    public DATA_CONFIG.TipManager TIP_MANAGER;
    public DATA_CONFIG.TowerDataManager TOWER_DATA_MANAGER;
    public DATA_CONFIG.EnemyDataManager ENEMY_DATA_MANAGER;
    public DATA_CONFIG.UpgradeDataManager UPGRADE_DATA_MANAGER;


    [Space(20)]
    public DATA_CONFIG.SellingGempack SELLING_GEMPACK_MANAGER;
    public DATA_CONFIG.SellingCoinpack SELLING_COINPACK_MANAGER;
    public DATA_CONFIG.SellingTower SELLING_TOWER_MANAGER;
    public DATA_CONFIG.SellingSkill SELLING_SKILL_MANAGER;




    private void Awake()
    {

        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);


        //PATH
#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE || UNITY_WEBGL || UNITY_STANDALONE
        PATH_OF_PLAYER_DATA_XML = Application.persistentDataPath + "/PlayerData.xml";
#elif UNITY_EDITOR
        PATH_OF_PLAYER_DATA_XML = Application.dataPath + "/Resources/Data/PlayerData.xml";
#endif
    }


    private void Start()
    {
        // SerialzerPlayerData();
        THE_PLAYER_DATA = new ThePlayerData();
        if (!File.Exists(PATH_OF_PLAYER_DATA_XML))
        {
            SerialzerPlayerData();
        }
        else
        {
            THE_PLAYER_DATA = DeserialzerPlayerData();
        }


        //-----9/2019
        UPGRADE_DATA_MANAGER.Init();
        TOWER_DATA_MANAGER.Init();
    }



    #region SERIALIZER PLAYER DATA
    public static ThePlayerData THE_PLAYER_DATA;
    public void SerialzerPlayerData()
    {
        ThePlayerData _thePlayerData = new ThePlayerData();
        XmlSerializer serialzer = new XmlSerializer(typeof(ThePlayerData));
        StreamWriter writer = new StreamWriter(PATH_OF_PLAYER_DATA_XML);

        serialzer.Serialize(writer.BaseStream, THE_PLAYER_DATA);
        writer.Close();
        print("SERIALZER: SAVE DONE!!!!");

        //ma hoa
        string _data = File.ReadAllText(PATH_OF_PLAYER_DATA_XML);
        _data = TheEncryptionManager.EncryptData(_data);
        byte[] _byte = System.Text.Encoding.ASCII.GetBytes(_data);
        System.IO.File.WriteAllBytes(PATH_OF_PLAYER_DATA_XML, _byte);
    }

    public ThePlayerData DeserialzerPlayerData()
    {
        if (File.Exists(PATH_OF_PLAYER_DATA_XML))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ThePlayerData));
            // StreamReader reader = new StreamReader(PATH_OF_PLAYER_DATA_XML);
            //ThePlayerData deserialized = (ThePlayerData)serializer.Deserialize(reader.BaseStream);

            StringReader reader = new StringReader(TheEncryptionManager.DecryptData(File.ReadAllText(PATH_OF_PLAYER_DATA_XML)));

            ThePlayerData deserialized = (ThePlayerData)serializer.Deserialize(reader);
            reader.Close();
            Debug.Log("SERIALZER: GET PLAYER DATA DONE!");
            return deserialized;

        }
        else
        {
            Debug.Log("SERIALZER: NO FILE EXITS");
            return null;
        }
    }

    #endregion




    #region RESET GAME

    [ContextMenu("Reset game!")]
    public void ResetGame()
    {
        if (File.Exists(PATH_OF_PLAYER_DATA_XML))
        {

            File.Delete(PATH_OF_PLAYER_DATA_XML);
            THE_PLAYER_DATA = new ThePlayerData();
            SerialzerPlayerData();

            //tower
            Instance.UPGRADE_DATA_MANAGER.Reset();
            Instance.TOWER_DATA_MANAGER.Reset();


            ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Menu);
            Instance.UPGRADE_DATA_MANAGER.Init();
            Instance.TOWER_DATA_MANAGER.Init();
        }
    }

    #endregion


    //CONVER FLOAT FROM STRING
    private float ConvertStringToFloat(string _value)
    {
        // Debug.Log("STRING VALUE: " + _value);
        float _f;
        //float.TryParse(_value, out _f);              
        _f = float.Parse(_value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        //  Debug.Log("FLOAT VALUE: " + _f);
        return _f;
    }

    private void OnApplicationQuit()
    {
        SerialzerPlayerData();//save
    }
}


//Tower data manager
namespace DATA_CONFIG
{
    [System.Serializable]
    public class TowerDataManager : TheGeneric<TowerData>
    {
        public void Init()
        {
            foreach (var item in MY_LIST)
            {
                item.Init();
            }
        }
        public TowerData Get(TheEnumManager.TOWER _tower)
        {
            foreach (var item in MY_LIST)
            {
                if (item.eTower == _tower) return item;
            }
            return null;
        }

        public void Reset()
        {
            foreach (var item in MY_LIST)
            {
                item.Reset();
            }

        }
    }



    [System.Serializable]
    public class EnemyDataManager : TheGeneric<EnemiesData>
    {
        public EnemiesData Get(TheEnumManager.ENEMY _enemy)
        {
            foreach (var item in MY_LIST)
            {
                if (item.eEnemy == _enemy) return item;
            }
            return null;
        }
    }



    [System.Serializable]
    public class UpgradeDataManager : TheGeneric<UpgradeConfig>
    {
        public UpgradeConfig Get(TheEnumManager.UPGRADE _upgrade)
        {
            foreach (var item in MY_LIST)
            {
                if (item.eUpgrade == _upgrade) return item;
            }
            return null;
        }

        public void Init()
        {
            foreach (var item in MY_LIST)
            {
                item.Init();
            }
        }

        public int GetTotalStarWasUsed(TheEnumManager.STAR_TYPE eStarKind)
        {
            int _total = 0;

            foreach (var item in MY_LIST)
            {
                if (item.ACTIVED && item.eStarType == eStarKind)
                    _total += item.iStarPrice;
            }

            return _total;
        }

        public void Reset()
        {
            foreach (var item in MY_LIST)
            {
                item.Reset();
            }
        }
    }

    [System.Serializable]
    public class TipManager
    {
        [SerializeField] List<string> LIST_TIPS;

        public int iTotal
        {
            get { return LIST_TIPS.Count; }
        }

        public string GetRandomTips()
        {
            int _rand = Random.Range(0, LIST_TIPS.Count);
            return GetTips(_rand);
        }
        public string GetTips(int _index)
        {
            if (_index >= LIST_TIPS.Count) _index = LIST_TIPS.Count - 1;
            return LIST_TIPS[_index];
        }
    }



    //SELLING PRODUCT
    [System.Serializable]
    public class SellingGempack : TheGeneric<GempackData>
    {
        public GempackData Get(TheEnumManager.GEM_PACK _pack)
        {
            foreach (var item in MY_LIST)
            {
                if (item.ePack == _pack) return item;
            }
            return null;
        }
    }



    [System.Serializable]
    public class SellingCoinpack : TheGeneric<CoinpackData>
    {
        public CoinpackData Get(TheEnumManager.COIN_PACK _pack)
        {
            foreach (var item in MY_LIST)
            {
                if (item.eCoin == _pack) return item;
            }
            return null;
        }
    }



    [System.Serializable]
    public class SellingTower : TheGeneric<SellingTowerData>
    {
        public SellingTowerData Get(TheEnumManager.TOWER _tower)
        {
            foreach (var item in MY_LIST)
            {
                if (item.eTower == _tower) return item;
            }
            return null;
        }
    }



    [System.Serializable]
    public class SellingSkill : TheGeneric<SellingSkillData>
    {
        public SellingSkillData Get(TheEnumManager.POWER_UP _skill)
        {
            foreach (var item in MY_LIST)
            {
                if (item.eSkill == _skill) return item;
            }
            return null;
        }
    }
}
