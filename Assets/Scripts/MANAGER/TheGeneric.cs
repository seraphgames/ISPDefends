using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TheGeneric<T>
{
    public List<T> MY_LIST = new List<T>();

    public T Get(int _index)
    {
        if (_index >= 0 && _index < MY_LIST.Count)
            return MY_LIST[_index];
        else
            return default;
    }


    public void Set(T _value, int _index)
    {
        if (_index >= 0 && _index < MY_LIST.Count)
            MY_LIST[_index] = _value;
        else
            MY_LIST.Add(_value);
    }
}



[System.Serializable]
public class MyGeneric_Platform : TheGeneric<InfoGame>
{
    public InfoGame Get(TheEnumManager.PLATFORM _platform)
    {
        foreach (var item in MY_LIST)
        {
            if (item.ePLATFORM == _platform) return item;
        }
        return null;
    }
}
