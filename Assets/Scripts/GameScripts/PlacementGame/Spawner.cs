using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    //public variables
    public List<Vector3> spawnPoints;
    public List<GameObject> placeables = new List<GameObject>();
    public int maxPlaceables;
    public float timeAlive;
    public LayerMask onlyPlaceables;
    // prefabs
    GameObject naught;
    GameObject cross;

    //Timer stuff
    float targetTime = 0;// seconds
    public float spawnInbetweenTime;

    //misc variables
    bool lastCross;

    // Use this for initialization
    void Start() {
        naught = Resources.Load("Prefab/NaughtCross/Naught") as GameObject;
        cross = Resources.Load("Prefab/NaughtCross/Cross") as GameObject;
        // targetTime = Time.time + spawnInbetweenTime;
        TimerTick();
        TimerTick();
    }

    // Update is called once per frame
    void Update() {
        if (Time.time >= targetTime) {
            TimerTick();
        }


    }

    //this is here so that we can spawn an object as soon as it locks in!
    void TimerTick()
    {
        targetTime = Time.time + spawnInbetweenTime;
        //check our places to see if we should deaths door something
        List<int> removals = new List<int>();
        for (int i = 0; i < placeables.Count; i++)
        {
            if (placeables[i] != null)
            {
                PlaceableControl reference = placeables[i].GetComponent<PlaceableControl>();
                if (reference.howIFeel != PlaceableControl.Condition.MarriedWithKids && Time.time > reference.startAlive + timeAlive)
                {
                    if (reference.DeathsDoor())
                    {
                        removals.Add(i);
                    }
                }
            }
            else { removals.Add(i); }
        }
        //remove
        foreach (int i in removals)
        {
            placeables.RemoveAt(i);
        }
        //

        //call the thing
        if (maxPlaceables > placeables.Count) {
            DecideAndSpawn();
        }
    }

    void DecideAndSpawn() {
        float seed = Random.Range(0, 5);
        int spawnPoint = Mathf.RoundToInt(Random.Range(0, spawnPoints.Count));
        int panicCounter = 0;
        while (Physics.Raycast(spawnPoints[spawnPoint], Vector3.down, Mathf.Infinity, onlyPlaceables)) {
            spawnPoint = Mathf.RoundToInt(Random.Range(0, spawnPoints.Count));
            panicCounter++;
            if (panicCounter > spawnPoints.Count + 1) {
                //uh oh!
                break;
            }
        }
        GameObject toSpawn;
        if (seed > 3)
        {
            toSpawn = lastCross ? naught : cross;
            lastCross = !lastCross;
        }
        else {
            toSpawn = lastCross ? cross : naught;
            lastCross = !lastCross;
        }

        placeables.Add(GameObject.Instantiate(toSpawn, spawnPoints[spawnPoint], Quaternion.identity));
    }


}

/*public struct SpawnedReference {
     public enum Condition {Fresh,Dying,LockedIn};
    public float startAlive;
     Condition deathsDoor;
    public SpawnedReference(GameObject toSpawn) {
        control = toSpawn.GetComponent<PlaceableControl>();
        startAlive = Time.time;
        deathsDoor = Condition.Fresh;
    }

    public void DeathsDoor() {
        Debug.Log("im dying but i was feeling "+ deathsDoor);
        
       deathsDoor = Condition.Dying;
        //Debug.Log(deathsDoor);
       control.deathsDoor = true;
    }

   public  Condition PatientIsFeeling() {
        return deathsDoor;
    }

    public bool FinalNail() {
       Debug.Log("Called");
        if (control.deathsDoor == true) {
            return true;
        }
        deathsDoor = Condition.LockedIn;
        return false;
    }

}//*/
