using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCast : MonoBehaviour {

	public SlinkyReferences[] slinkyMasters;

	public float speed;

	Vector3 playerCenterPos = new Vector3();

	void Update () {
	//	transform.position

		int slinkies = 0;
		Vector3 target = new Vector3();
		foreach (SlinkyReferences slinky in slinkyMasters) {
			if (!slinky.died.HasDied()) {
				slinkies++;
				target += slinky.data.GetCenterNode ().transform.position;
			}
		}


		if (slinkies > 0) {
			target /= slinkies;

			playerCenterPos = target;

			Quaternion targetRotation = Quaternion.LookRotation (transform.position - target);

			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);

			Vector3 ang = transform.eulerAngles;
			ang.x = 0;
			transform.eulerAngles = ang;
		}


	}

	public Vector3 GetPlayerCenter () {
		return playerCenterPos;
	}
}
