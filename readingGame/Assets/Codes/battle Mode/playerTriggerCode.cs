using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTriggerCode : MonoBehaviour {
	
	public AudioClip shootedClip;
	public GameObject passingLevelWithShortWay;


	//Movement Variable
	public int movementOrder = 0; //Left: -1, Origin: 0, Right: +1
	public Transform movementPointsLeft;
	public Transform movementPointsOrigin;
	public Transform movementPointsRight;
	public bool canGo; //This is requirent for the player can move from the point to another
	public Vector3 unitVector;
	public Vector3 targetPoint; //The point is where the player moves to
	public float movingSpeed;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "ammoOfEnemy") {
			gameObject.GetComponent<Animator> ().SetBool ("shooted", true); //Play the animation

			//Make the panel active and set the items of it
			GameObject.Find("Camera").GetComponent<battleSceneCamera>().panel.SetActive(true);
			GameObject.Find ("tryAgain").SetActive (true);
			GameObject.Find ("levelMenu").SetActive (false);
			GameObject.Find("Written").GetComponent<Text>().text = "Mission Failed";

			//If we have been trying for a lot of time to pass the level, the secret button will appear
			if (PlayerPrefs.GetInt ("howManyTimesWeTry") >= 3) {
				passingLevelWithShortWay.SetActive(true);
			}


			//SHOTED SOUND EFFECT
			GameObject shootedSoundEffect = new GameObject("shooted");
			shootedSoundEffect.AddComponent<AudioSource> ();
			shootedSoundEffect.GetComponent<AudioSource> ().clip = shootedClip;
			shootedSoundEffect.GetComponent<AudioSource> ().Play ();
			Destroy (shootedSoundEffect, 1);


			//DESTROYING
			Destroy(other.gameObject);
			Destroy(gameObject, 0.4f);
		}
	}

}
