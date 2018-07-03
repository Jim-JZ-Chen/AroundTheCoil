using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerManager : MonoBehaviour
{
    public float resetDelay;
    public int scoreLeft;
    public int scoreRight;
    public Transform goalLeft;
    public Transform goalRight;
    public Transform ball;
    public bool isPlaying;
    public TextMesh scoreMesh;

    void Start () {
        isPlaying = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isPlaying)
        {
            return;
        }
        else if (ball.position.x > goalRight.position.x)
        {
            scoreLeft++;
            StartCoroutine( reset());
            
        }
        else if (ball.position.x < goalLeft.position.x)
        {
            scoreRight++;
            StartCoroutine(reset());
        }
    }

    private IEnumerator reset()
    {
        isPlaying = false;
        scoreMesh.text = scoreLeft + ":" + scoreRight;
        yield return new WaitForSeconds(resetDelay);
        isPlaying = true;
        ball.transform.position = Vector3.zero;

    }
}
