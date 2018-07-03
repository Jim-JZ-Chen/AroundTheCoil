using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

	float yHeight = 0;

	public AnimationCurve speedIncrease;

	public CameraCast cameraCast;
	public float yOffSet;

	public float lerpSpeed;
	public float speedMulti;

	float counter = 0;
	void Update () {
		counter += Time.deltaTime;

		//float currentSpeed = speedIncrease.Evaluate(counter / 

		float maxTime = speedIncrease.keys [speedIncrease.keys.Length - 1].time;


		yHeight += speedIncrease.Evaluate(counter / maxTime) * 0.01f * speedMulti;

		Vector3 pos = transform.position;
		pos.y = yHeight;


		Vector3 yPlayerCenter = cameraCast.GetPlayerCenter ();

		pos.y += yPlayerCenter.y;
		pos.y += yOffSet;

		transform.position = Vector3.Lerp(transform.position, pos, lerpSpeed * Time.deltaTime);

	}
}
