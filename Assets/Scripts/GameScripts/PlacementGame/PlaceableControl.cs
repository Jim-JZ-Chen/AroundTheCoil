using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableControl : MonoBehaviour {
    public enum Condition { Fresh, Dying, MarriedWithKids }; // if you can think of a funnier joke for the lock in state than "Married with kids" please replace this, this is a placeholder joke
    public float maxFall;
    Transform myTransform;
   // Spawner mySpawner;
    public Condition howIFeel = Condition.Fresh;
    public float startAlive;
    float deathStart;
    //death flashes
    public bool deathsDoor = false; // if true start flashing
    public float timeBetweenFlash = 0.2f;
    float nextFlash = 0;
    bool flashOn = false;
    public float flashLength = 10f;
    MeshRenderer myRend;

    // Use this for initialization
    void Awake() {
        myTransform = GetComponent<Transform>();
        myRend = GetComponent<MeshRenderer>();
        startAlive = Time.time;
    }

  //  void GetSpawner(Spawner spawn) {
   //     mySpawner = spawn;
   // }

   public bool DeathsDoor() {
        if (howIFeel == Condition.Fresh)
        {
            //im now dying
            deathsDoor = true;
            howIFeel = Condition.Dying;
            deathStart = Time.time;
        }
        else if (howIFeel == Condition.Dying && Time.time > deathStart + flashLength) {
            if (GetComponent<Rigidbody>() != null)
            {
                // just kill me already GOD
                OnDeath();
                return true;
            }
            else {
                howIFeel = Condition.MarriedWithKids; // congratulations!!
            }


        }
        return false;
    }

    //this is where we say fair well to our loveable placeble control
    void OnDeath() {
        //do stuff to make it seem neat

        //then die
        Destroy(gameObject);
    }


	// Update is called once per frame
	void Update () {
        if (myTransform.position.y <= maxFall) {
            OnDeath();
        }

        if (deathsDoor) {
            if (GetComponent<Rigidbody>() == null)
            {
                deathsDoor = false;
                myRend.enabled = true;
                return;
            }
            if (Time.time > nextFlash) {
                myRend.enabled = flashOn ? false : true;
                flashOn = !flashOn;
                nextFlash = timeBetweenFlash + Time.time;
            }
        }
	}

}
