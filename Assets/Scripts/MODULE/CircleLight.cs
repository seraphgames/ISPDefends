using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLight : MonoBehaviour
{


    [SerializeField]
    private SpriteRenderer m_spriteRenderer;


    float _a = 0;
    private void Update()
    {
        _a = m_spriteRenderer.color.a;
        if(_a>0)
        {
            _a -= Time.deltaTime*0.25f;
            m_spriteRenderer.color = new Color(
                m_spriteRenderer.color.r,
                m_spriteRenderer.color.g,
                m_spriteRenderer.color.b,
                _a);
        }
        
    }

  

}
