using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class TheStartingWaveMark : MonoBehaviour
{

    public TextMesh m_textMesh;
    private float fCountTime, fTime = 11.0f;

    public bool IsCountComplete;



    private void Start()
    {
        m_textMesh.text = "";
        m_textMesh.fontSize = 130;
        GetComponent<SortingGroup>().sortingOrder = 100;


        TheLevel.Instance.LIST_STARTING_MARK.Add(this);
        gameObject.SetActive(false);
    }




    private void Update()
    {
        if (TheLevel.Instance.iCurrentWave == -1) return;

        if (fCountTime >= 1)
        {
            fCountTime -= Time.deltaTime;
            m_textMesh.text = ((int)fCountTime).ToString();
        }
        else
        {
            IsCountComplete = true;
            TheEventManager.PostGameEvent_OnTouchWaveMark(this);//touch wave mark
            gameObject.SetActive(false);
        }

    }


    private void OnMouseDown()
    {
        if (ThePopupManager.Instance.IsShowing) return;

        if (TheLevel.Instance.iCurrentWave == -1)
        {
            fCountTime = 1.0f;

            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TheObjPoolingManager.Instance.sprCallWave_Empty;
            m_textMesh.transform.localScale = Vector3.one * 1.5f;
        }

        TheEventManager.PostGameEvent_OnTouchWaveMark(this);//touch wave mark
    }


    private void OnEnable()
    {
        IsCountComplete = false;
        fCountTime = fTime;

        if (TheLevel.Instance.iCurrentWave >= 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TheObjPoolingManager.Instance.sprCallWave_Empty;
            m_textMesh.transform.localScale = Vector3.one * 1.5f;
        }
        else
        {
            StartCoroutine(SetButtonSpriteForCallWave());
        }
    }



    private IEnumerator SetButtonSpriteForCallWave()
    {
        yield return new WaitUntil(() => TheObjPoolingManager.Instance != null);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TheObjPoolingManager.Instance.sprCallWave_Skull;
    }


}
