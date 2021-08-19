using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class targetWord : MonoBehaviour {

	public showingWords cameraScript; //The camera's script file
	public GameObject Score;

	//My Objects
	public GameObject Text;
	public GameObject Panel;

	void Awake()
	{

			openingThePanel ();


	}

	public void openingThePanel()
	{/*
		if (PlayerPrefs.GetInt ("lastTextShow") == 1) {
			Debug.Log ("1 li durum çalışıyor");
			Time.timeScale = 0;
			//cameraScript.order = PlayerPrefs.GetInt ("lastText");
			//Text.GetComponent<Text> ().text = cameraScript.storeOfTarget [cameraScript.order]; //Show on the Text Board
			PlayerPrefs.SetInt ("lastTextShow", 0);

			PlayerPrefs.SetInt ("runOpeninFuntionInShowingWords", 0);
		} else {*/
			Debug.Log ("0 lı durum çalışıyor");
			Time.timeScale = 0;
			cameraScript.order = Random.Range(0, cameraScript.storeOfWritten.Length-1); //Set the "order"

			/*
			int countTheSucceededLevel = 0;
			while(PlayerPrefs.GetInt (cameraScript.order.ToString () + "Text") == 2)
			{
				countTheSucceededLevel++;
				cameraScript.order = Random.Range(0, cameraScript.storeOfWritten.Length-1);

				if (countTheSucceededLevel >= cameraScript.storeOfWritten.Length) {
					Text.GetComponent<Text> ().text = "Tebrikler! Tüm Seviyeleri Bitirdiniz!";
					for (int wait = 0; wait < 40000000; wait++) {
						//Wait for some time
					}
					Application.Quit ();
					break;

				}

			}
			*/



			Text.GetComponent<Text> ().text = cameraScript.storeOfTarget [cameraScript.order]; //Show on the Text Board
		//}

		Debug.Log ("Opening Çalıştı");


	}


	public void Clicking()
	{


		Time.timeScale = 1; // Go to game
		cameraScript.pausingGameController.SetActive(true); //The Pausing Game controller, show it
		//Start Game****
		cameraScript.settingWritten();

		cameraScript.rightNow = Time.time;

		//cameraScript.Wpm.GetComponent<Text> ().text = "Wpm" + (60f / cameraScript.waitingTime).ToString();
		//**


		Score.SetActive (true);//Show the Score Board
		Panel.SetActive(false); //Close us
	}



	void Update()
	{
		Debug.Log (PlayerPrefs.GetInt ("lastTextShow"));
	}
}
