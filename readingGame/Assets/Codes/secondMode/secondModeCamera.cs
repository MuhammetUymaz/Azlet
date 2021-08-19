using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class secondModeCamera : MonoBehaviour {

	public string[] theWords;

	public GameObject wordShowingTable;


	//The buttons
	public GameObject[] wordButtons;

	public GameObject[] wordButtonsTexts; //TEXT GAMEOBJECT
	//Note: Buttons and their texts need to be ordered respectively for each other

	//Finishing of the showing of the strings
	public bool allTextShown = false;

	//Timer code
	public float waitingTime = 3; //We will wait for 3 seconds
	float rightNow;

	//The properties of the word
	int order = 1;

	//The clicking number;
	public int clickingNumber = 1;

	//Random for random string
	int randomQueue;

	//When the time is up, the "for" loop will not run again. This is very important to destroy the button which we will touch on
	bool timeIsUp = false;

	//Camera Settings
	float width;
	float height;

	public string nextLevelName;


	void Awake()
	{

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
		if (gameObject.GetComponent<AudioSource> ()) {
			gameObject.GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("musicTime");
		}

	}




	// Use this for initialization
	void Start () {
		rightNow = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		//Timer
		if (Time.time >= rightNow + waitingTime) {
			rightNow = Time.time; //We get the right now time
	

			//Select a string from the string array randomly
			selectingRandomString(theWords);
				
			//When the all words have been shown for the user
			if (allTextShown) {
				wordShowingTable.SetActive (false); //Hide the showing table

				//Show all the buttons
					//We need to control the button exists or not. Because we will destroy the button when we will click in the selection (final) 
					//So we made a bool that is called "timeIsUp"
				if (timeIsUp == false) {
					for (int i = 0; i < wordButtons.Length; i++) {
						wordButtons [i].SetActive (true); 
					}
					timeIsUp = true; //So the "for" loop will not be run again
				}

			
			}



		}



	}

	void selectingRandomString(string[] theStrings)
	{
		randomQueue = Random.Range (0, theStrings.Length);
		//If the "strings" are null, we will repeat itself again
		if (theStrings [randomQueue] == null) {

			//Check it. Maybe its items have been consumed
			for (int i = 0; i < theWords.Length; i++) {
				if (theWords [i] != null) //There is a number, we know
				{
					selectingRandomString(theWords); // Repeat itself	
					break;
				}
				else if(i + 1 == theWords.Length && theWords[i] == null) //When we are in the last word and it is empty, too
				{
					
					allTextShown = true; //All text has been shown for the player
				}
			}
		} 
		else {
			//Show the text
			wordShowingTable.GetComponent<Text> ().text = theStrings [randomQueue]; 

			//Delete the text
			//theStrings[randomQueue] = null;

			//Start the function that is required for the buttons
			selectingRandomButtons(wordButtons, theStrings[randomQueue]);



			//Destroy(theStrings[randomQueue]);
			}
		}



	void selectingRandomButtons(GameObject[] theButtons, string theStringForText){
		int randomQueueSecond = Random.Range (0, theButtons.Length);
			//If the "strings" aren null, it means we can use the button to save the it
		if (theButtons [randomQueueSecond].GetComponent<buttons>().number == -15) {
			//Show the text
			wordButtonsTexts[randomQueueSecond].GetComponent<Text>().text = theStringForText; //Write the string on the text of the button
			theButtons [randomQueueSecond].GetComponent<buttons> ().number = order; //Assign 
			order++; //Increasing the order of the "words"
			//Delete the text
			theWords[randomQueue] = null;
		    	}

		else {
			//We dont need to check the buttons are consumed or not. Because number of the strings is same with number of the buttons. In addition, strings are contolled before being controlled of the buttons
			//So if the strings are consumed, this FUNCTION will not be run
			selectingRandomButtons(wordButtons, wordShowingTable.GetComponent<Text>().text); // Repeat itself		
				}
			}
		
			
	}





