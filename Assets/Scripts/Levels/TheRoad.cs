using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRoad : MonoBehaviour {

    public List<Vector2> LIST_POS;

    public int iTotalPos;
    private void Start()
    {
        iTotalPos = transform.childCount;

        for (int i = 0; i < iTotalPos; i++)
        {
            LIST_POS.Add(transform.GetChild(i).position);
        }

       TheLevel.Instance.LIST_THE_ROAD.Add(this);
    }




    public Vector2 GetPos(int _index)
    {
        if (_index >= iTotalPos) _index = iTotalPos - 1;
        if (_index < 0) _index = 0;

        Vector2 _pos = LIST_POS[_index];

        _pos.y += Random.Range(-1f, 1f);
        _pos.x += Random.Range(-0.4f, 0.4f);


        return _pos;
    }






    private void OnDrawGizmos()
    {
        int _total = transform.childCount;
        for (int i = 1; i < _total; i++)
        {
            Debug.DrawLine(transform.GetChild(i).position, transform.GetChild(i - 1).position,Color.blue);
        }
    }
}
