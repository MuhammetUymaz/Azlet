using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour {

	public int number = -15; //We make this as -15 for the initial. This is important to understand the buttons has been assigned or not

	public GameObject gameOverPanel, winnerPanel; //They are important to be shown after losing or winning


	public GameObject passingLevelWithShortWay;

	public void Clicking()
	{
		if (gameOverPanel.activeSelf == false && winnerPanel.activeSelf == false) { //There must not be any panel about winning or losing
			if (GameObject.Find ("Camera").GetComponent<secondModeCamera> ().clickingNumber == number) { //The order is correct
				Debug.Log("Doğru Seçim");
				Destroy (gameObject);
				GameObject.Find ("Camera").GetComponent<secondModeCamera> ().clickingNumber++;

				//WHEN WIN THE GAME!
				//If we click the all buttons correctly
				//It depends how many buttons on the scene

				//WHEN THE PLAYER WINS THE GAME
				if (number == GameObject.Find ("Camera").GetComponent<secondModeCamera> ().wordButtons.Length) {
					winnerPanel.SetActive (true);
				}

			} else {

				//WHEN THE PLAYER LOSES
				//If we have been trying for a lot of time to pass the level, the secret button will appear
				PlayerPrefs.SetInt ("howManyTimesWeTry", PlayerPrefs.GetInt ("howManyTimesWeTry") + 1);
				if (PlayerPrefs.GetInt ("howManyTimesWeTry") >= 3) {
					passingLevelWithShortWay.SetActive(true);
				}
				Debug.Log (PlayerPrefs.GetInt ("howManyTimesWeTry"));
				gameOverPanel.SetActive(true);
			}
		}

	}


}
