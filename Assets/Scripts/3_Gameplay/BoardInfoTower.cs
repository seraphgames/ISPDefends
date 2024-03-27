using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoardInfoTower : MonoBehaviour
{
    public Image iImageIconTower;
    public Text txtRange, txtSpeedAttack, txtDamage, txtTowerName;
    public Image imaStarLevel;
    public List<Sprite> LIST_STAR;

    public void ShowContent(Tower _tower)
    {
        iImageIconTower.sprite = _tower.TOWER_DATA.sprIcon;
        //iImageIconTower.GetComponentInChildren<Text>().text = TheUpgradeSystemManager.Instance.GetTowerInfo(eTower).strTowerName;
        //txtDistance.text = _distance;
        //txtRange.text = _range;
        //txtDamage.text = _damage;

        txtTowerName.text = _tower.TOWER_DATA.strTowerName;
        txtRange.text = _tower.fCurrentRange.ToString() + " m";
        txtSpeedAttack.text = _tower.fCurrentFireRate.ToString() + " s";
        txtDamage.text = (_tower.iCurrentDamage * _tower.TOWER_DATA.GetNumberOfBullet(_tower.eTowerLevel)).ToString();

        // txtHP.text = _towerdata.iHP.ToString();
        ShowLevelStar((int)_tower.eTowerLevel);

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


    private void ShowLevelStar(int _level)
    {
        imaStarLevel.sprite = LIST_STAR[_level];
    }

}
