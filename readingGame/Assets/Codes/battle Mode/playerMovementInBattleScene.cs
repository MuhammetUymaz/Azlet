using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementInBattleScene : MonoBehaviour {

	//The player
		//Movement
	public GameObject player;
	public GameObject ammo;
	public Rigidbody2D rb;
	public float speed = 0.5f;
	float ammoSpeed = 15f;
	public playerTriggerCode playerCode; //Trigger code from the player



		//Shooting
	public Transform maze;
	public bool canShoot = true;

		//Coefficients
	float coefficientX;
	float coefficientY;

	void Start()
	{
		//We need to edit the speed etc by making them depended on the screen's ratio
		coefficientX = GameObject.Find("Camera").GetComponent<Transform>().localScale.x;
		coefficientY = GameObject.Find ("Camera").GetComponent<Transform> ().localScale.y;

		speed *= coefficientX;
		ammoSpeed *= coefficientY;
	}




	public void rightMovement()
	{
		//rb.velocity = new Vector2 (speed, 0);
		/// We cant change player's direction while that is moving
		if (playerCode.movementOrder < 1) {
			playerCode.movementOrder++; //Increase this
			if (playerCode.movementOrder == 0) {
			
				float pX, pY, oX, Oy; //PlayerX, PlayerY, otherX, otherY
				pX = player.transform.position.x;
				pY = player.transform.position.y;
				oX = playerCode.movementPointsOrigin.position.x;
				Oy = playerCode.movementPointsOrigin.position.y;

				playerCode.unitVector = new Vector3(oX-pX, Oy-pY, 0) / Mathf.Sqrt(Mathf.Pow(oX-pX,2) + Mathf.Pow(Oy-pY, 2) + 0);

				playerCode.targetPoint = playerCode.movementPointsOrigin.position;

				playerCode.canGo = true;
			}

			else if (playerCode.movementOrder == 1) {

				float pX, pY, oX, Oy; //PlayerX, PlayerY, otherX, otherY
				pX = player.transform.position.x;
				pY = player.transform.position.y;
				oX = playerCode.movementPointsRight.position.x;
				Oy = playerCode.movementPointsRight.position.y;

				playerCode.unitVector = new Vector3(oX-pX, Oy-pY, 0) / Mathf.Sqrt(Mathf.Pow(oX-pX,2) + Mathf.Pow(Oy-pY, 2) + 0);

				playerCode.targetPoint = playerCode.movementPointsRight.position;

				playerCode.canGo = true;
			}

		
		}


	}

	public void leftMovement()
	{
		//rb.AddForce (new Vector2 (-speed, 0), ForceMode2D.Impulse);
	///	rb.velocity = new Vector2 (-speed, 0);
		/// We cant change player's direction while that is moving
		if (playerCode.movementOrder > -1) {
			playerCode.movementOrder--; //Decrease this
			if (playerCode.movementOrder == 0) {

				float pX, pY, oX, Oy; //PlayerX, PlayerY, otherX, otherY
				pX = player.transform.position.x;
				pY = player.transform.position.y;
				oX = playerCode.movementPointsOrigin.position.x;
				Oy = playerCode.movementPointsOrigin.position.y;

				playerCode.unitVector = new Vector3(oX-pX, Oy-pY, 0) / Mathf.Sqrt(Mathf.Pow(oX-pX,2) + Mathf.Pow(Oy-pY, 2) + 0);

				playerCode.targetPoint = playerCode.movementPointsOrigin.position;

				playerCode.canGo = true;
			}

			else if (playerCode.movementOrder == -1) {

				float pX, pY, oX, Oy; //PlayerX, PlayerY, otherX, otherY
				pX = player.transform.position.x;
				pY = player.transform.position.y;
				oX = playerCode.movementPointsLeft.position.x;
				Oy = playerCode.movementPointsLeft.position.y;

				playerCode.unitVector = new Vector3(oX-pX, Oy-pY, 0) / Mathf.Sqrt(Mathf.Pow(oX-pX,2) + Mathf.Pow(Oy-pY, 2) + 0);

				playerCode.targetPoint = playerCode.movementPointsLeft.position;

				playerCode.canGo = true;
			}


		}
	}

	public void upMouseFromLeftOrRight()
	{
		rb.velocity = new Vector2 (0, 0);
	}


	public void shootingButton()
	{
		//We can not shoot constantly
		//We must reload the gun
		if (canShoot == true) {

			//Play the shooting sound effect. So make a new game object and provide it to be able to play the sound effect

			//MAKE A CLASS! THıS ıS VERY LOGıCALLY! 
			GameObject soundObject = new GameObject("soundObject"); //MAKE A NEW OBJECT. 
			soundObject.AddComponent<AudioSource> ();
			soundObject.GetComponent<AudioSource> ().clip = GameObject.Find ("player").GetComponent<AudioSource> ().clip;
			soundObject.GetComponent<AudioSource> ().Play ();
			Destroy (soundObject, 1);

			//Make the ammo
			GameObject newAmmo = Instantiate(ammo, new Vector3(player.transform.position.x, player.transform.position.y, -9), Quaternion.EulerAngles(0,0,0));
			newAmmo.GetComponent<Transform> ().transform.localScale = new Vector3 (ammo.transform.lossyScale.x, ammo.transform.lossyScale.y, ammo.transform.lossyScale.z);
			//Push the ammo
			newAmmo.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, ammoSpeed), ForceMode2D.Impulse); 
			canShoot = false;
		}	
	}



	void Update()
	{
		//When we shoot, the "shooting button" will being turned to be reloaded
		if (canShoot == false) {
			gameObject.transform.eulerAngles += new Vector3 (0, 0, 6);
		}
		//When it is reloaded
		if (canShoot == false && gameObject.transform.eulerAngles.z >= 330) {
			gameObject.transform.eulerAngles = new Vector3 (0, 0, 0);
			canShoot = true;
		}



		//Player is moving
		//We dont want the "shooting" button to lead that. So we make that passive for this event
		if (gameObject.name != "shooting") {
		
			if (playerCode.canGo) {

				if (Mathf.Sqrt (Mathf.Pow (player.transform.position.x - playerCode.targetPoint.x, 2) + Mathf.Pow (player.transform.position.y - playerCode.targetPoint.y, 2)) > 0.1f) {
					player.transform.position += playerCode.unitVector * Time.deltaTime* playerCode.movingSpeed;
				} else {
					playerCode.canGo = false;
				}

			}

		}



	}


}
