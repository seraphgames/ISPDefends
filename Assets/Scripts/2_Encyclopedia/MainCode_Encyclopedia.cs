using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCode_Encyclopedia : MonoBehaviour
{
    public static MainCode_Encyclopedia Instance;
    public Button buBack, buScreenTips;
    public List<Button> LIST_BUTTON_GROUP;
    public List<GameObject> LIST_PANEL_GROUP;

    public Transform tranParent_ButtonTower;
    public Transform tranParent_ButtonEnemy;
    public Transform tranParent_ButtonSkill;

    public List<Button> LIST_IMAGE_BUTTON_TOWERS;
    public List<Button> LIST_IMAGE_BUTTON_ENEMIES;
    public List<Button> LIST_IMAGE_BUTTON_SKILL;



    public Transform GROUP_ENEMY_INFO_DATA;




    [Space(20)]
    [Header("INFO TEXT")]
    public Text txtDamage;
    public Text txtAttackSpeed, txtHP, txtRange, txtMoveSpeed, txtDefense;
    [Space(20)]
    public Text txtName, txtContent;

    [Space(20)]
    public Image imgMainIcon;
    public GameObject objBoardInfo;

    private void Awake()
    { //set camera for popup canvas
        Application.targetFrameRate = 60;
        ThePopupManager.Instance.SetCameraForPopupCanvas(GameObject.Find("Main Camera").GetComponent<Camera>());


        if (Instance == null)
            Instance = this;

        buBack.onClick.AddListener(() => ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.LevelSelection, true));

        LIST_BUTTON_GROUP[0].onClick.AddListener(() => ShowPanel(0));
        LIST_BUTTON_GROUP[1].onClick.AddListener(() => ShowPanel(1));
        LIST_BUTTON_GROUP[2].onClick.AddListener(() => ShowPanel(2));



        LIST_IMAGE_BUTTON_TOWERS = new List<Button>();
        LIST_IMAGE_BUTTON_ENEMIES = new List<Button>();
        LIST_IMAGE_BUTTON_SKILL = new List<Button>();



        AddButtonToList(LIST_IMAGE_BUTTON_TOWERS, tranParent_ButtonTower);
        AddButtonToList(LIST_IMAGE_BUTTON_ENEMIES, tranParent_ButtonEnemy);
        AddButtonToList(LIST_IMAGE_BUTTON_SKILL, tranParent_ButtonSkill);



    }

    private void Start()
    {
        ShowPanel(0);
        SetIndexForEnemyInfoData();

        //tips
        buNextTips.onClick.AddListener(() => ButtonNextTip());
        buBackTips.onClick.AddListener(() => ButtonBackTip());
        buGotIt.onClick.AddListener(() => ShowTipsScreen(false));
        buScreenTips.onClick.AddListener(() => ShowTipsScreen(true));
    }


    private void AddButtonToList(List<Button> _list, Transform _parent)
    {

        int _total = _parent.childCount;
        for (int i = 0; i < _total; i++)
        {
            _list.Add(_parent.GetChild(i).GetComponent<Button>());
        }
    }



    private void ShowPanel(int _index)
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
        int _total = LIST_PANEL_GROUP.Count;
        for (int i = 0; i < _total; i++)
        {
            if (i == _index)
            {
                LIST_PANEL_GROUP[i].SetActive(true);
                LIST_BUTTON_GROUP[i].image.color = Color.white;
            }
            else
            {
                LIST_PANEL_GROUP[i].SetActive(false);
                LIST_BUTTON_GROUP[i].image.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
        }

        if (_index == 0)
            LIST_IMAGE_BUTTON_TOWERS[0].GetComponent<TowerInfo>().ShowTowerInfo();
        else if (_index == 1)
            LIST_IMAGE_BUTTON_ENEMIES[0].GetComponent<EnemyInfo>().ShowEnemyInfo();
        else if (_index == 2)
            LIST_IMAGE_BUTTON_SKILL[0].GetComponent<SkillInfo>().ShowSkillinfo();
    }



    private void SetIndexForEnemyInfoData()
    {
        int _total = GROUP_ENEMY_INFO_DATA.GetChild(0).childCount;
        for (int i = 0; i < _total; i++)
        {
            EnemyInfo _enemyInfo = GROUP_ENEMY_INFO_DATA.GetChild(0).GetChild(i).GetComponent<EnemyInfo>();
            _enemyInfo.iIndex = i;
            _enemyInfo.Init();

        }
    }


    //Show tower info

    public void ShowTowerInfo(TowerData _mytowerdata, TheEnumManager.TOWER_LEVEL _towerLevel, Button _button)
    {
        objBoardInfo.SetActive(true);


        txtName.text = _mytowerdata.strTowerName.ToUpper().ToString();

        if (_mytowerdata.iLevelToUnlock == 0)
        {
            txtContent.text = _mytowerdata.strContent.ToString() + " - Unlocked";
            if (_mytowerdata.GetNumberOfBullet(_towerLevel) != 1)
                txtContent.text = _mytowerdata.strContent.ToString() + " With " + _mytowerdata.GetNumberOfBullet(_towerLevel) + " bullets - Unlocked";
        }
        else
        {
            txtContent.text = _mytowerdata.strContent.ToString() + " - Unlock at level " + _mytowerdata.iLevelToUnlock;
            if (_mytowerdata.GetNumberOfBullet(_towerLevel) != 1)
            {
                txtContent.text = _mytowerdata.strContent.ToString() + " With " + _mytowerdata.GetNumberOfBullet(_towerLevel) + " bullets - Unlock at level " + _mytowerdata.iLevelToUnlock;
            }
        }




        txtDamage.text = _mytowerdata.GetDamage(_towerLevel).ToString();
        txtAttackSpeed.text = _mytowerdata.GetFireRate(_towerLevel).ToString() + " s";
        txtRange.text = _mytowerdata.GetRange(_towerLevel).ToString() + " m";

        //hide
        txtMoveSpeed.text = " ...";
        txtDefense.text = "...";
        txtHP.text = "...";



        ColorButtonWasChoose(_button, LIST_IMAGE_BUTTON_TOWERS);//color for button
        imgMainIcon.sprite = _button.image.sprite;
    }

    private void ColorButtonWasChoose(Button _button, List<Button> _list)
    {

        int length = _list.Count;
        for (int i = 0; i < length; i++)
        {
            if (_button == _list[i])
            {
                Debug.Log(_button.name);
                _list[i].image.color = Color.white * 1.0f;
            }
            else
            {
                _list[i].image.color = Color.gray;
            }
        }
    }


    //show enemy info

    public void ShowEnemyInfo(EnemiesData _enemydata, Button _button)
    {
        objBoardInfo.SetActive(true);


        txtName.text = _enemydata.strName.ToUpper().ToString();
        txtContent.text = _enemydata.strContent.ToString();

        txtDamage.text = _enemydata.iConfig_Damage.ToString();
        txtAttackSpeed.text = _enemydata.fConfig_MoveSpeed.ToString() + " s";
        txtRange.text = _enemydata.fConfig_RangeToAttack.ToString() + " m";

        //hide
        txtMoveSpeed.text = _enemydata.GetSpeed().ToString();
        txtDefense.text = "...";
        txtHP.text = _enemydata.iConfig_BaseHp.ToString();


        ColorButtonWasChoose(_button, LIST_IMAGE_BUTTON_ENEMIES);//color for button
    }

    //Show skill info
    public void ShowSkillInfo(TheEnumManager.POWER_UP _skill, Sprite _icon, Button _button)
    {
        objBoardInfo.SetActive(false);
        imgMainIcon.sprite = _icon;

        SellingSkillData _product = TheDataManager.Instance.SELLING_SKILL_MANAGER.Get(_skill);
        txtName.text = _product.strName;
        txtContent.text = _product.strContent;
        
        ColorButtonWasChoose(_button, LIST_IMAGE_BUTTON_SKILL);

        txtDamage.text = "...";

        txtAttackSpeed.text = "...";
        txtHP.text = "...";
        txtRange.text = "...";
        txtMoveSpeed.text = "...";
        txtDefense.text = "...";
    }




    #region TIPS
    [Space(30)]
    public GameObject objTipsScreen;
    public Text txtTipsContent;
    public Button buNextTips, buBackTips, buGotIt;
    private int _index;
    private void ShowTipsScreen(bool _active)
    {
        if (_active) TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
        else TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);//sound
        objTipsScreen.SetActive(_active);
        _index = -1;
        ButtonNextTip();
    }
    private void ButtonNextTip()
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
        _index++;
        if (_index == TheDataManager.Instance.TIP_MANAGER.iTotal)
            _index = 0;
        txtTipsContent.text = "TIP: " + TheDataManager.Instance.TIP_MANAGER.GetTips(_index);
    }
    private void ButtonBackTip()
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);//sound
        _index--;
        if (_index == -1)
            _index = TheDataManager.Instance.TIP_MANAGER.iTotal - 1;
        txtTipsContent.text = "TIP: " + TheDataManager.Instance.TIP_MANAGER.GetTips(_index);
    }




    #endregion
}
