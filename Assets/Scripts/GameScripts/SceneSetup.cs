using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour {

	public bool isArcadeMode;

	public GameObject gameJamScene;
	public GameObject arcadeSpawner, arcadeSpawner2;

	void Start () {
		

		if (PlayerPrefs.HasKey ("ArcadeMode")) {
			isArcadeMode = true;
		} else {
			isArcadeMode = false;
		}

		if (!isArcadeMode) {
			gameJamScene.SetActive (true);
			arcadeSpawner.SetActive (false);
			arcadeSpawner2.SetActive (false);
		} else {
			gameJamScene.SetActive (false);
			arcadeSpawner.SetActive (true);
			arcadeSpawner2.SetActive (true);
		}


	}



	void Update () {
		if (Input.GetButtonDown ("Back_1") || Input.GetButtonDown ("Back_2") || Input.GetKeyDown(KeyCode.R)) {

			if (isArcadeMode) {
				PlayerPrefs.DeleteKey ("ArcadeMode");
			} else {
				PlayerPrefs.SetInt ("ArcadeMode", 1);
			}


			Application.LoadLevel (Application.loadedLevel);
		}
	}

}
