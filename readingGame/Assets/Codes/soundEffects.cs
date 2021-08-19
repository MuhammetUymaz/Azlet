using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffects : MonoBehaviour {

	public AudioClip[] correctSelectionEffects;

	public AudioClip wrongSelectiongEffect;

	public AudioClip clickingEffect;

	//AudioSource
	AudioSource source;

	void Start()
	{
		source = gameObject.GetComponent<AudioSource> ();
	}

	public void correctSelection()
	{
		int i = Random.Range (0, correctSelectionEffects.Length - 1);
		source.clip = correctSelectionEffects [i];
		source.Play ();
	}

	public void wrongSelection()
	{
		source.clip = wrongSelectiongEffect;
		source.Play ();
	}

	public void Clicking()
	{
		source.clip = clickingEffect;
		source.Play ();
	}

}
