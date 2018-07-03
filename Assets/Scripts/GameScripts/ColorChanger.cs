using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {
	public GameObject[] players;
	List<SlinkBody> playerBods = new List<SlinkBody> ();
	List<int> playerColIndex = new List<int>();
	Color[] colours = { Color.blue,Color.white, Color.grey, Color.magenta, Color.green, Color.yellow, Color.red };
	// Use this for initialization
	void Start () {
		int temp = 0;
		foreach (GameObject player in players) {

			playerBods.Add(new SlinkBody (player));
			playerColIndex.Add (temp);
			temp++;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.F)) {
			for (int i = 0; i < players.Length; i++) {
				ColUpdate (i, +1);
			}
		}
	}

	void ColUpdate(int player, int increment){
		playerColIndex[player] += increment;
		//GRUBBY IF STATMENTS PLEASE DONT LOOK AT THESE!
		if ((player == 0 || player == 3) && playerColIndex [player] == colours.Length - 1) {
			playerColIndex [player] = 0;
		}

		if ((player == 1 || player == 2) && playerColIndex [player] == 0) {
			playerColIndex [player] = 1;
		}

		if (playerColIndex [player] > colours.Length - 1) {
			playerColIndex [player] = 0;
		}
		playerBods[player].Recolour(colours[playerColIndex [player]]);
	} 
}

public struct SlinkBody{
	List<Material> skinList;

	public SlinkBody(GameObject SlinkMaster){
		skinList = new List<Material> ();
		SlinkInitiate (SlinkMaster);
	}

	void SlinkInitiate(GameObject SlinkMaster){
		MeshRenderer[] bodRends = SlinkMaster.GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer rend in bodRends) {
			skinList.Add (rend.material);
		}
	}

	public void Recolour(Color col){
		foreach (Material mat in skinList) {
			mat.color = col;
		}
	}
}
