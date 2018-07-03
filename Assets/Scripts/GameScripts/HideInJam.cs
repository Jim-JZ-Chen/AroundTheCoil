using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInJam : MonoBehaviour {

	void Start () {
		if (!PlayerPrefs.HasKey ("ArcadeMode"))
			gameObject.SetActive (false);
		
	}

}
