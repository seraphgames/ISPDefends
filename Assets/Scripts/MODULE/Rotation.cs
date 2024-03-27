using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    public Transform m_trannRotation;
    public float fSpeed;
    public bool bRightToLeft;
    private Vector3 vEuler;

    // Update is called once per frame
    void Update()
    {
        if (bRightToLeft)
            vEuler.z += Time.deltaTime * fSpeed;
        else
            vEuler.z -= Time.deltaTime * fSpeed;
        m_trannRotation.eulerAngles = vEuler;

    }
}
