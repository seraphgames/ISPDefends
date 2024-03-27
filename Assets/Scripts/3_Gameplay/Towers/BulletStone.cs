using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStone : BulletBezier
{
    private bool bAllowShowCrackHole = false;

    private void OnEnable()
    {
        Invoke("AllowShowCrackHole", 1.0f);
    }

    private void AllowShowCrackHole()
    {
        bAllowShowCrackHole = true;
    }

    private void OnDisable()
    {
        
        if (bAllowShowCrackHole)
        {
            HitEnemy();
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.stone_crack);//sound
            GameObject _crackHole = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.CrackHole).GetItem();
            if (_crackHole)
            {
                _crackHole.transform.position = m_tranform.position;
                _crackHole.SetActive(true);
            }
        }
    }

}