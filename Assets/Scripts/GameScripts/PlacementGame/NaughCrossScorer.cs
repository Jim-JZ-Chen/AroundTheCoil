using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaughCrossScorer : MonoBehaviour {
    //team 1 = naughts
    int team1Score = 0;
    public TextMesh team1NameText, team1NameShadow,  team1ScoreText, team1ScoreShadow;
    // team 2 = crosses
    int team2Score = 0;
    public TextMesh team2NameText, team2NameShadow, team2ScoreText, team2ScoreShadow;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void AddScore(int team, int score) {
        if (team == 0)
        {
            team1Score += score;
        }
        else {
            team2Score += score;
        }
        UpdateDisplay();
        //add score thingy
    }

    void UpdateDisplay() {
        team1ScoreText.text = team1ScoreShadow.text = "Score: " + team1Score;
        team2ScoreText.text = team2ScoreShadow.text = "Score: " + team2Score;
    }
}
