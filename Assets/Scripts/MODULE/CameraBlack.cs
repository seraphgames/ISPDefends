using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlack : MonoBehaviour {
    public static CameraBlack Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

}
