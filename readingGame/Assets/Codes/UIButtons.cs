using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour {

	public string levelName; 
	public GameObject playButton;
	public GameObject pauseButton;

	public AudioSource cameraMusic;


	public void Restart()
	{
		Time.timeScale = 1;

		//PlayerPrefs.SetInt("lastText", GameObject.Find("Camera").GetComponent<showingWords>().order); //Save the last level
		PlayerPrefs.SetInt ("lastTextShow", 1); //1 yes show it, 0 show it we will win this level


		PlayerPrefs.SetFloat ("musicTime", cameraMusic.time);

		//Save the how many time we try to pass the level
		PlayerPrefs.SetInt("howManyTimesWeTry", PlayerPrefs.GetInt("howManyTimesWeTry") + 1);

		Application.LoadLevel (levelName);

		//Delete the score
		PlayerPrefs.SetInt("myScore", 0);

		//Restart the WPM
		PlayerPrefs.SetFloat("theWaitingTime", 0.3f);

	}

	public void Quit()
	{
		PlayerPrefs.SetInt ("lastTextShow",0);

		//Set the score
		PlayerPrefs.SetInt ("myScore", 0);
		//Set the music time
		PlayerPrefs.SetFloat ("musicTime", 0);

		//Restart the WPM
		PlayerPrefs.SetFloat("theWaitingTime", 0.3f);

		Debug.Log ("Quit");
		Application.Quit ();

	}

	public void Pause()
	{
		playButton.SetActive (true);
		Time.timeScale = 0;
		gameObject.SetActive (false);

	}

	public void Play()
	{
		Time.timeScale = 1;
		pauseButton.SetActive (true);
		gameObject.SetActive (false);
	}

	public void startTheGame()
	{
		PlayerPrefs.SetFloat ("musicTime", cameraMusic.time);

		//First to play or not (0: First time, 1: Not)
		if (PlayerPrefs.GetInt ("firstTime") == 0) {
			Application.LoadLevel ("beginningScene");
		} 
		else
		{
			Application.LoadLevel ("levelMenu");
		}

	
	}


	public void goToLevelMenu()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}

		Application.LoadLevel("levelMenu");
	}

	public void passTheLevelDirectly()
	{
		//Save the time of the background music
		if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
			PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
		}
		//Open next level
		PlayerPrefs.SetInt(GameObject.Find("Camera").GetComponent<showingWords>().nextLevelName, 1);

		//Go to level Menu
		Application.LoadLevel ("levelMenu");
	}
}
