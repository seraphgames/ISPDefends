using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathGame
{

    //CÔNG THỨC TĂNG MÁU CHO ENEMY THEO LEVEL
    public static int GetHpFollowTheLevel(int _level)
    {
        return _level*2;
    }


    //CÔNG THỨC TĂNG MÁU CHO ENEMY THEO WAVE
    public static int GetHpFollowTheWave(int _wave, int _maxWave, int _BaseHP)
    {
        int _temp = Mathf.RoundToInt(Mathf.Sqrt((Mathf.Pow(_wave, 4) / 3) / (_maxWave + 1 - _wave)));      
        return _BaseHP + _temp;
        // return (int)(_BaseHP + Mathf.Sqrt((_wave ^ 4 / 2) / (_maxWave + 1 - _wave)));
        //return (int)(_BaseHP + Mathf.Sqrt((_wave ^ 4 / 4) / (_maxWave + 1 - _wave)));
    }



}
