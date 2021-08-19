using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cameraThirdMode : MonoBehaviour {

	public string[] storeOfWritten;

	public string written;

	//The word which we wanna cath
	public string[] storeOfTarget;
	public string targetWord;
	//public GameObject targetWordPanel;
	//public targetWord targetWordButton; //Dont confuse yourself. Because this script is from button of the panel


	int letterOrder = 0;

	//Time
	public float rightNow;
	public float waitingTime = 1f;

	//Showing Text & Wpm
	public GameObject textBoard;


	public GameObject[] textBoardReadingOrderly;
	public int  z = 0;//reading Orderly GameObjects' Orders

	public int i = 0;
	public int k = 0;
	bool show = true;

	//Variables
	char[] letter;

	//Total Word That We Read
	int wordWeRead = 1;

	//Stop the game
	int stopContinue = 0;

	//Catching the word
	public bool touchToCatch = false; //To catch the word, touch the screen
	public int passingTheWord; //We pass the word without catching the target word
	public int toleransToMiss; //The tolerans which we can use if we cant catch the word instantly

	//Score
	//public int score;
	//public GameObject scoreTable;

	//Panel 
	public GameObject gameOverPanel;
	public GameObject tryAgain;
	public GameObject levelMenu;
	public GameObject winnerWritten;
	public GameObject losingWritten;

	//Screen Settings
	float width;
	float height;

	public int[,] lettersLimited = new int[2,200000];
	//Note: lettersLimited[1][i] is start of the word
	// lettersLimited[0][i] is end of the word


	//Reading Modes
	public int readingMode = 0; //0: normal mode
	//1: reading orderly


	public int order;


	//Sound Effect
	public soundEffects soundEffectSelection;

	//Answers (Next step's buttons)
	public GameObject answers;


	//Pausing Game Controller
	//public GameObject pausingGameController;

	//Winning Panel
	//public GameObject winningPanel;
	//public GameObject winningScore;
	//public GameObject highScoreInWinningPanel;


	// Use this for initialization

	public string nextLevelName; //This is very important for us to be able to open next level


	//Passing Level with Shortway
	public GameObject passingLevelWithShortWay;


	//Coefficients
	float coefficientX;
	float coefficientY;

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




		//Below line is temporary
		//	scoreTable.GetComponent<Text> ().text = PlayerPrefs.GetInt ("myScore").ToString();

		if (readingMode == 0) {
			textBoard.SetActive (true);
		}
			
		//Coefficient for mathematical expressions
		coefficientX = gameObject.transform.localScale.x;
		coefficientY = gameObject.transform.localScale.y;

	}


	public void settingWritten()
	{
		//Actually we have got ONLY 1 TEXT for each level. By increasing their number, we can increase the randomly selecting text 
		written = storeOfWritten[order];
		targetWord = storeOfTarget [order];



		letterOrder = 0;
		i = 0;
		k = 0;
		z = 0;
		wordWeRead = 1;
		stopContinue = 0;


		while (true) {

			if (letterOrder >= written.Length) {
				break;
			} else if (written [letterOrder] == ' ' || letterOrder+1 == written.Length) {


				//End Letter
				//If the letterOrder isn't on the last word's last letter
				if (letterOrder + 1 != written.Length) {

					lettersLimited [0, i] = letterOrder;
				} 
				//Otherwise, we must take account the last word's last letter
				else {
					lettersLimited [0, i] = letterOrder+1;
				}


				//Start Letter
				//Every start word's location will be "Location of the last END WORD".
				//In first time 
				if (i == 0) {
					lettersLimited [1, i] = 0; //The first start word's location must be 0
				}
				else {
					//Every start word's location will be "Location of the last END WORD".
					lettersLimited [1,i] = lettersLimited [0,i-1];
				}

				//Debug.Log ("Start is: " + (lettersLimited [1, i]).ToString () + " End is: " + (lettersLimited [0, i]).ToString ());

				letterOrder++;
				i++;
			} else {
				letterOrder++;
			}
		}
	}



	// Update is called once per frame
	void Update () {

		if (k <= i) {
			Debug.Log ("iLK ZAMANDAN ÖNCE");
			if (show == true) {

				if (readingMode == 0) {

					for (int j = 0; j < lettersLimited [0,k] - lettersLimited [1,k]; j++) {
						//	Debug.Log ("Start is: " +  (lettersLimited [1,k]).ToString() + " End is: " + (lettersLimited [0,k]).ToString() + "Difference:"  + (lettersLimited [0,k] - lettersLimited [1,k]).ToString() + " j: " + j.ToString() + "k" + k.ToString());
						textBoard.GetComponent<Text> ().text += written [lettersLimited [1,k] + j];
					}

					//Detecting the TARGET WORD

					//If the target word was shown
					if (touchToCatch == true) {
						passingTheWord++;	
					} 
					//When we see the target word and touchToCathc is false (it is false so it means we see this target word first time. Dont forget target word can repeat itself)
					else if(touchToCatch == false && textBoard.GetComponent<Text> ().text == targetWord )
					{
						passingTheWord = 0; //We start on 0 to count

						touchToCatch = true;
					}

					//GAME OVER!
					if (passingTheWord >= toleransToMiss) {

						gameOver ();
						Debug.Log("Game Over!");
					}




					//if (textBoard.GetComponent<Text> ().text == targetWord) {
					//Debug.Log ("OK");
					//}

					show = false;

				}



			}

			if (Time.time >= rightNow + waitingTime) {
				k++; //New word, we increase the "k"
				rightNow = Time.time; //We get the right now time
				show = true; //We will show the text

				//Control the readingModeOrderly
			/*	if (z >= textBoardReadingOrderly.Length -1) {
					textBoardReadingOrderly [z].SetActive (false); //And current text board will be passive
					z = 0; //If z is at the limited of the Lenght of the text boards, it must be go to first text board
				}
				else*/
					z++; //We go to next one



				wordWeRead++; //Total word which we read has been increased
				//Debug.Log ("Total word which we read: " + (wordWeRead).ToString());
			}

			//The increasing/decreasing the game speed
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				waitingTime -= 0.01f;
			//	Wpm.GetComponent<Text> ().text = "Wpm" + (60f / waitingTime).ToString();

			} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
				waitingTime += 0.01f;
			//	Wpm.GetComponent<Text> ().text = "Wpm" + (60f / waitingTime).ToString();
			}

			//Moving front and back for the words
			if (Input.GetKeyDown (KeyCode.W)) {

				//Going to 5 words before
				if (k + 5 < i) {
					k += 5;

					//Increasewords' number which we've read
					wordWeRead += 5;
				} else {
					k = i;

					//Increasewords' number which we've read
					wordWeRead = 0;
				}



			} else if (Input.GetKeyDown (KeyCode.S)) {

				//Going to 5 words later
				if (k - 5 > 0) {
					k -= 5;

					//Decrease words' number which we've read
					wordWeRead -= 5;

				} else {
					k = 0;

					//Decrease words' number which we've read
					wordWeRead = 0;
				}


			}

			//Stop the game
			if (Input.GetKeyDown (KeyCode.Space)) {
				stopContinue++;
				if (stopContinue % 2 == 1) {
					Time.timeScale = 0;
				} else {
					Time.timeScale = 1;
				}

			}


		}
		else if (k > i && !GameObject.Find("Text")) {

			//WHEN THE ALL WORDS HAVE BEEN READ

			//GO TO NEXT STEP

			answers.SetActive (true); //Show the answer buttons

			textBoard.SetActive (false); //Hide the text board

		}

		//Sliding The Text Board
		if (textBoard.GetComponent<Text> ().text.Length >= 300) {

			textBoard.GetComponent<RectTransform> ().position += new Vector3 (0, coefficientY*(1 / waitingTime)/500, 0);


		}


	}
		


	//EVENTS WHEN GAME OVER
	public void gameOver()
	{
		soundEffectSelection.wrongSelection (); //Sound effect

		//Time.timeScale = 0;
		for (int wait = 0; wait < 30000000; wait++) {
			//Wait for some time
		}



		gameOverPanel.SetActive (true);
		textBoard.SetActive (false);
		//scoreTable.SetActive (false);
		Debug.Log ("Game Over!!");

		//If we have been trying for a lot of time to pass the level, the secret button will appear
		if (PlayerPrefs.GetInt ("howManyTimesWeTry") >= 3) {
			passingLevelWithShortWay.SetActive(true);
		}


		Time.timeScale = 0;

		//Save the score if it is the biggest
		/*if (score > PlayerPrefs.GetInt ("highScore")) {
			PlayerPrefs.SetInt ("highScore", score);
		}*/

		//Set the "Current Score" and "High Score" in the game over table
	//	GameObject.Find("currentScore").GetComponent<Text>().text = "Your Score: " + score.ToString();
	//	GameObject.Find ("highScore").GetComponent<Text> ().text = "High Score: " + PlayerPrefs.GetInt ("highScore").ToString();

	}




}
