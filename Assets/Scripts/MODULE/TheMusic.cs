using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMusic : MonoBehaviour
{
    public static TheMusic Instance;
    private AudioSource m_AudioSource;

    public AudioClip[] LIST_SOUNDTRACK;
    // Use this for initialization
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        m_AudioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetString("music") == "mute")
            Mute = true;
        else
            Mute = false;
    }


    private void Start()
    {
        Play(0);
    }

    //MUTE
    public bool Mute
    {
        get
        {
            return m_AudioSource.mute;
        }
        set
        {

            m_AudioSource.mute = value;

            //SAVE
            if (m_AudioSource.mute)
                PlayerPrefs.SetString("music", "mute");
            else
                PlayerPrefs.SetString("music", "no");

            PlayerPrefs.Save();
        }
    }

    //PLAY
    public void Play()
    {
        if (!m_AudioSource) return;

        m_AudioSource.Stop();
        int _rand = Random.Range(1, LIST_SOUNDTRACK.Length);
        m_AudioSource.clip = LIST_SOUNDTRACK[_rand];
        m_AudioSource.Play();
    }
    public void Play(int _index)
    {
        m_AudioSource.Stop();      
        m_AudioSource.clip = LIST_SOUNDTRACK[_index];
        m_AudioSource.Play();
    }



    //STOP
    public void Stop()
    {
        m_AudioSource.Stop();
    }

}
