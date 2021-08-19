using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class answersThirdMode : MonoBehaviour {

	public bool theAnswer;

	public void Clicking()
	{
		if (!theAnswer) {
			Debug.Log ("GAME OVER");
			GameObject.Find ("Camera").GetComponent<cameraThirdMode> ().gameOverPanel.SetActive (true);
			GameObject.Find ("Camera").GetComponent<cameraThirdMode> ().tryAgain.SetActive (true);
			GameObject.Find ("Camera").GetComponent<cameraThirdMode> ().losingWritten.SetActive (true);
			if (PlayerPrefs.GetInt ("howManyTimesWeTry") >= 3) {
				GameObject.Find ("Camera").GetComponent<cameraThirdMode> ().passingLevelWithShortWay.SetActive (true);
			}



		} else {
			Debug.Log ("YOU WIN!");

			GameObject.Find ("Camera").GetComponent<cameraThirdMode> ().gameOverPanel.SetActive (true);
			GameObject.Find ("Camera").GetComponent<cameraThirdMode> ().levelMenu.SetActive (true);
			GameObject.Find ("Camera").GetComponent<cameraThirdMode> ().winnerWritten.SetActive (true);


		}
	}


}
