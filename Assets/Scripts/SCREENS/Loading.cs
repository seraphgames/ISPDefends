using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {
    public static Loading Instance;
    public Animator m_animator;
    public AnimationClip aniLoadingIn, aniLoadingOut;

	// Use this for initialization
	void Awake () {
        if (Instance == null) Instance = this;
    }
	
	

    public void PlayLoading(bool _bool)
    {
        if (_bool)
        {
            m_animator.speed = 2.0f;
            m_animator.Play(aniLoadingIn.name);
        }
            
        else
        {
            m_animator.speed = 2.0f;
            m_animator.Play(aniLoadingOut.name);
          
        }
    }
}
