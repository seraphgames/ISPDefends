using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TheEventManager
{

    #region ENEMY EVENT
    public delegate void EnemyEvent(Enemy _enemy);


    //enemy is destroy on road
    public static event EnemyEvent OnEnemyIsDetroyOnRoad;
    public static void PostEvent_OnEnemyIsDetroyOnRoad(Enemy _enemy)
    {
        if (OnEnemyIsDetroyOnRoad != null)
            OnEnemyIsDetroyOnRoad(_enemy);
    }

    //enemy attack home
    public static event EnemyEvent OnEnemyCompletedRoad;
    public static void PostEvent_OnEnemyCompletedRoad(Enemy _enemy)
    {
        if (OnEnemyCompletedRoad != null)
            OnEnemyCompletedRoad(_enemy);
    }


    //enemy is ready to go
    public static event EnemyEvent OnEnemyIsBorn;
    public static void PostEvent_OnEnemyIsBorn(Enemy _enemy)
    {
        if (OnEnemyIsBorn != null)
            OnEnemyIsBorn(_enemy);
    }


    //enemy hit rocket
    public static event EnemyEvent OnEnemyHitRocket;
    public static void PostEvent_OnEnemyHitRocket(Enemy _enemy)
    {
        if (OnEnemyHitRocket != null)
            OnEnemyHitRocket(_enemy);
    }


    #endregion


    #region WEAPON
    public delegate void EventOfWeapon(Vector2 _pos, int _damage);

    public static event EventOfWeapon OnRocketHit;
    public static void PostEvent_RocketHit(Vector2 _pos, int _damage)
    {
        if (OnRocketHit != null) OnRocketHit(_pos, _damage);
    }


    #endregion


    #region EVENT
    public delegate void GameEvent();


    public static event GameEvent OnStartWave;
    public static void PostGameEvent_OnStartWave()
    {
        if (OnStartWave != null) OnStartWave();
    }


    public static event GameEvent OnUpdateBoardInfo;
    public static void PostGameEvent_OnUpdateBoardInfo()
    {
        if (OnUpdateBoardInfo != null) OnUpdateBoardInfo();
    }


    public static event GameEvent OnUpdateSkillBoard;
    public static void PostGameEvent_OnUpdateSkillBoard()
    {
        if (OnUpdateSkillBoard != null) OnUpdateSkillBoard();
    }


    public static event GameEvent OnOpenUIPopup;
    public static void PostGameEvent_OnOpenUIPopup()
    {
        if (OnOpenUIPopup != null) OnOpenUIPopup();
    }


    public static event GameEvent OnCloseUIPopup;
    public static void PostGameEvent_OnCloseUIPopup()
    {
        if (OnCloseUIPopup != null) OnCloseUIPopup();
    }


    public static event GameEvent OnBuyHeroes;
    public static void PostGameEvent_OnBuyHeroes()
    {
        if (OnBuyHeroes != null) OnBuyHeroes();
    }

    // mua hero
    public static event GameEvent OnUnlockTower;
    public static void PostGameEvent_OnUnlockTower()
    {
        if (OnUnlockTower != null) OnUnlockTower();
    }






    #endregion

    #region EVENT OF WAVE MARK
    public delegate void EventOfWaveMark(TheStartingWaveMark _wavemark);

    // touch wave mark
    public static event EventOfWaveMark OnTouchWaveMark;
    public static void PostGameEvent_OnTouchWaveMark(TheStartingWaveMark _wavemark)
    {
        if (OnTouchWaveMark != null) OnTouchWaveMark(_wavemark);
    }
    #endregion


    #region EVENT OF GAME STATUS

    public delegate void EVENT_OF_GAMESTATUS(int _star);
    public static event EVENT_OF_GAMESTATUS OnGameWinning;
    public static event EVENT_OF_GAMESTATUS OnGameDefeat;
    public static event EVENT_OF_GAMESTATUS OnGameStart;


    public static void EventGameWinning(int _star)
    {
        if (OnGameWinning != null) OnGameWinning(_star);
    }
    public static void EventGameDefeat(int _star = 0)
    {
        if (OnGameDefeat != null) OnGameDefeat(_star);
    }
    public static void EventGameStart(int _star = 0)
    {
        if (OnGameStart != null) OnGameStart(_star);
    }



    #endregion

}
