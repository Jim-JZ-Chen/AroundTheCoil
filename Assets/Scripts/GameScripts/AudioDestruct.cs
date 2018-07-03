using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestruct : MonoBehaviour {

	public AudioSource source;
	bool allowDestroy = false;

	void Start () {
		StartCoroutine (SetEnable ());
	}

	IEnumerator SetEnable () {
		yield return new WaitForSeconds (0.5f);
		allowDestroy = true;
	}

	void Update () {
		if (allowDestroy) {
			if (!source.isPlaying)
				GameObject.Destroy (gameObject);

		}
	}
}
