using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextType : MonoBehaviour {

	string initText;
	TextMesh textMesh;

	float counter;
	bool typing = false;

	public float typeSpeed;
	public float delay;


	public bool playAtStart;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh> ();
		initText = textMesh.text;
		ClearText ();

		if (playAtStart) {
			StartTyping ();
		}
	}

	void ClearText () {
		textMesh.text = "";
	}

	public void StartTyping () {
		if (delay > 0) {
			StartCoroutine (StartTypingDelay ());
		} 
		else 
		{
			typing = true;
		}
	}

	IEnumerator StartTypingDelay () {
		yield return new WaitForSeconds (delay);
		typing = true;
	}


	void EvaluateText (float percent)
	{
		if (counter >= typeSpeed)return;
		int textLength = initText.Length;
		int textPoint = Mathf.RoundToInt(percent * textLength);

		textMesh.text = initText.Substring(0, textPoint);
	}




	void Update () {
		if (typing) {
			Type ();
		}
	}

	public void Type () {
		counter += Time.deltaTime;
		EvaluateText (counter / typeSpeed);
	}
}
