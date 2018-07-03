using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectLava : MonoBehaviour {
	Transform camHeight;
	public float yOffset;

	void Start () {
		camHeight = Camera.main.transform.parent;
	}

	void Update () {
		if (camHeight) {
			if (transform.position.y < camHeight.transform.position.y + yOffset) {
				GameObject.Destroy (gameObject);
			}
		}
	}
}
