using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonLevel : MonoBehaviour
{

    public Text txtText;
    private Button buThis;
    public Image imaRenderNomarl, imaRenderHard, imaRenderNightmate;
    private int iLevel;
    private int iStar;
    private bool bLocked = true;



    TheEnumManager.DIFFICUFT eDifficuft;

    private void Awake()
    {
        buThis = GetComponent<Button>();
        buThis.onClick.AddListener(() => ButtonClick());

    }



    // Use this for initialization
    public void Init(int _level, int _star, TheEnumManager.DIFFICUFT _difficuft, bool _lock)
    {
        iLevel = _level;
        iStar = _star;
        eDifficuft = _difficuft;

        if (!TheDataManager.THE_PLAYER_DATA.TESTING_MODE)
            bLocked = _lock;
        else
            bLocked = false;



        txtText.text = (iLevel+1).ToString();
        SetImage();
    }


    public void ButtonClick()
    {
        TheDataManager.THE_PLAYER_DATA.iCurrentLevel = iLevel;
        
        if (!bLocked)
        {
            //TheUIManager.Instance.LoadScene(TheEnumManager.SCENE.Gameplay);
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.battle_start);//sound
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Ready);
        }
        else
        {
            Debug.Log("LOCKED");
        }
    }



    public void SetImage()
    {
        if (iStar < 0)
        {
            
                imaRenderNomarl.gameObject.SetActive(true);
                imaRenderNomarl.sprite = MainCode_LevelSelection.Instance.LIST_SPRITE_OF_BUTTON_LEVEL_NORMAL[0];
                txtText.text = "";
                imaRenderHard.gameObject.SetActive(false);
                imaRenderNightmate.gameObject.SetActive(false);
           
        }
        else
        {
            switch (eDifficuft)
            {
                case TheEnumManager.DIFFICUFT.Normal:
                    imaRenderNomarl.sprite = MainCode_LevelSelection.Instance.LIST_SPRITE_OF_BUTTON_LEVEL_NORMAL[iStar];
                    imaRenderNomarl.gameObject.SetActive(true);
                    imaRenderHard.gameObject.SetActive(false);
                    imaRenderNightmate.gameObject.SetActive(false);
                    
                    break;
                case TheEnumManager.DIFFICUFT.Hard:
                    imaRenderNomarl.gameObject.SetActive(false);
                    imaRenderHard.gameObject.SetActive(true);
                    imaRenderHard.sprite = MainCode_LevelSelection.Instance.LIST_SPRITE_OF_BUTTON_LEVEL_HARD[iStar];
                    imaRenderNightmate.gameObject.SetActive(false);
                    break;
                case TheEnumManager.DIFFICUFT.Nightmate:
                    imaRenderNomarl.gameObject.SetActive(false);
                    imaRenderHard.gameObject.SetActive(false);
                    imaRenderNightmate.gameObject.SetActive(true);
                    imaRenderNightmate.sprite = MainCode_LevelSelection.Instance.LIST_SPRITE_OF_BUTTON_LEVEL_NIGHTMATE[iStar];
                    break;
            }
        }

    }

    public void Hide(int _index)
    {
        if (TheDataManager.THE_PLAYER_DATA.TESTING_MODE)
        {
            iLevel = _index;
               bLocked = false ;
            txtText.text = (_index+1).ToString();
            return;
        }
        imaRenderNomarl.gameObject.SetActive(false);
        imaRenderHard.gameObject.SetActive(false);
        imaRenderNightmate.gameObject.SetActive(false);
        txtText.color = Color.white * 0.0f;
    }

}
