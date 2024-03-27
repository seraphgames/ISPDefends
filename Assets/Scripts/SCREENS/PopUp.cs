using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopUp : MonoBehaviour
{
    [SerializeField] ThePopupManager.POP_UP ePopUp;
    public ThePopupManager.POP_UP ePOPUP
    {
        get
        {
            return ePopUp;
        }
    }


    [SerializeField] Button buClose;


    private bool isShowing;
    public bool IS_SHOWING
    {
        get
        {
            return isShowing;
        }
    }







    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (buClose != null)
            buClose.onClick.AddListener(() => SetButton(buClose));
    }


    protected virtual void SetButton(Button _bu)
    {
        if (_bu == buClose)
        {
            ThePopupManager.Instance.Hide(ePopUp);
        }
    }


    public void Active(bool _active)
    {
        this.gameObject.SetActive(_active);
    }



    protected virtual void OnEnable()
    {
        isShowing = true;
    }

    protected virtual void OnDisable()
    {
        isShowing = false;
    }


}
