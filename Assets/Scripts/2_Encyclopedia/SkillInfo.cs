using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour {
    private Button buThisButton;
    private Sprite sprIcon;
    public TheEnumManager.POWER_UP eSkill;
   // public Transform m_tranOfMark;

	// Use this for initialization
	void Awake () {
        buThisButton = GetComponent<Button>();
        buThisButton.onClick.AddListener(() => ShowSkillinfo());
        sprIcon = buThisButton.image.sprite;


       
    }


    public void ShowSkillinfo()
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
        //m_tranOfMark.position = transform.position;
        MainCode_Encyclopedia.Instance.ShowSkillInfo(eSkill, sprIcon,buThisButton);
    }

}
