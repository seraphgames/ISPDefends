using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    public int iIndex;
    private Button buThisButton;
    //public Transform m_tranOfMark;
    // Use this for initialization
    void Awake()
    {
        buThisButton = GetComponent<Button>();
        buThisButton.onClick.AddListener(() => ShowEnemyInfo());

    }

    public void Init()
    {
        int _totalEnemy = System.Enum.GetNames(typeof(TheEnumManager.ENEMY)).Length;
        if (iIndex >= _totalEnemy)
            gameObject.SetActive(false);
    }

    public void ShowEnemyInfo()
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
        //m_tranOfMark.position = transform.position;
        MainCode_Encyclopedia.Instance.ShowEnemyInfo(
            TheDataManager.Instance.ENEMY_DATA_MANAGER.Get((TheEnumManager.ENEMY)iIndex), buThisButton);
    }
}
