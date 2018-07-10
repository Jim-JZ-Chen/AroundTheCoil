using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour {

    // Use this for initialization
    public float lifeTime;
	void Awake () {
        Invoke("SelfDestruct", lifeTime);
	}

    void SelfDestruct() {

        Destroy(gameObject);

    }
}
