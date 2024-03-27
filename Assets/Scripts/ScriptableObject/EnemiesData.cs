using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy data")]
[System.Serializable]
public class EnemiesData : ScriptableObject
{
    public TheEnumManager.ENEMY eEnemy;
    public GameObject objPrefab;
    public string strName;
    public string strContent;

    [Space(30)]
    public int iHeart;
    public int iConfig_Damage;
    public float fConfig_RangeToAttack;
    public float fConfig_AttackSpeed;

    public float fConfig_MoveSpeed;  
    public int iConfig_BaseHp;
    public int iConfigCoin;

    [Space(30)]
    public bool bIsInfantry; // bo binh
    public bool bIsAirForece;// khong quan
    public bool bIsBoss;



    //GET HP
    private int _currentHP;
    public int GetHp(int iCurrentWave,int iMaxWave)
    {
        _currentHP = iConfig_BaseHp;
        if (iCurrentWave > 10)
        {
            float _factorWave = iCurrentWave * 1.0f / iMaxWave;
            //EASY
            if (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Normal)
            {
                _currentHP += 5 * iCurrentWave;
            }
            //NORMAL
            else if (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Hard)
            {
                if (_factorWave < 0.5f)
                {
                    _currentHP += 5 * iCurrentWave;
                }
                else if (_factorWave >= 0.5f && _factorWave <= 0.7f)
                {
                    _currentHP += 6 * iCurrentWave;
                }
                else
                {
                    _currentHP += 7 * iCurrentWave;
                }
            }
            //HARD
            else if (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
            {
                if (_factorWave < 0.5f)
                {
                    _currentHP += 6 * iCurrentWave;
                }
                else if (_factorWave >= 0.5f && _factorWave <= 0.7f)
                {
                    _currentHP += 8 * iCurrentWave;
                }
                else
                {
                    _currentHP += 10 * iCurrentWave;
                }
            }
        }
        else
        {
            //EASY
            if (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Normal)
            {
                _currentHP += (5 * iCurrentWave);
            }
            //NORMAL
            else if (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Hard)
            {
                _currentHP += (6 * iCurrentWave);
            }
            //HARD
            else if (TheDataManager.THE_PLAYER_DATA.GAME_DIFFICUFT == TheEnumManager.DIFFICUFT.Nightmate)
            {
                _currentHP += (7 * iCurrentWave);
            }
        }

        //level 1->4, máu giảm 50% để người chơi làm quen với game.
        if ( TheDataManager.THE_PLAYER_DATA.iCurrentLevel < 3)
        {
            _currentHP = (int)(_currentHP * 0.8f);
        }
        return _currentHP;
    }


    //GET SPEED
    private float _currentSpeed;
    public float GetSpeed()
    {
        _currentSpeed = fConfig_MoveSpeed;


        return _currentSpeed;
    }
}
