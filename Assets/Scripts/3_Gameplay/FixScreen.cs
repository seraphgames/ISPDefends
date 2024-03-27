using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScreen : MonoBehaviour
{
    private Camera m_MainCamera;


    float _defaulWidth;
    // Use this for initialization
    void Awake()
    {
        m_MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
       // m_MainCamera.aspect = 16.0f / 9.0f;
        _defaulWidth = m_MainCamera.orthographicSize * (16.0f/9.0f);
        // Debug.Log("m_MainCamera.aspect: " + m_MainCamera.aspect);
        m_MainCamera.orthographicSize = _defaulWidth / m_MainCamera.aspect;
    }

   
}
