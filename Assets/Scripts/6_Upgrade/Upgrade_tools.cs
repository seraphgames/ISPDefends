using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_tools : MonoBehaviour {

    public Transform GROUP_UPGRADE_BUTTON;
	[ContextMenu("Config content for upgrade buttons")]
    public void AutoSetContentForUpgradeButton()
    {
        int _total = GROUP_UPGRADE_BUTTON.childCount;
        for (int i = 0; i < _total; i++)
        {
            GROUP_UPGRADE_BUTTON.GetChild(i).GetComponent<ButtonUpgrade>().eUpgrade
                 = (TheEnumManager.UPGRADE)i;

            string _name = ((TheEnumManager.UPGRADE)i).ToString();
            GROUP_UPGRADE_BUTTON.GetChild(i).name = _name.ToLower();
        }
        Debug.Log("DONE");
    }
}
