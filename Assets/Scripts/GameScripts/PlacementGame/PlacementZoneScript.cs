using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementZoneScript : MonoBehaviour {

    //references
    public BoardController boardControl;
    public GameObject myKey; // this is used in case we want a specific item placed here
    public LayerMask grabbableMask;
    public GameObject currentKey;
    Transform myTransform;
    //variables
    bool locked = false; // if true we dont accept any new keys
    Vector3 lockIn;
    public float cineSmooth = 10f;
    BoardPlace.BoardFill fillReturn;


	// Use this for initialization
	void Start () {
        myTransform = GetComponent<Transform>();
        lockIn = GetComponent<BoxCollider>().bounds.center;

	}

    public void RecieveController(BoardController boardControl) {
        this.boardControl = boardControl;
    }


    void StartSetting(GameObject key)
    {
        //check we have the right key
        if (myKey != null) {
            if (key != myKey) {
                print("ejected");
                return;
            }
        }
        locked = true;
        currentKey = key;
        //destroy stuff
        if (key.GetComponent<Joint>() != null) {
            Destroy(key.GetComponent<Joint>());
        }
        key.GetComponent<Collider>().enabled = false;
        Destroy(key.GetComponent<Rigidbody>());
        //cinematically move object into placement area
        StartCoroutine("CineMove", key);
        //tel the controller the big news
        boardControl.PlacementUpdate(fillReturn, myTransform.position);

    }

    public void Wipe() {
        if (currentKey != null) {
            Destroy(currentKey);
            fillReturn = BoardPlace.BoardFill.none;
            locked = false;
            boardControl.PlacementUpdate(fillReturn, myTransform.position);
        }
    }


    //change this to not flip keys over
    IEnumerator CineMove(GameObject key) {
        //rotate and move the key to be alligned with lockIn and this objects
        Transform keyTransform = key.transform;
        bool rotDone = false;
        bool movDone = false;
        while (true) {

            keyTransform.position = Vector3.Lerp(keyTransform.position, lockIn, cineSmooth* Time.deltaTime);
            keyTransform.rotation = Quaternion.Lerp(keyTransform.rotation, myTransform.rotation, 10*  Time.deltaTime);
            //check that we're close enough
            if (Mathf.Abs(keyTransform.rotation.eulerAngles.y - myTransform.rotation.eulerAngles.y) < 1f) {
                keyTransform.rotation = transform.rotation;
                rotDone = true;
            }
            if (Vector3.Distance(keyTransform.position, lockIn) < 0.5f) {
                keyTransform.position = lockIn;
                movDone = true;
            }
            if (movDone && rotDone) {
                break;
            }
            yield return null; 
        }
    }



    bool ValidRotation(GameObject key) {

        float angleCheck = Vector3.Angle(Vector3.up, key.transform.up);

        if (angleCheck < 15 || 180 - angleCheck <15) {
            return true;
        }
        return false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!locked)
        {
            GameObject key = collision.gameObject;
            if (key.CompareTag("Naught"))
            {
                fillReturn = BoardPlace.BoardFill.Naught;
                //check rotation
                if (ValidRotation(key))
                {
                    StartSetting(key);
                }
            }
            else if (key.CompareTag("Cross"))
            {    //check rotation
                if (ValidRotation(key))
                {
                    fillReturn = BoardPlace.BoardFill.Cross;
                    StartSetting(key);
                }
            }
        }
    }
}
