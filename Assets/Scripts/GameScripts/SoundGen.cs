using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGen : MonoBehaviour {

	public GameObject audioObj;


	public void PlaySound (AudioClip clip, float vol, float pitch) {
		GameObject AudioObject = Instantiate (audioObj);
		AudioSource source = AudioObject.GetComponent<AudioSource> ();
		source.volume = vol;
		source.clip = clip;
		source.pitch = pitch;
		source.Play ();

		source.name = "Audio_" + clip.name;
	}
}
