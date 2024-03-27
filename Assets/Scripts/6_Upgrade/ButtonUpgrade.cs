using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgrade : MonoBehaviour
{

    public TheEnumManager.UPGRADE eUpgrade;

    [HideInInspector]
    public Sprite sprIconImageWhite;
    public Sprite sprIconImageGray;


    public UpgradeConfig m_UpgradeConfig;
    private Button buButton;
    private Image imaIconStar;
    private int iPrice;

    private void Awake()
    {

    }
    private void Start()
    {
        buButton = GetComponent<Button>();
        sprIconImageWhite = buButton.image.sprite;
        buButton.onClick.AddListener(() => ThisButton());


        m_UpgradeConfig = TheDataManager.Instance.UPGRADE_DATA_MANAGER.Get(eUpgrade);
        buButton.transform.GetChild(0).GetComponent<Text>().text = m_UpgradeConfig.iStarPrice.ToString();

        
        SetStatus(m_UpgradeConfig.ACTIVED);
        buButton.GetComponentInChildren<Text>().text = m_UpgradeConfig.iStarPrice.ToString();

        if (eUpgrade == 0)
        {
            Invoke("ThisButton", 0.1f);
        }
        buButton.GetComponentInChildren<Text>().text = m_UpgradeConfig.iStarPrice.ToString();


        //icon star
        imaIconStar = transform.GetChild(1).GetComponent<Image>();
        switch (m_UpgradeConfig.eStarType)
        {
            case TheEnumManager.STAR_TYPE.white:
                imaIconStar.sprite = MainCode_Upgrade.Instance.sprStar_Normal;
                break;
            case TheEnumManager.STAR_TYPE.blue:
                imaIconStar.sprite = MainCode_Upgrade.Instance.sprStar_Hard;
                break;
            case TheEnumManager.STAR_TYPE.yellow:
                imaIconStar.sprite = MainCode_Upgrade.Instance.sprStar_Nightmate;
                break;

        }
    }


    public Button GetButton()
    {
        return buButton;
    }
   


    private void ThisButton()
    {
        MainCode_Upgrade.Instance.SetUpgrade(this);
    }


    public void Upgrade(bool _active)
    {
        m_UpgradeConfig.ACTIVED=_active;

        SetStatus(_active);
    }





    private void SetStatus(bool _active)
    {
        if (_active)
            buButton.image.sprite = sprIconImageWhite;
        else
            buButton.image.sprite = sprIconImageGray;
    }

    
}
