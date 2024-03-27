using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTime : MonoBehaviour {
    private Image m_Image;
    public float fTimeLife = 3.0f;
    private float fCountTime;

	// Use this for initialization
	void Start () {
        m_Image = GetComponent<Image>();
        fCountTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Fill();
    }

    private void Fill()
    {
        if (fCountTime > 0) fCountTime -= Time.deltaTime;
        m_Image.fillAmount = fCountTime / fTimeLife;
    }

    public void StartCount()
    {
        fCountTime = fTimeLife;
    }
    public bool IsReady()
    {
        return fCountTime <= 0;
    }
}
