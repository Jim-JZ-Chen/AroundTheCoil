using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerType : MonoBehaviour {

	public TextType textType;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "SlinkySegment") {
			textType.StartTyping ();
		}
	}
}
