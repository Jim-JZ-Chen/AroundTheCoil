using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlinkyFlip : MonoBehaviour {

	public SlinkyReferences master;

	int slinkyMode = 0; //0 = first node, 1 = last node
	GameObject lastControlledNode;
	GameObject controlledNode;
	int lastControlledNodeIndex = 0;
	int controlledNodeIndex = 0;

	public float switchCooldown = 0.2f;
	float switchCounter;

	public List<bool> collisionList; 

	public LayerMask collisionMask = -1;

	public float player;

	public LayerMask grabMask = -1; // make this just the grabbable layer
	public bool grabbing; // this is to check if we are trying to grab something
	private bool holding; // this is so we know if we have a precious friend in our arms


	public GameObject connectParticles;


	// new functions
	public void setControlNodeGrabbing(bool input){
		grabbing = input;
		if (holding && !grabbing) {
			holding = false;
			//let go function
			Transform temp = controlledNode.transform.GetChild (0);
			temp.transform.SetParent(null);
			temp.gameObject.AddComponent<Rigidbody> ();
		}
	}


	void GrabObject(GameObject grabbed){
		//print("grabbable touched");
		grabbed.transform.SetParent(controlledNode.transform);
		Object.Destroy (grabbed.gameObject.GetComponent<Rigidbody> ());
		//baby = hit.collider.gameObject;
		holding = true;
	}



	public void setControlNode (GameObject node, int nodeIndex, int mode) {
		if (controlledNode) ChangeFromNode (controlledNodeIndex);

		slinkyMode = mode;
		lastControlledNode = controlledNode;
		controlledNode = node;
		setControlNodeIndex (nodeIndex);

		if (controlledNode)	ChangeToNode (controlledNodeIndex);
	}


	public GameObject getControlNode () {
		return controlledNode;
	}

	public int getControlNodeIndex () {
		return controlledNodeIndex;
	}

	public void setControlNodeIndex (int input) {
		lastControlledNodeIndex = controlledNodeIndex;
		controlledNodeIndex = input;
	}

	void Update () {
		if (controlledNode) {
			//logic for working if its flipped

			switchCounter += Time.deltaTime;




			if (collisionList[0] || collisionList [master.data.GetLastNodeIndex()]) {
			//	CheckNodeCollision ();
			}


			RaycastHit hit;
			Vector3 rayDir = slinkyMode == 0 ? -controlledNode.transform.up :controlledNode.transform.up ;
			Debug.DrawRay (controlledNode.transform.position, rayDir * 1f, Color.green);
			Debug.DrawRay (controlledNode.transform.position, -rayDir * 1f, Color.red);

			if ((Physics.Raycast (controlledNode.transform.position, rayDir, out hit, 1f, collisionMask.value) || Physics.Raycast (controlledNode.transform.position, -rayDir, out hit, 1f, collisionMask.value))&& !grabbing ) {
//				Debug.Log(hit.collider.gameObject.tag);
				CheckNodeCollision (hit.normal);
			}
			else if((Physics.Raycast (controlledNode.transform.position, rayDir, out hit, 0.5f, grabMask.value) || Physics.Raycast (controlledNode.transform.position, -rayDir, out hit, 0.5f, grabMask.value))&& grabbing){
				//check if grabbing
				GrabObject(hit.collider.gameObject);
			}

			//Debug.Log (controlledNode.transform.eulerAngles);

		}
	}

	void CheckNodeCollision (Vector3 norm) {
		float angle = slinkyMode == 0 ? Vector3.Angle(controlledNode.transform.up, Vector3.up) : 180 - Vector3.Angle(controlledNode.transform.up, Vector3.up);
		if (angle > 15 && angle < 180 - 15)	return;
		Switch (norm);
	}


	public void NodeCollision (int nodeIndexCollision) {
		collisionList [nodeIndexCollision] = true;
		StartCoroutine(TurnCollisionOff(nodeIndexCollision));
	}

	/*
	public void NodeCollisionOff (int nodeIndexCollision) {
		collisionList [nodeIndexCollision] = false;
	}*/




	IEnumerator TurnCollisionOff(int nodeIndexCollision) {
		yield return new WaitForSeconds (0.5f);
		collisionList [nodeIndexCollision] = false;
	}


	void ChangeFromNode (int previousNodeIndex) {
		FreezeNode (previousNodeIndex);
		UnlinkSensor (previousNodeIndex);

		GameObject node = master.data.GetNode (previousNodeIndex);

		if (player == 0) {
			node.GetComponent<Renderer> ().material.color = Color.white;
		} else {
			node.GetComponent<Renderer> ().material.color = Color.green;
		}

	}

	void ChangeToNode (int nextNodeIndex) {
		UnfreezeNode (nextNodeIndex);
		LinkSensor (nextNodeIndex);


		GameObject node = master.data.GetNode (nextNodeIndex);

		if (player == 0) {
			node.GetComponent<Renderer> ().material.color = Color.red;
		} else {
			node.GetComponent<Renderer> ().material.color = Color.blue;
		}
	}




	public void FreezeNode (int nodeIndexToFreeze) {
		Rigidbody rigid = master.data.GetRigid (nodeIndexToFreeze);
		rigid.isKinematic = true;
	}


	public void UnfreezeNode (int nodeIndexToUnfreeze) {
		Rigidbody rigid = master.data.GetRigid (nodeIndexToUnfreeze);
		rigid.isKinematic = false;
	}


	public void LinkSensor (int nodeIndexToLink) {
		SlinkySensors sensor = master.data.GetSensor (nodeIndexToLink);



	}


	public void UnlinkSensor (int nodeIndexToUnlink) {
		SlinkySensors sensor = master.data.GetSensor (nodeIndexToUnlink);


	}


	public void Switch (Vector3 norm) {
		if (switchCounter < switchCooldown) return;
		switchCounter = 0;


		//controlledNode.transform.rotation = norm;
		//norm.x = norm.z;
		//Debug.Log ("hit normal: " + norm);
		//controlledNode.transform.rotation = Quaternion.Euler(norm * 360) * Vector3.forward;


		if (slinkyMode == 0) {
			controlledNode.transform.eulerAngles = new Vector3 (0, 180, 0);
		} else if (slinkyMode == 1) {
			controlledNode.transform.eulerAngles = new Vector3 (0, 180, 180);
		}



		GameObject particles = Instantiate (connectParticles, controlledNode.transform);
		particles.transform.localPosition = Vector3.zero;
		particles.transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);
		ParticleSystem particleSystem = particles.GetComponent<ParticleSystem> ();
		particleSystem.Emit (30);



		int nextSlinkyMode = slinkyMode == 0 ? 1 : 0;
		if (nextSlinkyMode == 1) {
			setControlNode (master.data.GetFirstNode (), master.data.GetFirstNodeIndex(), nextSlinkyMode);
		} else if (nextSlinkyMode == 0) {
			setControlNode (master.data.GetLastNode (), master.data.GetLastNodeIndex(), nextSlinkyMode);
		}
	}


	/*
	public int getMode () {
		return slinkyMode;
	}

	public void setMode (int input) {
		slinkyMode = input;
	}
	public void Switch () {
		setMode (getMode () == 0 ? 1 : 0);
	}

*/



}
