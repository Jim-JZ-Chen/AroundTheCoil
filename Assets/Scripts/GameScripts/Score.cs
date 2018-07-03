using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	public MasterReferences master;

	float baseOffSet;

	bool allowScore = false;
	int score = 0;
	public TextMesh scoreText, scoreText2, leaderText, leaderText2; 

	int currentLeader = 0;
	public List<string> leaderString;
	public List<Color> leaderCol;

	public List<SlinkyFlip> slinkyFlips;

	public List<int> slinkyPos;
	public List<SlinkyDied> slinkyDied;

	bool gameOver = false;


	public void SetNewLeader (int newLeader) {
		currentLeader = newLeader;
		DisplayLeader ();
	}


	void Start () {
		baseOffSet = 0 - GetCamPos ();
		allowScore = true;
	}

	void Update () {
		GetCamPos ();
		if (allowScore && !gameOver) {
			float getCamPosY = (GetCamPos () + baseOffSet) * 5;
			if (score < getCamPosY) {
				score = Mathf.RoundToInt(getCamPosY);
			}
			DisplayScore ();

			CheckLeader ();


		}
		CheckGameEnd ();

	}


	void CheckGameEnd () {
		int slinkiesThatHaveDied = 0;
		for (int slinkyIndex = 0; slinkyIndex < slinkyDied.Count; slinkyIndex++) {
			SlinkyDied slinkyD = slinkyDied [slinkyIndex];
			if (slinkyD.HasDied ()) {
				slinkiesThatHaveDied++;
			}
		}
		if (slinkiesThatHaveDied == 3 && !gameOver) {
			GameOver ();
		}
		if (slinkiesThatHaveDied == 4) {
			GameEnd ();
		}
	}






	void GameOver () {
		gameOver = true;
		leaderText.gameObject.SetActive (false);
		leaderText2.gameObject.SetActive (false);
		master.gameOver.Over (currentLeader, leaderCol[currentLeader]);
	}

	void GameEnd () {
		master.gameOver.End ();
	}


	void CheckLeader () {
		for (int slinkyIndex = 0; slinkyIndex < slinkyFlips.Count; slinkyIndex++) {
			SlinkyFlip flip = slinkyFlips [slinkyIndex];

			if (flip.getControlNode()) {

				slinkyPos [slinkyIndex] = Mathf.RoundToInt(flip.getControlNode ().transform.position.y);

				int slinkyScore = slinkyPos [slinkyIndex];

				if (BetterThanAllOtherScores (slinkyScore, slinkyIndex)) {
					SetNewLeader (slinkyIndex);
				}

			}
		}
	}

	bool BetterThanAllOtherScores (float thisSlinkyScore, int thisSlinkyIndex) {
		for (int slinkyIndex = 0; slinkyIndex < slinkyPos.Count; slinkyIndex++) {
			if (slinkyIndex != thisSlinkyIndex) {
				int otherSlinkyScore = 	slinkyPos[slinkyIndex];
				if (otherSlinkyScore > thisSlinkyScore) {
					return false;
				}
			}
		}
		return true;
	}

	float GetCamPos () {
		return master.camPos.position.y;
	}

	void DisplayScore () {
		scoreText2.text = scoreText.text = "Score: " + score;
	}

	void DisplayLeader () {
		leaderText2.text = leaderText.text = leaderString [currentLeader];
		leaderText.color = leaderCol [currentLeader];
	}
}
