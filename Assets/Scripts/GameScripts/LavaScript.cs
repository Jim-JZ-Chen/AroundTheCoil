using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// i probably am going to look into changing how intensity and colour changes work
public class LavaScript : MonoBehaviour {

    //references
    Renderer rend;
    //Colour values
    Color pulseMin;// Lava colour is 200 green, -3 intensity (emissions) We start as this one
    Color pulseMax; // Lava colour is 0 green, 10 intensity (emissions)

    //bools
    public bool allowPulse = true; // if there is pulsating allowed
    //value changes
    public float redChange = 0.5f;
    public float greenChange = 0f;
    public float blueChange = 0f;
    public float intensityChange = 2f;

    // randomised floats
    float lerpSpeed;
    float seed;
    public int element = 1; // where the material is in the list

    void Start()
    {
        rend = GetComponent<Renderer>();
        pulseMin = rend.materials[element].GetColor("_EmissionColor");
        pulseMax = new Color(pulseMin.r + redChange, pulseMin.g + greenChange, pulseMin.b + blueChange, pulseMin.a - pulseMin.a);
        pulseMax = pulseMax * intensityChange;
        pulseMin = pulseMin * -intensityChange;

        seed = Random.Range(0.1f, 4f);
        lerpSpeed = Random.Range(3f, 7f);
    }

    void Update()
    {
        if (allowPulse)
        {
            float lerpTime = Mathf.PingPong(Time.time + seed, lerpSpeed) / lerpSpeed;
            Color lerpCol = Color.Lerp(pulseMax, pulseMin, lerpTime);
            rend.materials[element].SetColor("_EmissionColor", lerpCol);
        }
    }
}
