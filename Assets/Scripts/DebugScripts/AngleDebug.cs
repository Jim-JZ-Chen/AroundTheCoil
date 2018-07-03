using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleDebug : MonoBehaviour {

	void Update () {

		Debug.Log ( Vector3.Angle(transform.up, Vector3.up) );






	}
}
