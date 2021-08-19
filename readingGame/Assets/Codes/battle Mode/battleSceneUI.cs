using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleSceneUI : MonoBehaviour {

	public string thisLevelName;

	//THESE CODES ARE REQUIRED FOR BATTLE SCENE'S WINNER PANEL
	public void levelMenu()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}
		Application.LoadLevel ("levelMenu");
	}

	public void quitGame()
	{
		Application.Quit ();
	}

	public void tryAgain()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}

		//Save the how many time we try to pass the level
		PlayerPrefs.SetInt("howManyTimesWeTry", PlayerPrefs.GetInt("howManyTimesWeTry") + 1);

		Application.LoadLevel (thisLevelName);
	}


	public void passTheLevelDirectly()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}
		//Open next level
		PlayerPrefs.SetInt(GameObject.Find("Camera").GetComponent<battleSceneCamera>().nextLevelName, 1);

		//Go to level Menu
		Application.LoadLevel ("levelMenu");
	}
}
