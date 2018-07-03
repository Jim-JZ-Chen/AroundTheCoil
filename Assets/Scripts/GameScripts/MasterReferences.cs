using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterReferences : MonoBehaviour {

	[Header("Script References")]
	public SoundGen sounder;
	public Score scorer;
	public GameOver gameOver;

	[Header("Object References")]
	public Transform camPos;
}
