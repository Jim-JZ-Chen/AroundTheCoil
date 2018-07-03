using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public TextMesh gameOverText, gameOverText2;
	public float zoomOutDistOver, zoomOutDistEnd;
	public float zoomSpeed;
	public Camera mainCam;


	bool zoomOut = false;
	float zoomOutDist = 0;






	public void Over (int leader, Color textCol) {
		gameOverText2.text = gameOverText.text = "Player " + leader + " has Won!";
		gameOverText.color = textCol;
		gameOverText.gameObject.SetActive (true);
		gameOverText2.gameObject.SetActive (true);
		zoomOutDist = zoomOutDistOver;
		zoomOut = true;
	}

	public void End () {
		StartCoroutine (GameEnd ());
	}

	IEnumerator GameEnd () {
		yield return new WaitForSeconds (5);
		Application.LoadLevel (Application.loadedLevel);
	}

	void Update () {
		if (zoomOut) {
			if (mainCam.orthographicSize < zoomOutDist - zoomSpeed) {
				mainCam.orthographicSize += zoomSpeed;
			} else if (mainCam.orthographicSize > zoomOutDist + zoomSpeed) {
				mainCam.orthographicSize -= zoomSpeed;
			} else {
				zoomOut = false;
			}
		}


	}
}
