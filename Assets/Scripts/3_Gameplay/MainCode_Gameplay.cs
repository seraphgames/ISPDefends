using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MainCode_Gameplay : MonoBehaviour
{
    public enum INPUT_PLAYER
    {
        Normal,
        SHOW_UI,
        ReadyToUseSkill,
        ClickHero,
    }
    public INPUT_PLAYER eCURRENT_INPUT;

    public static MainCode_Gameplay Instance;


    [Space(20)]
    //public Transform m_tranBoardBuyTower;        // Position of tower------
    //public Transform m_tranBoardUpgrade;        // Board infor of Tower----
    public GameObject m_objPreBuildingPos;     // P------------------------
    public GameObject m_BoardMark;           // P-------------------------
    public BoardInfoTower m_boardInfoTower;
    public GameObject m_objPosIcon;




    private void Awake()
    {
        Application.targetFrameRate = 60;
        m_CameraMain = GameObject.Find("Main Camera").GetComponent<Camera>();
        //set camera for popup canvas
        ThePopupManager.Instance.SetCameraForPopupCanvas(Camera.main);


        if (Instance == null) Instance = this;
        TheGameStatusManager.SetGameStatus(TheGameStatusManager.GAME_STATUS.Loading);
        LoadLevel(TheDataManager.THE_PLAYER_DATA.iCurrentLevel);//Load level
        m_BoardMark.transform.position = Vector2.one * 1000;
    }



    private void Start()
    {

        Debug.Log("LEVEL_" + TheDataManager.THE_PLAYER_DATA.iCurrentLevel);

        TheMusic.Instance.Play();//play sound


        //tutorial
        if (TheDataManager.THE_PLAYER_DATA.iCurrentLevel == 0 && TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Normal)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Tutorial, 1.2f);
        }

    }




    private void Update()
    {
        InputPlayer();
    }





    //LOAD LEVEL  =======================================================
    private void LoadLevel(int _index)
    {
        GameObject _level = Resources.Load<GameObject>("Levels/LEVEL_" + (TheDataManager.THE_PLAYER_DATA.iCurrentLevel + 1));

        if (!_level)
            _level = Resources.Load<GameObject>("Levels/LEVEL_1");


        Instantiate(_level);

    }



    //INPUT  ============================================================
    RaycastHit2D _hit;
    TheItems _currentObj;
    Vector2 vInputMouse;
    Camera m_CameraMain;
    public void InputPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ThePopupManager.Instance.IsShowing) return;

            vInputMouse = m_CameraMain.ScreenToWorldPoint(Input.mousePosition);
            _hit = Physics2D.Raycast(vInputMouse, Vector2.zero);

            if (_hit.collider)
            {
                _currentObj = _hit.collider.GetComponent<TheItems>();

                if (_currentObj != null)
                {
                    Debug.Log(_currentObj.m_items.ToString());
                    ProcessItem(_currentObj);
                }
            }
        }
    }



    //PROCESS ITEM   ===================================================

    public Tower TOWER_IS_SELECTED;//sell or upgrade
                                   // private Transform PRE_BUILDING_POS;

    public void ProcessItem(TheItems _theItem)
    {
        #region SKILL
        if (eCURRENT_INPUT == INPUT_PLAYER.ReadyToUseSkill)
        {
            TheSkillManager.Instance.POWER_UI_MANAGER.Get((int)TheSkillManager.Instance.eCURRENT_SKILL).Active(vInputMouse);
            return;
        }
        #endregion



        #region NORMAL
        switch (_theItem.m_items)
        {
            case TheEnumManager.ITEMS.Background://TOUCH BACKGROUND==========================

                TheButtonTower.Instance.ShowBoard(TheButtonTower.Board.HideAll);
                //show info tower
                m_boardInfoTower.Hide();
                break;

            case TheEnumManager.ITEMS.PointBuilding://POS TO BUILD TOWER=============================


                m_BoardMark.transform.position = _theItem.transform.position + new Vector3(0, 1.0f, 0);
                TheButtonTower.Instance.SetTowerPosToBuild(_theItem.gameObject);
                TheButtonTower.Instance.ShowBoard(TheButtonTower.Board.BuyTower);

                //show info tower
                m_boardInfoTower.Hide();

                TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.touch_tower_pos);//sound
                break;

            case TheEnumManager.ITEMS.Tower://TOUCH TOWER===================================



                m_BoardMark.transform.position = _theItem.transform.position + new Vector3(0, 1.5f, 0);
                TOWER_IS_SELECTED = _theItem.GetComponent<Tower>();
                TOWER_IS_SELECTED.ShowCircle();

                #region BOARD UPGRADE
                TheButtonTower.Instance.SetSelectedTower(TOWER_IS_SELECTED);
                TheButtonTower.Instance.ShowBoard(TheButtonTower.Board.UpgradeOfSell);


                //show info tower
                // m_boardInfoTower.gameObject.SetActive(true);

                //TOWER MAGIC, ARCHER, STONE
                m_boardInfoTower.ShowContent(TOWER_IS_SELECTED);
                #endregion

                TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.touch_tower_pos);//sound
                break;

            case TheEnumManager.ITEMS.Hero://TOUCH HERO===================================
                eCURRENT_INPUT = INPUT_PLAYER.ClickHero;
                TOWER_IS_SELECTED = _theItem.GetComponent<Tower>();

                break;

        }
        #endregion


    }

    //MAIN RANGE: Phạm vi an toàn để các sự kiện game (bị đánh,...) diễn ra. Dự kiến các tâm màn hình khoảng 8
    public bool IsSafetyRange(Vector2 _target)
    {
        if (Vector2.Distance(_target, Vector2.zero) <= 8) return true;
        return false;
    }

    //SET INPUT    ====================================================
    public void SetInput(INPUT_PLAYER eInput)
    {
        eCURRENT_INPUT = eInput;
        switch (eCURRENT_INPUT)
        {
            case INPUT_PLAYER.Normal:
                break;
            case INPUT_PLAYER.ReadyToUseSkill:
                break;

        }
    }




    private void OnDisable()
    {
        //SAVE DATA
        TheDataManager.Instance.SerialzerPlayerData(); //SAVE

    }




}
