using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TheTextSizeManager : MonoBehaviour
{
    [System.Serializable]
    public struct TEXT_SIZE
    {
        public Text txtText;
        public int iFontSize;
        public void Init()

        {
            txtText.fontSize = Screen.width / iFontSize;
        }
    }
    public List<TEXT_SIZE> LIST_TEXT;
    private void Start()
    {
        int _total = LIST_TEXT.Count;
        for (int i = 0; i < _total; i++)
        {
            LIST_TEXT[i].Init();
        }
    }

}
