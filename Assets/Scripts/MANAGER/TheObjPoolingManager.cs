using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheObjPoolingManager : MonoBehaviour
{
    public static TheObjPoolingManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        int _total = LIST_OBJ_POOLING.Count;
        OBJ_POOLING _objPooling = new OBJ_POOLING();
        for (int i = 0; i < _total; i++)
        {
            _objPooling = LIST_OBJ_POOLING[i];
            _objPooling.Init();
            LIST_OBJ_POOLING[i] = _objPooling;
        }
    }



    #region TOWER PREFAB
    [System.Serializable]
    public class ClassTower
    {
        public TheEnumManager.TOWER eTower;
        public GameObject objTower;
    }
    [Header("_____ALL TOWER PREFAB IN GAME_________")]
    public List<ClassTower> LIST_TOWER_PREFAB;
    public GameObject GetTowerPrefab(TheEnumManager.TOWER eTower)
    {
        int length = LIST_TOWER_PREFAB.Count;
        for (int i = 0; i < length; i++)
        {
            if (LIST_TOWER_PREFAB[i].eTower == eTower)
                return LIST_TOWER_PREFAB[i].objTower;
        }
        return null;
    }
    #endregion


    #region ENEMY PREFAB
    [System.Serializable]
    public class ClassEnemyPrefab
    {
        public string strNameEnemy;
        public TheEnumManager.ENEMY eEnemy;
        public GameObject objEnemy;
    }
   
    [Header("_____ALL ENEMY PREFAB IN GAME_________")]
    [Space(20)]
    public List<ClassEnemyPrefab> LIST_ENEMY_PREFAB;
    public GameObject GetEnemyPrefab(TheEnumManager.ENEMY eEnemy)
    {
        return LIST_ENEMY_PREFAB[(int)eEnemy].objEnemy;
    }
    #endregion


    #region POOLING
    [System.Serializable]
    public struct OBJ_POOLING
    {
        public TheEnumManager.ITEMS_POOLING eItem;
        public GameObject objPrefab;
        public int iNumberOfArray;
        public GameObject[] Array;
        public void Init()
        {
            if (iNumberOfArray == 0) return;
            Array = new GameObject[iNumberOfArray];
            for (int i = 0; i < iNumberOfArray; i++)
            {
                Array[i] = Instantiate(objPrefab);
                Array[i].transform.SetParent(Instance.transform);
                Array[i].SetActive(false);
            }
        }
        public GameObject GetItem()
        {
            if (iNumberOfArray == 0) return null;
            for (int i = 0; i < iNumberOfArray; i++)
            {
                if (!Array[i].activeInHierarchy)
                {
                    // Array[i].transform.eulerAngles = new Vector3(0, 0, 90);
                    return Array[i];
                }
            }
            return null;
        }
    }
    [Header("______GAMEOBJECT POOL_______")]
    [Space(20)]
    public List<OBJ_POOLING> LIST_OBJ_POOLING;
    public OBJ_POOLING GetObj(TheEnumManager.ITEMS_POOLING eItem)
    {
        return LIST_OBJ_POOLING[(int)eItem];
    }
    #endregion


    #region OBJ OF SKILL
  
    [Header("_____OBJ PREFAB SKILL____________")]
    [Space(20)]
    public GameObject objSkill_MineOfRoad;
    public GameObject objSkill_BoomFromSky;
    public GameObject objSkill_PondOfPoison;
    public GameObject objSkill_Reinforcements_2Mans;
    public GameObject objSkill_Reinforcements_3Mans;
    #endregion

   

    #region TOWER POS
   
    [Header("_______KIND OF TOWER-POS___________")]
    [Space(20)]
    public GameObject objTowerPos_OfGlassMaps;
    public GameObject objTowerPos_OfSnowMaps;
    public GameObject GetTowerPosObj(TheEnumManager.KIND_OF_MAPS eKindOfMaps)
    {
        switch (eKindOfMaps)
        {
            case TheEnumManager.KIND_OF_MAPS.GlassMap:
                return objTowerPos_OfGlassMaps;
            case TheEnumManager.KIND_OF_MAPS.RedLand:
                return objTowerPos_OfGlassMaps;
            case TheEnumManager.KIND_OF_MAPS.SnowLand:
                return objTowerPos_OfSnowMaps;

        }
        return objTowerPos_OfGlassMaps;
    }

    #endregion


    #region OTHERS

 
    [Header("____________OTHER_________________")]
    [Space(20)]
    public Sprite sprCallWave_Skull;
    public Sprite sprCallWave_Empty;
    public GameObject objCoinGift; // Khi nguoi choi click wave ra som
    #endregion



    #region  HELPER
    [ContextMenu("Auto Update List enemy")]
    public void UpdateListEnemy()
    {
        int _total = System.Enum.GetNames(typeof(TheEnumManager.ENEMY)).Length;
        LIST_ENEMY_PREFAB.Clear();

        for (int i = 0; i < _total; i++)
        {
            ClassEnemyPrefab _enemy = new ClassEnemyPrefab();
            _enemy.eEnemy = (TheEnumManager.ENEMY)i;
            _enemy.strNameEnemy = _enemy.eEnemy.ToString();

            if (!_enemy.strNameEnemy.Contains("boss")) // Loại boss ra khỏi list
            {
                GameObject _objEnemy = Resources.Load<GameObject>("ENEMY-PREFAB/" + _enemy.strNameEnemy);
                _enemy.objEnemy = _objEnemy;
                LIST_ENEMY_PREFAB.Add(_enemy);
            }
        }

        Debug.Log("====UPDATE ENEMY: DONE=====");
    }

    [ContextMenu("Check Error of PrefabLevel")]
    public void CheckErrorOfPrefabLevel()
    {
        //Kiểm tra số lượng road trong level và số button đánh dấu road | 2 số liệu này phỉa bằng nhau:
        Debug.Log("CHECKING...");
        for (int i = 1; i < 1000; i++)
        {
            GameObject _objLevel = Resources.Load<GameObject>("Levels/LEVEL_" + i);
            if (_objLevel)
            {
                int _totalChild = _objLevel.transform.childCount;
                int _totalRoad = 0;
                int _totalMarkRoad = 0;
                for (int j = 0; j < _totalChild; j++)
                {

                    //ROADS
                    if (_objLevel.transform.GetChild(j).gameObject.name == "ROADS")
                    {
                        _totalRoad = _objLevel.transform.GetChild(j).childCount;
                    }

                    //MARK_GROUP
                    if (_objLevel.transform.GetChild(j).gameObject.name == "MARK_GROUP")
                    {
                        _totalMarkRoad = _objLevel.transform.GetChild(j).childCount;
                    }

                }
                if (_totalRoad != _totalMarkRoad)
                {
                    Debug.Log("ERROR: Road != MarkRoak: LEVEL" + i);
                }
            }
            else
            {
                Debug.Log("CHECK " + i + ": DONE");
                break;
            }

        }


    }

    [ContextMenu("Set enemy For All levels")]
    public void SetEnemyForAllLevel()
    {
        //Kiểm tra số lượng road trong level và số button đánh dấu road | 2 số liệu này phỉa bằng nhau:

        Debug.Log("SET LEVEL...");
        TheLevel _temp = new TheLevel();
        GameObject _objLevel = new GameObject();
        for (int i = 1; i < 1000; i++)
        {
            _objLevel = Resources.Load<GameObject>("Levels/LEVEL_" + i);

            int _totalEnemyOfGame = System.Enum.GetNames(typeof(TheEnumManager.ENEMY)).Length;

            if (_objLevel)
            {
                _temp = _objLevel.GetComponent<TheLevel>();

            HERE:
                _temp.LIST_CONFIG_ENEMY_FOR_LEVEL.Clear();
                for (int j = 0; j < 5; j++)//Moi level co 5 loai enemy
                {
                    int _rand = Random.Range(0, _totalEnemyOfGame);
                    string _idEnemy = ((TheEnumManager.ENEMY)_rand).ToString();
                    if (!_idEnemy.Contains("boss"))
                    {
                        _temp.LIST_CONFIG_ENEMY_FOR_LEVEL.Add((TheEnumManager.ENEMY)_rand);
                    }
                    else goto HERE;
                }


            }
            else
            {

                break;
            }

        }


        Debug.Log(" DONE");

    }

    [ContextMenu("Check -TowerPos- in Level")]
    public void CheckTowerPosGroup()
    {
        for (int i = 1; i < 1000; i++)
        {
            GameObject _objLevel = Resources.Load<GameObject>("Levels/LEVEL_" + i);
            if (_objLevel)
            {
                if (!_objLevel.transform.Find("TowerPos"))
                {
                    Debug.Log("NOT TOWERPOS AT LEVEL: " + i);
                }
            }
            else
            {

                break;
            }

        }

        Debug.Log(" DONE");

    }

    [ContextMenu("Check all enemy có đã được sử dụng hay không?")]
    public void CheckAllEnemyToUse()
    {
        int _totalEnemyInGame = System.Enum.GetNames(typeof(TheEnumManager.ENEMY)).Length;
        List<string> LIST_ENEMY_WAS_DESIGN = new List<string>();
        for (int i = 0; i < _totalEnemyInGame; i++)
        {
            LIST_ENEMY_WAS_DESIGN.Add(((TheEnumManager.ENEMY)i).ToString());
        }




        for (int i = 1; i < 1000; i++)
        {
            GameObject _objLevel = Resources.Load<GameObject>("Levels/LEVEL_" + i);
            if (_objLevel)
            {
                for (int j = 0; j < _objLevel.GetComponent<TheLevel>().LIST_CONFIG_ENEMY_FOR_LEVEL.Count; j++)
                {
                    if (CheckStringInList(LIST_ENEMY_WAS_DESIGN, _objLevel.GetComponent<TheLevel>().LIST_CONFIG_ENEMY_FOR_LEVEL[j].ToString()))
                    {
                        LIST_ENEMY_WAS_DESIGN.Remove(_objLevel.GetComponent<TheLevel>().LIST_CONFIG_ENEMY_FOR_LEVEL[j].ToString());
                    }
                }
            }
            else
            {

                break;
            }

        }

        for (int i = 0; i < LIST_ENEMY_WAS_DESIGN.Count; i++)
        {
            Debug.Log(" LIST_ENEMY_WAS_DESIGN: " + LIST_ENEMY_WAS_DESIGN[i]);
        }


    }

    private bool CheckStringInList(List<string> LIST, string _id)
    {
        for (int i = 0; i < LIST.Count; i++)
        {
            if (LIST[i] == _id)
                return true;
        }
        return false;
    }

    #endregion
}



