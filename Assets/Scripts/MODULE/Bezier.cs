using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
    static Vector2 vBezier;
    static Vector2 vHigh;

    public static Vector2 GetBezier(Vector2 _from, Vector2 _to, float _time, float _high)
    {
        vHigh = (_from+_to)/2 + new Vector2(0, _high);
         vBezier = (1 - _time) * (1 - _time) * _from + 2 * (1 - _time) * _time * vHigh + _time * _time * _to;

        return vBezier;
    }
}
