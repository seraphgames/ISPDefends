
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLife : MonoBehaviour
{
    public enum ACTIVE
    {
        DESTROY,
        ACTIVE,
    }
    public ACTIVE eActive;
    public float fTimelife;
    public bool bUnscaleTime;

    // Use this for initialization
    void OnEnable()
    {
        if (bUnscaleTime)
            StartCoroutine(Wait(fTimelife * Time.timeScale));
        else
            StartCoroutine(Wait(fTimelife));
    }

    private IEnumerator Wait(float _time)
    {
        yield return new WaitForSeconds(_time);
        switch (eActive)
        {
            case ACTIVE.DESTROY:
                Destroy(this.gameObject);
                break;
            case ACTIVE.ACTIVE:
                gameObject.SetActive(false);
                break;

        }
    }
}
