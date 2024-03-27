using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{
    private Button buThisButton;
    public TheEnumManager.TOWER eTower;
    public TheEnumManager.TOWER_LEVEL eTowerLevel;
    // public Transform m_tranOfMark;

    // Use this for initialization
    void Awake()
    {
        buThisButton = GetComponent<Button>();
        buThisButton.onClick.AddListener(() => ShowTowerInfo());


    }



    public void ShowTowerInfo()
    {

        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound                                                         // m_tranOfMark.position = transform.position;
        MainCode_Encyclopedia.Instance.ShowTowerInfo(TheDataManager.Instance.TOWER_DATA_MANAGER.Get(eTower), eTowerLevel, buThisButton);

    }



}
