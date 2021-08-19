using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchTheScreen : MonoBehaviour {

	public showingWords cameraScript;
	public GameObject scoreText;


	//Winner Panel
	public GameObject winnerPanel;  //If the winner panel is active, we can't touch the screen to not be lose

	public void Touch()
	{



		if (cameraScript.touchToCatch == true && winnerPanel.activeSelf == false) {
			//EVENTS WHEN WE TOUCH THE SCREEN IN TRUE TIME
			//Sound Effect
			cameraScript.soundEffectSelection.correctSelection();
			Time.timeScale = 0;
			for (int wait = 0; wait < 50000000; wait++) {
				//Wait for some time
			}
			Time.timeScale = 1;


			//The score system
			//Debug.Log ("Score: 100"); //Score increase
			cameraScript.score += (int)((60f / cameraScript.waitingTime) / (cameraScript.toleransToMiss * (cameraScript.passingTheWord + 1)));
			Debug.Log ("Score: 100"); //Score increase
			scoreText.GetComponent<Text> ().text = cameraScript.score.ToString ();

			Handheld.Vibrate ();

			//The tour will be restarted
			cameraScript.touchToCatch = false; //It is false
			cameraScript.passingTheWord = 0; 

			//The WPM will be increased
			cameraScript.increasingWpm();






		} 
		//When we click before the target word is shown
		else if (cameraScript.touchToCatch == false && GameObject.Find("targetWordPanel") == null && winnerPanel.activeSelf == false) {



			cameraScript.gameOver ();

		}



	}
		

}
