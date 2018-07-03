using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEmission : MonoBehaviour {

	Renderer render;
	Color currColor;

	bool allowPulse;

	public float lerpSpeed = 1, intensity = 0.4f;

	float seed = 0;

	void Start () {
		render = GetComponent<Renderer> ();
		//StartCoroutine (AllowPulseCo ());
	}

	IEnumerator AllowPulseCo () {
		yield return new WaitForSeconds (Random.Range(0.2f,0.3f));
		currColor = render.material.GetColor("_EmissionColor");
		seed = Random.Range (0, 20f);
		allowPulse = true;

	}

	void Update () {
		if (allowPulse) {
			Color lerpedColor = Color.Lerp(currColor * (1 - intensity), currColor * (1 + intensity), Mathf.PingPong(Time.time + seed, lerpSpeed));
			render.material.SetColor("_EmissionColor", lerpedColor);
		}
	}
}
