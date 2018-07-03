using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinkyController : MonoBehaviour {

	public SlinkyReferences master;


	bool flyMode;

	[Header("Controls")]
	public float influence;
	public float fly;

	[Header("References")]
	public AudioClip[] clips;


	float slinkCounter = 0;
	public Vector2 slinkCooldown;
	float slinkCooldownSet;
	public Vector2 slinkPitch;


	void Start () {
		RandomCooldown ();
	}

	void RandomCooldown () {
		slinkCooldownSet = Random.Range (slinkCooldown.x, slinkCooldown.y);
	}

	public void Move (Vector2 input) {

		int nodeToMove = master.flip.getControlNodeIndex();

		Rigidbody rigid = master.data.GetRigid (nodeToMove);


		Vector3 dir = new Vector3 (input.x, 0, input.y);

		int xDir = dir.x != 0 ? (dir.x < 0 ? -1 : 1) : 0;
		int zDir = dir.z != 0 ? (dir.z < 0 ? -1 : 1) : 0;
	

		rigid.AddForce (AngleToCamRight() * influence * xDir);
		rigid.AddForce (AngleToCamForward() * influence * zDir);

		rigid.AddTorque (AngleToCamForward() * 5 * -xDir);
		rigid.AddTorque (AngleToCamRight() * 5 * zDir);

		if (!flyMode) rigid.AddForce (Vector3.up * -fly * 0.3f);
		//Debug.Log (flyMode + gameObject.name);

		if (slinkCounter > slinkCooldownSet) {
			if (master) {
				if (master.master) {
					if (master.master.sounder) {
						master.master.sounder.PlaySound (clips [Random.Range (0, clips.Length)], 1, Random.Range (slinkPitch.x, slinkPitch.y));
					}

				}
			}
			slinkCounter = 0;
			RandomCooldown ();
		}
	}


	public void Fly () {
		flyMode = true;
		int nodeToMove = master.flip.getControlNodeIndex();

		Rigidbody rigid = master.data.GetRigid (nodeToMove);

		rigid.AddForce (Vector3.up * fly * 4);
	}

	public void NoFly () {
		flyMode = false;
	}


	Vector3 AngleToCamForward () {
		return Camera.main.transform.forward;
	}

	Vector3 AngleToCamRight () {
		return Camera.main.transform.right;
	}


	void Update () {
		slinkCounter += Time.deltaTime;



	}


}
