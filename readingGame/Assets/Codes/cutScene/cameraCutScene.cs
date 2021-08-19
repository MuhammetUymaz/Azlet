using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cameraCutScene : MonoBehaviour
{

	//Images
	public Image[] characters; //Probably we use only two characters in the cut scene 

	//Speech
	public string[] firstOne; //First speaker
	public string[] secondOne; //Second speaker
	public int firstOnesTextPiece;
	public int secondOnesTextPiece;

	public int speechOrder; //0: first speaker, 1: second speaker


	//Text Board
	public GameObject textBoard;

	//Buttons
	public GameObject[] nextButton;
	public GameObject finishButton;

	//Written Effect
	public bool startTime = false;
	public float rightNow;
	public float waitingTime = 0.3f;
	string speakerA; //To call the textFuntion in Update function. They represent
	int iA;
	int speakerOrderA;

	//Screen settings
	float width;
	float height;

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

	// Usse this for initialization
	void Start ()
	{
		prepFunction (); //Call the first function

	}

	public void prepFunction() //This is important for showing the image etc
	{
		//Show the character's image
		characters [speechOrder].gameObject.SetActive(true);


		//characters [speechOrder].GetComponent<Image> ().color = new Color (characters [speechOrder].GetComponent<Image> ().color().r, characters [speechOrder].GetComponent<Image> ().color().g, characters [speechOrder].GetComponent<Image> ().color().b, 1);
		//characters [speechOrder].GetComponent<Image>().SetAllDirty

		//characters [speechOrder].GetComponent<Image> ().color = new Color (characters [speechOrder].GetComponent<Image> ().color.r, characters [speechOrder].GetComponent<Image> ().color.g, characters [speechOrder].GetComponent<Image> ().color.b, 1);



		//Chech which speaker is active and run the function
		if (speechOrder == 0) {//If this is the first speaker

			textFunction (firstOne [firstOnesTextPiece], 0 ,1); //We begin from the last text piece
			//Also the text is starting to be written from letter 0
			//1 represents the speaker who is the "first"
		}
		else if(speechOrder == 1) //If this is the second speaker
		{
			textFunction (secondOne [secondOnesTextPiece], 0, 2); //We begin from the last text piece
			//Also the text is starting to be written from letter 0
			//1 represents the speaker who is the "first"
		}





		//Image nextButtonImage = nextButton [speechOrder].GetComponent<Image>();
		//nextButton [speechOrder].GetComponent<Image> ().color = new Color (nextButtonImage.color.r, nextButtonImage.color.g, nextButtonImage.color.b, 1);
	}

	void Update()
	{
		if (startTime) {
			if (Time.time >= rightNow + waitingTime) {
				//Call the textFuntion
				startTime = false; //Turn off it until we will turn on this again
				textFunction(speakerA, iA, speakerOrderA);
				Debug.Log ("Bak çalışıyor!");
			}
		}
	}

	public void textFunction(string speaker, int i, int speakerOrder)
	{
		//Debug.Log ("Speech order: " + speechOrder.ToString ());
		//Write on the text board
		textBoard.GetComponent<Text> ().text += speaker[i];

		Debug.Log (textBoard.GetComponent<Text> ().text);
		//Writing effect (waiting for little)

		i++;  //We increase the "i" (letter's order)

		if (i != speaker.Length) { //If we arent in the final of the string
			startTime = true; //Start it
			rightNow = Time.time; //Save the time
			//The parametres of the textFunction 
			speakerA = speaker;
			iA = i;
			speakerOrderA = speakerOrder;
			//textFunction (speaker, i, speakerOrder); //Repeat itself with next letter 
		} 
		else //Control the next item of the string is empty or not
		{
			
			//When the text funtion has been ended, show the "NEXT" button
			nextButton [speechOrder].gameObject.SetActive (true);




			switch (speakerOrder) {

			case 1: //First speaker

				firstOnesTextPiece++; //Increase the item of the string (Next item)
				secondOnesTextPiece++; //We need to increase the another one. Because when the first character is speaking, another's field must be empty

				if (firstOne [firstOnesTextPiece] == "") { //Next item is null
					//Debug.Log("Biz şimdi Evet yazısına geçiyoruz");
					//firstOnesTextPiece++; //We dont wanna meet with empty text for next 
					return; //Finish
				} else {

					textBoard.GetComponent<Text> ().text = null; //Clean the board

					prepFunction();
					//textFunction (firstOne [firstOnesTextPiece], 0, 1); //Call the function again
				}
				break;


			case 2: //Second speaker
				
				secondOnesTextPiece++; //Increase the item of the string (Next item)
				firstOnesTextPiece++; //We need to increase the another one. Because when the second character is speaking, another's field must be empty

				if (secondOne [secondOnesTextPiece] == "") { //Next item is null
					//secondOnesTextPiece++; //We dont wanna meet with empty text for next 
					return; //Finish
				} else {
					//Debug.Log ("Şimdi buradayım!");
					//Debug.Log ("Speech Order: " + speechOrder.ToString ());
					textBoard.GetComponent<Text> ().text = null; //Clean the board
					prepFunction();
					//textFunction (secondOne [secondOnesTextPiece], 0, 2); //Call the function again
				}
				break;
			}

 

		}

	}
}
