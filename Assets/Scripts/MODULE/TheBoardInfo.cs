using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheBoardInfo : MonoBehaviour, IButton
{

    private Text txtValue;
    public TheEnumManager.BOARD_INFO eBoardInfo;
    private Button buButton;

    // Use this for initialization
    void Awake()
    {
        txtValue = GetComponentInChildren<Text>();
        if (eBoardInfo == TheEnumManager.BOARD_INFO.GemBoard)
        {
            buButton = GetComponent<Button>();
            buButton.onClick.AddListener(() => SetButton(buButton));
        }
    }


    public void SetButton(Button _bu)
    {
        if (_bu == buButton)
        {
            ThePopupManager.Instance.Show(ThePopupManager.POP_UP.Shop);
            Shop.Instane.ShowPanel(Shop.PANEL.Gems);
        }
    }




    private void UpdateBoard()
    {
        switch (eBoardInfo)
        {
            case TheEnumManager.BOARD_INFO.GemBoard:
                txtValue.text = TheDataManager.THE_PLAYER_DATA.GEM.ToString();
                break;
            case TheEnumManager.BOARD_INFO.CoinBoard:
                if (txtValue && TheLevel.Instance)
                    txtValue.text = TheLevel.Instance.iOriginalCoin.ToString();
                break;
            //case TheEnumManager.BOARD_INFO.WaveBoard:
            //    if (TheLevel.Instance)
            //        txtValue.text ="WAVE:    "+ TheLevel.Instance.iCurrentWave.ToString()+"/"+ TheLevel.Instance.iMAX_WAVE_CONFIG;
            //    break;
            //case TheEnumManager.BOARD_INFO.HeartBoard:
            //    if (TheLevel.Instance && TheLevel.Instance.iCurrentHeart>=0)
            //        txtValue.text = TheLevel.Instance.iCurrentHeart.ToString();
            //    break;


            case TheEnumManager.BOARD_INFO.StarToUpgradeBoard_Normal:
                string _content1 = TheDataManager.Instance.UPGRADE_DATA_MANAGER.GetTotalStarWasUsed(TheEnumManager.STAR_TYPE.white) + "/" + TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Normal).ToString();
                txtValue.text = _content1;
                break;

            case TheEnumManager.BOARD_INFO.StarToUpgradeBoard_Hard:
                string _content2 = TheDataManager.Instance.UPGRADE_DATA_MANAGER.GetTotalStarWasUsed(TheEnumManager.STAR_TYPE.blue) + "/" + TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Hard).ToString();
                txtValue.text = _content2;
                break;

            case TheEnumManager.BOARD_INFO.StarToUpgradeBoard_Nightmate:
                string _content3 = TheDataManager.Instance.UPGRADE_DATA_MANAGER.GetTotalStarWasUsed(TheEnumManager.STAR_TYPE.yellow) + "/" + TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Nightmate).ToString();
                txtValue.text = _content3;
                break;



            case TheEnumManager.BOARD_INFO.TotalStarBoard_Normal:
                txtValue.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Normal).ToString();
                break;
            case TheEnumManager.BOARD_INFO.TotalStarBoard_Hard:
                txtValue.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Hard).ToString();
                break;
            case TheEnumManager.BOARD_INFO.TotalStarBoard_Nightmate:
                txtValue.text = TheDataManager.THE_PLAYER_DATA.GetTotalStar(TheEnumManager.DIFFICUFT.Nightmate).ToString();
                break;
        }
    }

    private void OnEnable()
    {
        UpdateBoard();
        TheEventManager.OnUpdateBoardInfo += UpdateBoard;
    }
    private void OnDisable()
    {
        TheEventManager.OnUpdateBoardInfo -= UpdateBoard;
    }


}
