using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Blink : MonoBehaviour {

	public VignetteAndChromaticAberration vignette;
	public AnimationCurve blinkCurve;
	public float blinkTime;


	public void BlinkOnce () {
		StartCoroutine (BlinkOnceCoroutine ());
	}

	IEnumerator BlinkOnceCoroutine () {
		float counter = 0;	
		vignette.intensity = 0;
		while (counter < blinkTime) {
			vignette.intensity = blinkCurve.Evaluate (counter / blinkTime);
			Debug.Log (blinkCurve.Evaluate (counter / blinkTime) + " T");
			counter += Time.deltaTime;
			yield return null;
		}
		vignette.intensity = 0;
	}




	void Update () {
		if (Input.GetKeyDown (KeyCode.Y)) {
			BlinkOnce ();
		}

	}
}
