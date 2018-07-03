using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinkyInit : MonoBehaviour {

	public SlinkyReferences master;

	[Header("Controls")]
	public float mass;
	public float springyness;



	void Start () {
		SetupSlinky ();
	}

	void SetupSlinky () {
		master.data.LoadInNodes ();
		setupRigid (GetNodes());
		master.flip.setControlNode (master.data.GetFirstNode (), master.data.GetFirstNodeIndex(), 1);
		master.flip.FreezeNode (master.data.GetLastNodeIndex ());
	}





	List<GameObject> GetNodes () {
		return master.data.GetNodes();
	}




	void setupRigid (List<GameObject> nodes) {
		for (int nodeIndex = 0; nodeIndex < nodes.Count; nodeIndex++) {
			GameObject node = nodes [nodeIndex];

			Rigidbody rigid = node.AddComponent<Rigidbody> ();
			master.data.AddRigid (rigid);
			rigid.isKinematic = false;
			rigid.mass = mass;

			if (nodeIndex != 0) {
				SpringJoint spring = node.AddComponent<SpringJoint> ();
				master.data.AddSpring (spring);
				spring.enableCollision = true;
				spring.spring = springyness;
				spring.connectedBody = nodes [nodeIndex - 1].GetComponent<Rigidbody> ();
			}

			//hinge.connectedBody = nodeIndex == 0 ? rigid : nodes [nodeIndex - 1].GetComponent<Rigidbody> ();

		}
	}



}
