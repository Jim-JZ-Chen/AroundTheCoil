using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MenuManager.inst.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.inst.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
	}
}
