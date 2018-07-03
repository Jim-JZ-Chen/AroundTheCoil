using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMatCol : MonoBehaviour {

	public Renderer render;
	public List<Material> mats; 

	void Start () {
		render = GetComponent<Renderer> ();
		render.material = mats [Random.Range(0, mats.Count)];
	}
}
