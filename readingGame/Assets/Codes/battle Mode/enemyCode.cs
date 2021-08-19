using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCode : MonoBehaviour {

	//The components
	Rigidbody2D rb;

	//Movement
	[Tooltip("Movement speed of the enemy")]
	public float speed = 0.4f; 

	//Time
	bool canShoot = false;
	float rightNow;
	[Tooltip("This is the time which the enemy need to waiting for to shoot to the player again")]
	public float waitForLitte = 0.5f;

	//Ammo
	public GameObject ammo;
	public Transform maze;
	[Tooltip("Ammo speed of the enemy")]
	public float ammoSpeed = 0.4f;

	//To Follow The Player
	public Transform player;
	[Tooltip("Will this plane follow the player's position?")]
	public bool canFollow = false; //Only one of the enemies can follow the player
	public bool isThatOutOfBackground; //If the planes are out of the background, they will not shoot

	//To make next plane follewer
	public Vector3 unitVector;
	public Vector3 initialPosition;
	public float initialDistance;
	public bool goFollowerPosition = false;
	public float unitVectorSpeed = 0.05f;


	//Sound Effects
	public AudioClip[] shootingClips;
	public AudioClip shootedClip;



	//Coefficients
	float coefficientX;
	float coefficientY;

	// Use this for initialization
	void Start () {
		//Get this rigidbody 
		rb = gameObject.GetComponent<Rigidbody2D> ();
		initialPosition = gameObject.transform.position;
		rightNow = Time.time;

		//Set the speed etc by making them depended on the coefficient;
		coefficientX = GameObject.Find("Camera").GetComponent<Transform>().localScale.x;
		coefficientY = GameObject.Find ("Camera").GetComponent<Transform> ().localScale.y;

		speed *= coefficientX;
		ammoSpeed *= coefficientY;
	}
	
	// Update is called once per frame
	void Update () {

		//Shooting
		if (Time.time >= rightNow + waitForLitte) {
			canShoot = true;
		}

			//EXCEPTION: If the player's plane has been destroyed, the enemy planes will not shoot. That is logically
		if (!GameObject.FindWithTag ("Player")) {
			canShoot = false;
		}

		//If the plane is out of the background, that cant shoot
		//Also its shooting time must be up to be able to shoot (canShoot needs to be true)
		if (canShoot == true && isThatOutOfBackground == false) {
			canShoot = false; //Turn off this
			rightNow = Time.time; //Get the right now



				if (GameObject.FindWithTag ("followingPlane")) {
					//Get its transform
					Transform followerPlane = GameObject.FindWithTag ("followingPlane").GetComponent<Transform>();

					//If we arent behind of the follower plane, we can shoot (very logically)
					//Or if we are the follower plane, we can shoot when the our gun is reloaded
					//THIS BELOW LINE IS VERY IMPORTANT!
				if ((transform.position.x < followerPlane.position.x - 0.45f || transform.position.x > followerPlane.position.x + 0.45f)
					|| gameObject.tag == "followingPlane"
						) 
					{
							//Play the shooting sound effect. So make a new game object and provide it to be able to play the sound effect

							//MAKE A CLASS! THıS ıS VERY LOGıCALLY! 
							GameObject soundObject = new GameObject("soundObjectEnemy"); //MAKE A NEW OBJECT. 
							soundObject.AddComponent<AudioSource> ();
							int randomClip = Random.Range (0, shootingClips.Length);
							soundObject.GetComponent<AudioSource> ().clip = shootingClips [randomClip];
							soundObject.GetComponent<AudioSource> ().Play ();
							Destroy (soundObject, 1);

						//Make the new ammo
					//GameObject newAmmo = Instantiate(ammo, maze.position, Quaternion.EulerAngles(0,0,-3/2f));
					GameObject newAmmo = Instantiate(ammo, maze.position, Quaternion.Euler(0,0,-90));


					//Throw it
						Rigidbody2D ammoRB = newAmmo.GetComponent<Rigidbody2D>();
						ammoRB.AddForce (new Vector2 (0, -1f * ammoSpeed), ForceMode2D.Impulse);

					}
				}

			//If there is any follwe plane, find it




}

		//Following
		if (canFollow == true && goFollowerPosition == false) {
			//When the player's plane is destroy, the code will not try to find it. So we must say "if player != null"
			//If there is the player, the follower plane will follow it
			if (player != null) {    
				if (transform.position.x >= player.position.x + 0.15f || transform.position.x <= player.position.x - 0.15f) { 

					float movementDirection = (transform.position.x - player.position.x) / Mathf.Abs (transform.position.x - player.position.x);

					rb.velocity = new Vector2 (-1f*speed * movementDirection,0);
					//We need to go to along inverse direction with direction to close the X position of the player
				}
			}

		}


		//Go to follower position
		if (goFollowerPosition) {
		
			//transform.position += unitVector * unitVectorSpeed;
			//We use coefficient to make this game available for alomost every portrait telephone
			transform.position += new Vector3(unitVector.x * (coefficientX*unitVectorSpeed), unitVector.y * (coefficientY * unitVectorSpeed));



			float currentDistance = Mathf.Sqrt (Mathf.Pow (initialPosition.x - transform.position.x, 2) + Mathf.Pow (initialPosition.y - transform.position.y, 2));
			Debug.Log ("initialPosition= x: " + initialPosition.x.ToString() + "y: " + initialPosition.y.ToString());
			Debug.Log ("İnitial Distance: " + initialDistance.ToString () + "Current Distance: " + currentDistance.ToString ());
			if (Mathf.Abs(currentDistance - initialDistance) <= 0.15f) {
			//if (currentDistance == initialDistance){
				goFollowerPosition = false;
			}
		}


	}


	void OnTriggerEnter2D(Collider2D other)
	{
		//When the plane is shooted

		if (other.tag == "ammoOfPlayer") {

			//If there is planes
			if (GameObject.FindWithTag("enemyConstantPlane")) {
				//Get the number of the constant plane
				int randomSelection = Random.Range(0,  GameObject.FindGameObjectsWithTag ("enemyConstantPlane").Length - 1);

				//Make the new gameobject to be new follower
				GameObject[] newFollower = GameObject.FindGameObjectsWithTag ("enemyConstantPlane");


				newFollower[randomSelection].tag = "followingPlane"; //Change the tag
				newFollower [randomSelection].GetComponent<enemyCode> ().canFollow = true;//Let follow the player

				//If the new follower has been out of the background, we change it for it can move
				if (newFollower [randomSelection].GetComponent<enemyCode> ().isThatOutOfBackground == true) {
					newFollower[randomSelection].GetComponent<enemyCode>().isThatOutOfBackground = false; 
				}

				//To make next plane follower
				//Actually x1 and x2 need to be position of the new follower
				//Because the new follower will be trying to come here (vector AB = B - A dont forget this fact)
				float x1, x2, y1, y2, distance;
				x2 = transform.position.x;
				y2 = transform.position.y;
				x1 = newFollower [randomSelection].transform.position.x;
				y1 = newFollower [randomSelection].transform.position.y;

				distance = Mathf.Sqrt (Mathf.Pow (x2 - x1, 2) + Mathf.Pow (y2 - y1, 2));
				// vector Unit = (B - A) / distanceBA
				newFollower [randomSelection].GetComponent<enemyCode> ().unitVector = (transform.position - newFollower [randomSelection].transform.position) / distance;
				newFollower [randomSelection].GetComponent<enemyCode> ().initialDistance = distance;
				newFollower [randomSelection].GetComponent<enemyCode> ().goFollowerPosition = true;

			}

			//SHOTED SOUND EFFECT
			GameObject shootedSoundEffect = new GameObject("shootedEnemy");
			shootedSoundEffect.AddComponent<AudioSource> ();
			shootedSoundEffect.GetComponent<AudioSource> ().clip = shootedClip;
			shootedSoundEffect.GetComponent<AudioSource> ().Play ();
			Destroy (shootedSoundEffect, 1);


			//DESTROYING
			Destroy (other.gameObject); //Ammo
			gameObject.GetComponent<Animator>().SetBool("shooted", true); //Play the animation
			GameObject.Find("Camera").GetComponent<battleSceneCamera>().numberOfEnemy--; //Decrease the number of enemy variable in the Camera script
			Destroy (gameObject, 0.2f); //Destroy the object after 0.2 second. Because we want the animation to be shown

		}
	}

}
