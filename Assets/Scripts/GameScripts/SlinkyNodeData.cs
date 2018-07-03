using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinkyNodeData : MonoBehaviour {

	int nodeIndex;

	public void SetIndex (int index) {
		nodeIndex = index;
	}

	public int GetIndex () {
		return nodeIndex;
	}
}
