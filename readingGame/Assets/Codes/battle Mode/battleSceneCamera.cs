using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleSceneCamera : MonoBehaviour {

	public int numberOfEnemy;
	public GameObject panel;

	//Screen Settings
	float width;
	float height;


	//Background Effect
	public Image bg1;
	public Image bg2;
	public float streamingSpeed = 0.5f;

	//Next Level
	public string nextLevelName;

	//Coefficient
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

	}

	void Start()
	{
		//Make the stream speed depended on screen
		coefficientY = GameObject.Find("Camera").GetComponent<Transform>().localScale.x;
		streamingSpeed *= coefficientY;
	}

	// Update is called once per frame
	void Update () {

		//Background Effect
		bg1.GetComponent<RectTransform>().localPosition -= new Vector3(0,streamingSpeed,0);
		bg2.GetComponent<RectTransform>().localPosition -= new Vector3(0,streamingSpeed,0);
		//Debug.Log ("bg1 Y is: " + bg1.GetComponent<RectTransform> ().localPosition.y.ToString ());
		if (bg1.GetComponent<RectTransform> ().localPosition.y <= -960) {
			//Debug.Log ("Muhammet bak burası çalıştı!");
			bg1.GetComponent<RectTransform> ().localPosition = new Vector3 (bg1.GetComponent<RectTransform> ().localPosition.x, 960, bg1.GetComponent<RectTransform> ().localPosition.z);
		}
		if (bg2.GetComponent<RectTransform> ().localPosition.y <= -960) {
			//Debug.Log ("Muhammet bak burası çalıştı!");
			bg2.GetComponent<RectTransform> ().localPosition = new Vector3 (bg2.GetComponent<RectTransform> ().localPosition.x, 960, bg2.GetComponent<RectTransform> ().localPosition.z);
		}

		//WINNING
		//When the all enemies have been destroyed
		if (numberOfEnemy <= 0) {

			//Make the panel active and set the items of it
			panel.SetActive (true);
			GameObject.Find ("tryAgain").SetActive (false);
			GameObject.Find ("levelMenu").SetActive (true);
			GameObject.Find("Written").GetComponent<Text>().text = "Mission Successful!";

			//Open next level
			PlayerPrefs.SetInt(nextLevelName, 1);

		}
	}
}
