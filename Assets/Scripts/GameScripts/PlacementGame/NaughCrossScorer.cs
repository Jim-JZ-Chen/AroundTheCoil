using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaughCrossScorer : MonoBehaviour {

    //references
    public Spawner spawner;
    public GameObject scoreChangeText;
    //copunt down timer
    public float timer;
    public TextMesh timerText, timerShadow;
    bool counting = true;
    //team 1 = naughts
    int team1Score = 0;
    public TextMesh team1NameText, team1NameShadow,  team1ScoreText, team1ScoreShadow;
    // team 2 = crosses
    int team2Score = 0;
    public TextMesh team2NameText, team2NameShadow, team2ScoreText, team2ScoreShadow;

	// Use this for initialization
	void Start () {
        scoreChangeText = Resources.Load("Prefab/NaughtCross/Increase") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (counting)
        {
            timer -= Time.deltaTime;
            timerText.text = timerShadow.text = Mathf.RoundToInt(timer).ToString();
            if (timer <= 0f)
            {
                EndTheGame();
                counting = false;
            }
        }
	}

    void EndTheGame() {
        spawner.enabled = false;
    }

    public void AddScore(int team, int score) {
        if (team == 0)
        {
            team1Score += score;
            string announcement = score > 0 ? "+" + score : "-" + score;
            ScoreChangeAnimation(team, announcement);
        }
        else {
            team2Score += score;
            string announcement = score > 0 ? "+" + score : "-" + score;
            ScoreChangeAnimation(team, announcement);
        }
        UpdateDisplay();
        //add score thingy
    }

    //not really an animation, didnt have a better term
    void ScoreChangeAnimation(int team, string report) {
        Transform teamTransform = team == 0 ? team1ScoreText.transform : team2ScoreText.transform;
        Instantiate(scoreChangeText, teamTransform.position,teamTransform.rotation);
        

    }


    void UpdateDisplay() {
        team1ScoreText.text = team1ScoreShadow.text = "Score: " + team1Score;
        team2ScoreText.text = team2ScoreShadow.text = "Score: " + team2Score;
    }
}
