using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour {

	public GameObject nextZone, thisZone;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "SlinkySegment") {
			nextZone.SetActive (true);
			thisZone.SetActive (false);
		}
	}
}
