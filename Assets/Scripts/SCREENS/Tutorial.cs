using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : PopUp
{
    public Button buLeft, buRight;
    public Image imaMain;
    public List<Sprite> LIST_SPRITE;
    int iIndexOfSprite = 0;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        buLeft.onClick.AddListener(() => ButtonLeft());
        buRight.onClick.AddListener(() => ButtonRight());

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        iIndexOfSprite = 0;
        ShowImage(0);
    }



    private void ButtonLeft()
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_back);//sound
        iIndexOfSprite--;
        if (iIndexOfSprite < 0) iIndexOfSprite = LIST_SPRITE.Count - 1;
        ShowImage(iIndexOfSprite);
    }
    private void ButtonRight()
    {
        TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.ui_click_next);//sound
        iIndexOfSprite++;
        if (iIndexOfSprite >= LIST_SPRITE.Count) iIndexOfSprite = 0;
        ShowImage(iIndexOfSprite);
    }


    private void ShowImage(int _index)
    {
        imaMain.sprite = LIST_SPRITE[_index];
    }
}
