using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SellingProduct : ScriptableObject
{
    [SerializeField] string _name;
    public string strName { get { return _name; } }


    [SerializeField] string _content;
    public string strContent { get { return _content; } }


    [SerializeField] int _valueToAdd;
    public int iValueToAdd { get { return _valueToAdd; } }



    public virtual void Get()
    {

    }
}
