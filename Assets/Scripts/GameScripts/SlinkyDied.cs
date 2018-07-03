using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinkyDied : MonoBehaviour {

	public SlinkyReferences master;
	public float explosion;

	public bool died = false;

	public int playerID;

	void Start () {
		if (!PlayerPrefs.HasKey ("ArcadeMode")) {
			if (playerID != 0) {
				died = true;
				gameObject.SetActive(false);
			}
		}

	}

	public void SetDied (bool d) {
		if (!died) {
			died = d;

			if (died) {
				foreach (SpringJoint spring in master.data.GetSprings()) {
					spring.spring = 0;
				}

				foreach (Rigidbody rigid in master.data.GetRigids()) {
					rigid.isKinematic = false;
					rigid.AddForce(new Vector3( Random.Range(-explosion, explosion), Random.Range(-explosion, explosion), Random.Range(-explosion, explosion)));
				}
			}
		}
	}

	public bool HasDied () {
		return died;
	}
}
