using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{

    public static Data inst;
    public int currLevel;
    public int currStage;

    private void Awake()
    {
        if (Data.inst != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
