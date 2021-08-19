using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondBattleModeCamera : MonoBehaviour {

	float rightNow;
	public float[] boundedTime;

	int timeChart = 0; //This works for determining

	[Tooltip("When the all times are up (time chart is completed)")]
	public bool allTimesUp; 

	//Other planes which we move to the scene
	public GameObject[] otherPlanes;

	[Tooltip("We assign all planes in the level")]
	public GameObject[] allPlanes;


	//Screen settings
	float width, height;


	//Panel  for winning or losing
	public GameObject Panel;
	public GameObject  passingLevelWithShortWay; //Short way
	public string nextLevelName; //Next Level Name

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
			


		///////
		//Background Music
		if (gameObject.GetComponent<AudioSource> ()) {
			gameObject.GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("musicTime");
		}



	}





	// Use this for initialization
	void Start () {
		
		//Move the first plane
		rightNow = Time.realtimeSinceStartup;


	}
	
	// Update is called once per frame
	void Update () {

		if (!allTimesUp) {
			
			//First movement pattern for planes
			if (timeChart == 0) { 



				if (Time.realtimeSinceStartup >= rightNow + boundedTime [timeChart]) {



					for (int i = 0; i < otherPlanes.Length; i++) {
						
							otherPlanes [i].GetComponent<secondBattleModeEnemyCode> ().selectPoint = true;
						otherPlanes [i].GetComponent<secondBattleModeEnemyCode> ().stop = false;

						

					}
					timeChart++; //Increase the order of time
					rightNow = Time.realtimeSinceStartup; //Update the right now

					if (timeChart >= boundedTime.Length) {


						allTimesUp = true;

					}
				}
			}

			//Second movement pattern for planes
			if (timeChart == 1) {
				


				if (Time.realtimeSinceStartup >= rightNow + boundedTime [timeChart]) {



					//Stop them

					for (int i = 0; i < allPlanes.Length; i++) {


						allPlanes[i].GetComponent<secondBattleModeEnemyCode> ().selectPoint = false;
						allPlanes[i].GetComponent<secondBattleModeEnemyCode> ().stop = true;
						
						}
							timeChart++; //Increase the order of time
							rightNow = Time.realtimeSinceStartup; //Update the right now
							if (timeChart >= boundedTime.Length) {
								allTimesUp = true;
							}
					}


				}







		} else 
	{

			//WHEN THE ALL PLANES ARE STOPPED (EVENTS), PLAYER WILL BE ABLE TO SHOOT




		}



	}
}
