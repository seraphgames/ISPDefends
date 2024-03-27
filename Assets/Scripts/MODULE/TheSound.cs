using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSound : MonoBehaviour
{
    public static TheSound Instance;
    private AudioSource m_AudioSource;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        m_AudioSource = GetComponent<AudioSource>();

    }

    private void Start()
    {
        if (PlayerPrefs.GetString("sound") == "mute")
            Mute = true;
        else
            Mute = false;
    }



    ////SOUND--------------------------------------------
    #region UI SOUND
    //public enum SOUND_UI
    //{
    //    ButtonNext,
    //    ButtonBack,
    //    ShowPopup,
    //    HidePopup,
    //    victory,
    //    defeat,

    //    star_wave,
    //    can_not,
    //    coin,
    //    start_gameplay,
    //    buy_something,

    //}
    //[System.Serializable]
    //public struct SOUND_UI_ELEMENT
    //{
    //    public SOUND_UI eSound;
    //    public AudioClip auSound;
    //}
    //[Header("----SOUND UI----")]
    //public List<SOUND_UI_ELEMENT> LIST_SOUND_UI;
    //public void PlaySoundUI(SOUND_UI eSound)
    //{
    //    if (m_AudioSource)
    //        m_AudioSource.PlayOneShot(LIST_SOUND_UI[(int)eSound].auSound);
    //}
    #endregion






    //SOUND IN GAMEPLAY--------------------------------
    public enum SOUND_IN_GAME
    {
        //*****************[UI]*************
        ui_buy_something,
        ui_can_not,
        ui_click_back,
        ui_click_next,
        ui_coin,
        ui_winning,
        ui_defeat,
        ui_upgrade_upgrade,
        ui_upgrade_reset,
        ui_star_1,
        ui_star_2,
        ui_star_3,


        //*****************[SFX]*************
        battle_new_wave,
        battle_start,
        battle_last_wave,
        heart,

        stone_crack,

        explosion,
        explosion_mine_on_road,
        explosion_poison,

        //SKILL
        skill_freeze,
        skill_lord_fire,
        skill_soldier,
        skill_poison,

        //TOWER

        touch_tower_pos,
        tower_build,
        tower_sell,
        tower_upgrade,

        attack_archer,
        attack_cannonner,
        attack_gunmen,
        attack_thunder,
        attack_magic,
        attack_rocket_laucher,
        attack_soldier,
        attack_soldier1,
        attack_soldier2,
        attack_poison,


        soldier_no_fair,

    }

    #region  SOUND IN GAME


    [System.Serializable]
    public struct SOUND_IN_GAME_ELEMENT
    {
        public SOUND_IN_GAME eSound;
        public AudioClip auSound;
    }
    [Space(20)]
    [Header("----SOUND IN GAME----")]
    public List<SOUND_IN_GAME_ELEMENT> LIST_SOUND_IN_GAME;
    public void PlaySoundInGame(SOUND_IN_GAME eSound)
    {
        if (m_AudioSource)
            m_AudioSource.PlayOneShot(LIST_SOUND_IN_GAME[(int)eSound].auSound);
    }
    public void PlaySound(AudioClip _audio)
    {
        if (m_AudioSource)
            m_AudioSource.PlayOneShot(_audio);
    }


    //weapon fo soldier
    int _rand = 0;
    public void PlayerSoundSoldierAttack()
    {
        _rand = Random.Range(0, 3);
        if (_rand == 0)
        {
            PlaySoundInGame(SOUND_IN_GAME.attack_soldier);
        }
        else if (_rand == 1)
        {
            PlaySoundInGame(SOUND_IN_GAME.attack_soldier1);
        }
        else if (_rand == 2)
        {
            PlaySoundInGame(SOUND_IN_GAME.attack_soldier2);
        }
    }
    #endregion







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
                PlayerPrefs.SetString("sound", "mute");
            else
                PlayerPrefs.SetString("sound", "no");

            PlayerPrefs.Save();
        }
    }


    //STOP
    public void Stop()
    {
        m_AudioSource.Stop();
    }




    //HELPER
    [ContextMenu("Tự động cập nhật sfx vào list sound")]
    public void AutoUpdateSound()
    {
        int _total = System.Enum.GetNames(typeof(SOUND_IN_GAME)).Length;
        LIST_SOUND_IN_GAME.Clear();

        for (int i = 0; i < _total; i++)
        {
            SOUND_IN_GAME_ELEMENT _temp = new SOUND_IN_GAME_ELEMENT();
            _temp.eSound = (SOUND_IN_GAME)i;
            AudioClip _audio = Resources.Load<AudioClip>("Audio/" + _temp.eSound.ToString());
            if (_audio)
                _temp.auSound = _audio;

            LIST_SOUND_IN_GAME.Add(_temp);
        }

        Debug.Log("SOUND UPDATE: DONE");
    }
}
