using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsForSecondMode : MonoBehaviour {

	public string LevelMenuName;
	public string thisLevelNameToPlayAgain;

	public void tryAgain()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}


		Application.LoadLevel (thisLevelNameToPlayAgain);

	}

	public void levelMenu()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}
		//Open next level
		PlayerPrefs.SetInt(GameObject.Find("Camera").GetComponent<secondModeCamera>().nextLevelName, 1);
		Application.LoadLevel ("levelMenu");
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void passTheLevelDirectly()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}
		//Open next level
		PlayerPrefs.SetInt(GameObject.Find("Camera").GetComponent<secondModeCamera>().nextLevelName, 1);

		//Go to level Menu
		Application.LoadLevel ("levelMenu");
	}

}
