using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen : MonoBehaviour {
	
	public MasterReferences master;

	public Transform camPos;
	bool allowGenerate = true;

	[SerializeField]
	public List<Spire> spires;

	[System.Serializable]
	public struct Spire {
		public string name;
		public GameObject spirePole;
		public List<GameObject> rooms;
		public GameObject stair;
	}



	public float currentSpireHeight = 0;
	public float spireHeight = 100;

	public int roomsInEachSpire = 3;
	public float roomHeight;

	public int stairsBetweenRooms = 3;
	public float stairHeight;
	public float lastStairHeight;

	public float spireRotation = 0;
	public float roomDegrees, stairDegrees, lastStairDegrees;

	bool isFirst = true;


	void Start () {
		//for (int i = 0, i < 
		GenerateSpire ();
	}


	void GenerateSpire () {
		Spire spire = GetRandomSpire ();

		GameObject SpirePollObj = Instantiate (spire.spirePole, transform);
		Vector3 spirePollPos = new Vector3 (0, currentSpireHeight, 0);
		SpirePollObj.transform.localPosition = spirePollPos;

		GenerateRooms (spire, currentSpireHeight);

		currentSpireHeight += spireHeight;
		allowGenerate = true;
	}


	void GenerateRooms (Spire spire, float baseHeight) {


		for (int roomIndex = 0; roomIndex < roomsInEachSpire; roomIndex++) {
			
			GameObject room = Instantiate (GetRandomRoom (spire), transform);
			Vector3 roomPos = room.transform.position;
			roomPos.y = baseHeight;
			room.transform.position = roomPos;
			room.transform.eulerAngles = new Vector3 (0, spireRotation, 0);


			baseHeight += roomHeight;
			spireRotation += roomDegrees;


			//generate stairs
			for (int stairIndex = 0; stairIndex < stairsBetweenRooms; stairIndex++) {
				GameObject stair = Instantiate (GetStair (spire), transform);
				Vector3 stairPos = stair.transform.position;
				stairPos.y = baseHeight;
				stair.transform.position = stairPos;
				stair.transform.eulerAngles = new Vector3 (0, spireRotation, 0);

				spireRotation += stairDegrees;
				baseHeight += stairHeight;
				if (stairIndex == stairsBetweenRooms - 1) {
					baseHeight += lastStairHeight;
					spireRotation += lastStairDegrees;
				}
			}



		}

	}

	GameObject GetStair (Spire spire) {
		return spire.stair;
	}



	int lastRoomIndex;

	GameObject GetRandomRoom (Spire spire) {
		int getRoomIndex = Random.Range (0, spire.rooms.Count);
		while (getRoomIndex == lastRoomIndex || getRoomIndex == 0) {
			getRoomIndex = Random.Range (0, spire.rooms.Count);
		}
		lastRoomIndex = getRoomIndex;

		if (isFirst) {
			isFirst = false;
			lastRoomIndex = 0;
			return spire.rooms [0];
		}





		return spire.rooms [getRoomIndex];
	}


	Spire lastSpire = new Spire();
	Spire GetRandomSpire () {
		Spire getSpire = spires [Random.Range (0, spires.Count)];
		while (getSpire.name == lastSpire.name) {
			getSpire = spires [Random.Range (0, spires.Count)];
		}
		lastSpire = getSpire;


		return getSpire;
	}


	void Update () {

		float yCam = camPos.position.y;

		if (yCam > currentSpireHeight - 10 && allowGenerate) {
			allowGenerate = false;
			GenerateSpire ();
		}

	}





}
