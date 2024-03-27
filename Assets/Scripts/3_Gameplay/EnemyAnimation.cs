using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public enum ANI
    {
        Move,
        Attack,
    }
    public Animator m_animator;
    public AnimationClip m_aniMove, m_aniAttack;
    public float fSpeedAnimation = 1;


    public void Stop()
    {
        if (m_animator)
            m_animator.speed = 0;
    }


    public void Play(ANI _ani, float _speed = 1)
    {
        if (!m_animator) return;

        m_animator.speed = _speed;
        switch (_ani)
        {
            case ANI.Move:

                m_animator.Play(m_aniMove.name);
                break;
            case ANI.Attack:
                if (m_aniAttack)
                    m_animator.Play(m_aniAttack.name);
                break;
            default:
                break;
        }
    }
}
