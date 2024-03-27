using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheElectric : MonoBehaviour
{

   [SerializeField]
    private LineRenderer m_lineRenderer;
    [SerializeField]
    private GameObject objLineRender;

    private Vector2[] LIST_POS;
    // Use this for initialization
    void Awake()
    {
        LIST_POS = new Vector2[2];

        objLineRender = gameObject;
        m_lineRenderer = GetComponent<LineRenderer>();
        LIST_POS[0] = transform.position;
    }



    public void ShowElectric(Vector2 _enemy)
    {
        objLineRender.SetActive(true);
        LIST_POS[1] = _enemy;
        m_lineRenderer.SetPosition(0, LIST_POS[0]);
        m_lineRenderer.SetPosition(1, LIST_POS[1]);
      
        Invoke("TurnOffLine", 0.13f);
    }

    private void TurnOffLine()
    {
        objLineRender.SetActive(false );
    }
}
