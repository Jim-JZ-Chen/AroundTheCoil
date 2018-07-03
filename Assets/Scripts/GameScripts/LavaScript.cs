using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour {

	public Material coolLava;//put the base lava here with a 0 for the emission
	public Material hotLava; // put a new lava that has a 1 for emission
	public Renderer rend;
	float duration;
	float offSetNum;

	public int element = 2;

	void Start(){
		rend = GetComponent<Renderer> ();
		rend.materials [1] = coolLava;
		offSetNum = Random.Range (0.1f, 3.5f);
		duration = Random.Range (2.0f, 5.0f);
		//rend.material = coolLava;
		//lava = GetComponent<Material> ();
	}
	//Color baseCol = lava.GetColor(" _EmissionColor");
	void Update () {
		//lava.Lerp(
		float lerp = Mathf.PingPong(Time.time+ offSetNum ,2.0f) / 2.0f;
		rend.materials[element - 1].Lerp (coolLava, hotLava, lerp);
	}
}
