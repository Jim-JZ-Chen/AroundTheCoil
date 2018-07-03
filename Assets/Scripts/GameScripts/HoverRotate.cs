using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverRotate : MonoBehaviour {

	public float maxFloatHeight = 1.7f;// added to starting transform
	public float minFloatHeight =1; // removed from starting transform
	public float yRotSpeed =0.6f;
	public float floatSpeed = 0.4f;
	public bool xRotenabled = true;

	bool allowChange = false;
	float ranSpeedMulti;
	float rotDir = -1;

	private float baseTransform;
	private bool goingUP = true;
	void Start(){
		baseTransform = transform.position.y;
		ranSpeedMulti = Random.Range (0.8f, 1.2f);
		rotDir = Random.Range (0, 2) == 1 ? -1 : 1;
		StartCoroutine(StartAllow ());
	}

	IEnumerator StartAllow () {
		yield return new WaitForSeconds (Random.Range (0, 4f));
		allowChange = true;
	}

	// Update is called once per frame
	void Update () {
		if (allowChange) {
			float yVal = goingUP ? baseTransform + maxFloatHeight : baseTransform - minFloatHeight;
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, yVal, transform.position.z), (floatSpeed * Time.deltaTime));
			if (Mathf.Abs (transform.position.y - yVal) < 1f) {
				goingUP = goingUP ? false : true;
			} 
			if (xRotenabled) {
				transform.Rotate (0, yRotSpeed * ranSpeedMulti * rotDir, 0);
			}
		}
	}
}
