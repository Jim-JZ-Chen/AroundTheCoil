using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinkyData : MonoBehaviour {

	List<GameObject> nodes = new List<GameObject>();
	List<Rigidbody> rigids = new List<Rigidbody>();
	List<SpringJoint> springs = new List<SpringJoint>();
	List<SlinkySensors> sensors = new List<SlinkySensors>();

	[HideInInspector] public bool loadedNodes = false;


	public void LoadInNodes () {
		int childIndex = 0;
		foreach (Transform child in transform) {
			if (child.tag != "Eye") {
				nodes.Add (child.gameObject);
				sensors.Add (child.GetComponent<SlinkySensors> ());
				child.GetComponent<SlinkyNodeData> ().SetIndex (childIndex);
				childIndex++;
			}
		}
		loadedNodes = true;
	}

	public GameObject GetNode (int index) {
		return nodes[index];
	}

	public List<GameObject> GetNodes () {
		return nodes;
	}

	public int GetFirstNodeIndex () {
		return 0;
	}

	public GameObject GetFirstNode () {
		return nodes [0];
	}

	public GameObject GetCenterNode () {
		return nodes [Mathf.RoundToInt(GetNodeCount() / 2)];
	}

	public GameObject GetLastNode () {
		return nodes [GetNodeCount () - 1];
	}

	public int GetLastNodeIndex () {
		return GetNodeCount () - 1;
	}

	public int GetNodeCount () {
		return nodes.Count;
	}

	public Rigidbody GetRigid (int index) {
		return rigids [index];
	}

	public SlinkySensors GetSensor (int index) {
		return sensors [index];
	}


	public void AddRigid (Rigidbody rigid) {
		rigids.Add (rigid);
	}

	public List<SpringJoint> GetSprings () {
		return springs;
	}

	public List<Rigidbody> GetRigids () {
		return rigids;
	}

	public void AddSpring (SpringJoint spring) {
		springs.Add (spring);
	}

}
