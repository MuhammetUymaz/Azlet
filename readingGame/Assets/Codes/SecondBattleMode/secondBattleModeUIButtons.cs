using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondBattleModeUIButtons : MonoBehaviour {

	public GameObject playButton;
	public GameObject pauseButton;

	public string thisLevelName;

	public void Pause()
	{
		playButton.SetActive (true);
		Time.timeScale = 0;
		gameObject.SetActive (false);
	}

	public void Play()
	{
		pauseButton.SetActive (true);
		Time.timeScale = 1;
		gameObject.SetActive (false);
	}


	public void levelMenu()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}
		Application.LoadLevel ("levelMenu");
	}


	public void QuitGame()
	{
		Application.Quit ();
	}

	public void passingLevelDirectly()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}
		//Open next level 
		//We use this UI script file for second battle mode and third mode (that's reading mode)
		//For second battle mode
		if (GameObject.Find ("Camera").GetComponent<battleSceneCamera> ()) {
			PlayerPrefs.SetInt (GameObject.Find ("Camera").GetComponent<battleSceneCamera> ().nextLevelName, 1);
		} 
		//For third mode
		else if (GameObject.Find ("Camera").GetComponent<cameraThirdMode> ()) {
			PlayerPrefs.SetInt (GameObject.Find ("Camera").GetComponent<cameraThirdMode> ().nextLevelName, 1);
		}


		//Go to level Menu
		Application.LoadLevel ("levelMenu");
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
}
