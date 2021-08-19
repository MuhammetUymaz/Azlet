using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuCamera : MonoBehaviour {

	 float width;
	 float height;

	// Use this for initialization
	void Awake () {
		//SCREEN SETTıNG
		float newWidth = Screen.width * 1f;
		float newHeight = Screen.height * 1f;

		width = (newWidth / newHeight) / (480f/960f);
		height = (newHeight / newWidth) / (960f/ 480f);

		if (newWidth / newHeight < 0.5f) {
			height -= ((480f / 960f) - (newWidth / newHeight)) * (960f / 480f);
		}
		else if (newWidth / newHeight > 0.5f) {
			height += ((newWidth / newHeight) - (480f/960f)) * (960f / 480f);
		}

		//Debug.Log (PlayerPrefs.GetInt ("gameMode"));
		//Debug.Log (Screen.width + "x" + Screen.height);
		gameObject.GetComponent<Transform>().localScale = new Vector3(width, height, gameObject.GetComponent<Transform>().localScale.z); 

		///////
		//Background Music
		//gameObject.GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("musicTime");

		PlayerPrefs.SetFloat ("theWaitingTime", 0.3f);
	}

}
