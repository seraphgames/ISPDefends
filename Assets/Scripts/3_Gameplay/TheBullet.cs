using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBullet : MonoBehaviour {
    public Transform m_transform;
    public float fSpeed;
    private Vector2 vCurrentPos, vTargetPos;

    private void Awake()
    {
        m_transform = transform;
    }

    //private void OnEnable()
    //{
    //    vCurrentPos = m_transform.position;
    //}

    // Update is called once per frame
    void Update () {

    
        vCurrentPos = m_transform.position;
        vCurrentPos = Vector2.MoveTowards(vCurrentPos, vTargetPos, Time.deltaTime * fSpeed);
        if(vCurrentPos==vTargetPos)
        {
            gameObject.SetActive(false);
     
        }
        m_transform.position = vCurrentPos;

    }

    public void Shot(Vector2 _pos)
    {
        vTargetPos = _pos;
   
    }
}
