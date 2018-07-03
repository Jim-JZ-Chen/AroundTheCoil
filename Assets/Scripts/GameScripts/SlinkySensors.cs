using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinkySensors : MonoBehaviour {

	public SlinkyReferences master;

	public SlinkyNodeData nodeData;

	bool lastObjectTouching = false;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Lava") {
			master.died.SetDied(true);
		}
	}

	/*void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "SlinkySegment") {
			if (col.gameObject.layer != gameObject.layer) {
//				col.gameObject.transform.parent.GetComponent<SlinkyFlip> ().GetHit ();
				Debug.Log (gameObject.name + " hit " + col.gameObject.name);
			}
			//master.flip
		//	master.flip.NodeCollision (nodeData.GetIndex ());
		//	lastObjectTouching = true;
		}
	}*/

	/*
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag != "SlinkySegment") {
			//master.flip
			master.flip.NodeCollision (nodeData.GetIndex ());
			lastObjectTouching = true;
		}
	}*/

	/*void OnCollisionExit () {
		if (lastObjectTouching) {
			lastObjectTouching = false;
			master.flip.NodeCollisionOff (nodeData.GetIndex ());
		}
	}*/



}
