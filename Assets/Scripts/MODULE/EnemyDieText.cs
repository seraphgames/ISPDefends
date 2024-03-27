using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieText : MonoBehaviour {

    public Transform m_tranOfRender;
    public SpriteRenderer m_spriteRender;
    public Sprite[] LIST_SPRITE;

    int _index;

    private void OnEnable()
    {
        m_tranOfRender.eulerAngles = new Vector3(0, 0, Random.Range(-13, 13));
           _index = Random.Range(0, LIST_SPRITE.Length);
        m_spriteRender.sprite = LIST_SPRITE[_index];
    }
}
