using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class secondBattleModeEnemyCode : MonoBehaviour {


	//Essential plane which is our real target
	public bool essentialPlane = false;

	//The borders
	public Transform[] borders = new Transform[4];

	//The plane selects the point on the borders
	public bool selectPoint = false;


	Vector3 unitVector;
    Vector3 newArrivalPoint; //This is the new arrival for the plane's movement
	 Vector3 currentUnitVector; //This is important to learn how many the angle is 

	bool canGo;
	//When the objects are stopped
	public bool stop;

	public float movingSpeed = 1.2f;

	//When the plane is on the another plane's surface
	bool collisionWithAnotherPlane = false;

	//When the plane is still colliding with another plane, we will stop that to change its position (even in second time)
	int dontRepeatThatBounded  = 0;

	//The player has wanted to shoot this plane or not
	public bool hasBeenShooted = false;



	//The panel's written
	public GameObject panelWritten;

	// Use this for initialization
	void Start () {

		if (essentialPlane) {
			selectPoint = true;
			stop = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if ( (selectPoint && !stop) || (collisionWithAnotherPlane && selectPoint) ) {
		
			if (collisionWithAnotherPlane) {
				Debug.Log ("repeat itself");
				Debug.Log ("selectPoint is: " + selectPoint.ToString ());
				Debug.Log ("stop is: " + stop.ToString ());
				Debug.Log ("Collision with another plane bool is: " + collisionWithAnotherPlane.ToString ());
			}

			//newArrivalPoint = borders [Random.Range (0, borders.Length)].position;

			//We make a new arrival point among the borders (if the collision with another plane has not been realized)
			//Otherwise, "Trigger" function has been assign that
			if (!collisionWithAnotherPlane) {
				newArrivalPoint = new Vector3(Random.Range(borders[0].position.x, borders[1].position.x), Random.Range(borders[0].position.y, borders[3].position.y), gameObject.transform.position.z);

			}
		


			float pX, pY, aX, aY; //planeX, planeY, arrivalX, arrivalY

			pX= gameObject.transform.position.x;
			pY= gameObject.transform.position.y;
			aX = newArrivalPoint.x;
			aY = newArrivalPoint.y;

			//Make the unit vector
			unitVector = new Vector3(aX-pX, aY-pY, 0) / Mathf.Sqrt(Mathf.Pow(aX-pX,2) + Mathf.Pow(aY-pY, 2) + 0 );

			canGo = true;

			selectPoint = false;

		}


		if (canGo) {

			//The unit vector for current
			currentUnitVector = new Vector3 (newArrivalPoint.x - gameObject.transform.position.x, newArrivalPoint.y - gameObject.transform.position.y, 0) / Mathf.Sqrt (Mathf.Pow (newArrivalPoint.x - gameObject.transform.position.x, 2) + Mathf.Pow (newArrivalPoint.y - gameObject.transform.position.y, 2));

			//We compare the angle between the unit vector and current unit vector
			//0 Degree: Same way, the plane has not passed the arrival point
			//180 Degree: Inverse way, the plane has passed the arrival point
			float theAngle = Vector3.Angle (unitVector, currentUnitVector);
			if (theAngle == 0) {

				// Debug.Log ("The angle is: " + theAngle.ToString());
				gameObject.transform.position += unitVector * movingSpeed * Time.deltaTime;
			} else {

				//When the collision with another plane situation isn't realized
				if (!collisionWithAnotherPlane) {
					//Select a new point
					canGo = false;
					selectPoint = true;
					//Debug.Log("if situation");
				} else {
					collisionWithAnotherPlane = false; //The exceptional situation should be false now
					Debug.Log("else situation");
					selectPoint = false;
					canGo = false;
					stop = true;
				}


			}

		
		}


		//When the player shoots wrong plane, all plane will be boomed
		if (panelWritten.GetComponent<Text>().text == "Mission Failed") {
		
			gameObject.GetComponent<Animator> ().SetBool ("isShooted", true);

			//Boomed sound
			//gameObject.GetComponent<AudioSource> ().Play ();

			Destroy (gameObject, 0.8f);
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//When the plane is stopped on the another plane's surface, we will give a new place for it
		if (dontRepeatThatBounded < 5 && other.tag == gameObject.tag && stop && other.GetComponent<secondBattleModeEnemyCode>().collisionWithAnotherPlane == false) {
			
			Debug.Log ("Coliision");
			newArrivalPoint = borders [Random.Range (0, borders.Length)].position;
			collisionWithAnotherPlane = true;
			selectPoint = true; //Because we wanna use the NEW "newArrivalPoint" only one time in Update function.
										//We can control that by using selectPoint because we make selectPoint false
										//ıIf you want, you can look at the code
			dontRepeatThatBounded++; //We dont want the plane to repeat trying to change its position
			Debug.Log(dontRepeatThatBounded);
		}

		//Being Shooted
		if (other.tag == "ammoOfPlayerSecondMode" && hasBeenShooted)
			{
				Destroy (other.gameObject);
				
				//CORRECT PLANE OR NOT
			if (essentialPlane) {
				Debug.Log ("KAZANDIN!");

				//Play boombed animation


				other.GetComponent<Animator> ().SetBool ("boomedAmmo", true);

				gameObject.GetComponent<Animator> ().SetBool ("isShooted", true);


				//Boomed sound
				gameObject.GetComponent<AudioSource> ().Play ();

				Destroy (gameObject, 0.8f);




				//Make the panel active and set the items of it
				GameObject.Find("Camera").GetComponent<secondBattleModeCamera>().Panel.SetActive (true);
				GameObject.Find ("tryAgain").SetActive (false);
				GameObject.Find ("levelMenu").SetActive (true);
				GameObject.Find("Written").GetComponent<Text>().text = "Mission Successful!";

				//Open next level
				PlayerPrefs.SetInt(GameObject.Find("Camera").GetComponent<secondBattleModeCamera>().nextLevelName, 1);







			} else {


				Debug.Log ("KAYBETTİN");

				//Play boombed animation


				other.GetComponent<Animator> ().SetBool ("boomedAmmo", true);

				gameObject.GetComponent<Animator> ().SetBool ("isShooted", true);

				//Boomed sound
				gameObject.GetComponent<AudioSource> ().Play ();

				Destroy (gameObject, 0.8f);




				//Make the panel active and set the items of it
				GameObject.Find("Camera").GetComponent<secondBattleModeCamera>().Panel.SetActive(true);
				GameObject.Find ("tryAgain").SetActive (true);
				GameObject.Find ("levelMenu").SetActive (false);
				GameObject.Find("Written").GetComponent<Text>().text = "Mission Failed";

				//If we have been trying for a lot of time to pass the level, the secret button will appear
				if (PlayerPrefs.GetInt ("howManyTimesWeTry") >= 3) {
					GameObject.Find("Camera").GetComponent<secondBattleModeCamera>().passingLevelWithShortWay.SetActive(true);
				}



			}

			}


		//When the plane collides with the panel
		if (other.tag == "thePanel") {
			Time.timeScale = 1;
			Destroy (gameObject);

			Debug.Log ("Collision with the panel");
		}
	}


}
