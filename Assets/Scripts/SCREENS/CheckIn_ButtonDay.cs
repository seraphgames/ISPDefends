using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckIn_ButtonDay : MonoBehaviour
{
    private Animator m_animator;

    public CheckIn m_CheckIn;
    private Button buButton;
    private Text txtTitle, txtNumber;
    private Image imaIcon;
    public GameObject objTick;//da nhan qua

    public int iIndex;

    
    private void Start()
    { 
        buButton.onClick.AddListener(() => GetGift());
    }

    private void GetGift()
    {
        m_CheckIn.GetGiftCheckIn(iIndex);    
        Init();
    }



    public void Init()
    {
        m_animator = GetComponent<Animator>();
        m_animator.enabled = false;
        transform.localScale = Vector3.one;

        buButton = this.GetComponent<Button>();
        txtTitle = transform.GetChild(0).GetComponent<Text>();
        txtNumber = transform.GetChild(2).GetComponent<Text>();
        imaIcon = transform.GetChild(1).GetComponent<Image>();


        txtTitle.text = "DAY " + (iIndex+1);
        txtNumber.text =TheCheckInGiftManager.Instance.GetGift((TheCheckInGiftManager.Gift)iIndex).iValue.ToString();
        imaIcon.sprite = TheCheckInGiftManager.Instance.GetGift((TheCheckInGiftManager.Gift)iIndex).sprIcon;

        if (iIndex < m_CheckIn.iNumberOfGiftsReceived)
        {
            //    buButton.image.color = Color.gray;
            //    objTick.SetActive(false);
            objTick.SetActive(true);
        }
        //}
        else if (iIndex == m_CheckIn.iNumberOfGiftsReceived)
        {
           // buButton.image.color = Color.white;
            objTick.SetActive(false );
            m_animator.enabled = true ;
        }
        else
        {
            buButton.image.color = Color.white;
            objTick.SetActive(false);
          
        }
    }
}
