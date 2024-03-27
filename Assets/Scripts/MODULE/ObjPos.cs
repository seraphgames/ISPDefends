using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPos : MonoBehaviour {
    private Transform m_tranform;
    private Transform m_tranChild;
    private Vector3 vPosChild;
	// Use this for initialization

	void Start () {
        m_tranform = transform;
        int _total = m_tranform.childCount;

        for (int i = 0; i < _total; i++)
        {
            m_tranChild = m_tranform.GetChild(i);
            vPosChild = m_tranChild.position;
            vPosChild.z = vPosChild.y;
            m_tranChild.position = vPosChild;
        }
	}
	
}
