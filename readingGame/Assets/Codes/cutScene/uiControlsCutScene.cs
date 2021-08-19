using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class uiControlsCutScene : MonoBehaviour {

	[Tooltip("This string is name of the next level. We need to edit it with what next level is")]



	public GameObject finishButtonObject;
	public bool isThisLastDialog = false;
	public bool isThisBeginningDialog = false;

	//Next Level
	public string nextLevelName;


	public void nextButton()
	{
		//Script file
		cameraCutScene theCamera = GameObject.Find ("Camera").GetComponent<cameraCutScene> ();
		//The order from the camera's script
		int order = theCamera.speechOrder;
		Debug.Log ("Speech Order: " + theCamera.speechOrder.ToString ());
		Debug.Log ("First Text Piece: " + theCamera.firstOnesTextPiece.ToString() + "Second Text Piece: " + theCamera.secondOnesTextPiece.ToString());
		if ((theCamera.firstOne [theCamera.firstOnesTextPiece] == "" && theCamera.secondOne [theCamera.secondOnesTextPiece] == "") ||
			(theCamera.secondOne [theCamera.secondOnesTextPiece + 1] == null && theCamera.firstOne [theCamera.firstOnesTextPiece + 1] == null)
		
		
		)
		{ //If the all text have been hided

			//The finish button is active
			Debug.Log("Bak burası çalıştı");

			//Image finishButtonImage = finishButtonObject.GetComponent<Image> ();
			//finishButtonObject.GetComponent<Image> ().color = new Color (finishButtonImage.color.r, finishButtonImage.color.g, finishButtonImage.color.b, 1);

			finishButtonObject.gameObject.SetActive (true);

			//Hide this (with its color)
			//Image thisButtonImage = gameObject.GetComponent<Image>();

			gameObject.SetActive (false);

			//gameObject.GetComponent<Image>().color = new Color(thisButtonImage.color.r, thisButtonImage.color.g, thisButtonImage.color.b, 0);
			


		} else { //If there is text still;

			//Hide the image of the last character
			//Image theImageCharacter = theCamera.characters [order].GetComponent<Image>();
			//theCamera.characters [order].GetComponent<Image> ().color = new Color (theImageCharacter.color.r, theImageCharacter.color.g, theImageCharacter.color.b, 0);

			theCamera.characters [order].gameObject.SetActive (false);

			//Switch the speaker

			theCamera.speechOrder = (order + 1) % 2;

			//Clean the text board
			GameObject.Find ("textBoard").GetComponent<Text> ().text = null;

			//Active the pred function
			theCamera.prepFunction();

			//Hide this (with its color)
			//Image thisButtonImage = gameObject.GetComponent<Image>();

			gameObject.SetActive (false);

			//gameObject.GetComponent<Image>().color = new Color(thisButtonImage.color.r, thisButtonImage.color.g, thisButtonImage.color.b, 0);

		}
			


	}

	public void finishButton()
	{



		//Open next level
		PlayerPrefs.SetInt(nextLevelName, 1);

		if (isThisLastDialog == true) {
			//Save the time of the background music
			if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
				PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
			}

			//Go to final scene
			Application.LoadLevel ("finalScene");
		} 
		else if (isThisBeginningDialog == true)
		{
			//Save the time of the background music
			if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
				PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
			}

			//Go to first dialog
			Application.LoadLevel ("dialogOne");
		}
		else
		{
			//Save the time of the background music
			if (GameObject.Find ("Camera").GetComponent<AudioSource> ()) {
				PlayerPrefs.SetFloat ("musicTime", GameObject.Find ("Camera").GetComponent<AudioSource> ().time);
			}

			//Go to level menu
			Application.LoadLevel("levelMenu");
		}


		//If this is the first time, change it
		if (PlayerPrefs.GetInt ("firstTime") == 0) {
			PlayerPrefs.SetInt ("firstTime", 1);
		} 

	}
		

}
