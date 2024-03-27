using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCode_EndGame : MonoBehaviour {

    public Button buRateUs, buMoreGame, buMenu;

	// Use this for initialization
	void Start ()
    {
        Application.targetFrameRate = 60;
        TheMusic.Instance.Play();//music
        buMenu.onClick.AddListener(() => ThePopupManager.Instance.SCENE_MANAGER.LoadScene(TheEnumManager.SCENE.Menu));
        buRateUs.onClick.AddListener(() => ThePopupManager.Instance.OpenLink(ThePlatformManager.Instance.GAME_INFO.strLinkLike));
        buMoreGame.onClick.AddListener(() => ThePopupManager.Instance.OpenLink(ThePlatformManager.Instance.GAME_INFO.strLinkMoreGame));

    }
	
	
}
