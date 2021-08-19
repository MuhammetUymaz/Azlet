using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondBattleModePlayer : MonoBehaviour {

	public bool rotate;

	bool rotateSlowly;

	bool rotateIncOrDec; //True: Increase, False: Decrease

	public Transform plane;
	Rigidbody2D rb;

	public float rotatingSpeed = 0.4f;

	float angle;
	float anglePiece = 0;


	//Shooting
	public GameObject ammo;
	public Transform muzzle;
	public float ammoSpeed;


	//The player has shooted
	public bool hasShoot = false;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rotate) {

			Vector3 direction = plane.position - transform.position;
			angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			Debug.Log ("The angle is: " + angle.ToString ());
			rotate = false;
			rotateSlowly = true;

			if ((angle - 90) / Mathf.Abs(angle - 90) >= 0) {
				rotateIncOrDec = true;
			} else if ((angle - 90) / Mathf.Abs(angle - 90) < 0) 
			{
				rotateIncOrDec = false;
			}

		}

		if (rotateSlowly) {
			if (rotateIncOrDec == true) {
				Debug.Log ("Increasing!");
				anglePiece += rotatingSpeed * Time.deltaTime;
				rb.rotation = anglePiece;
				if(anglePiece >= angle - 90)
				{
					Debug.Log ("Completed");
					rotateSlowly = false;
					rotate = false;
					Shooting ();
				}
			} else if (rotateIncOrDec == false) {
				Debug.Log ("Decreasing!");
				anglePiece -= rotatingSpeed * Time.deltaTime;
				rb.rotation = anglePiece;
				if(anglePiece < angle - 90)
				{
					Debug.Log ("Completed");
					rotate = false;
					rotateSlowly = false;
					Shooting ();
				}
			}



		}
	}


	void Shooting()
	{
		//Make the new ammo
		GameObject newAmmo = Instantiate (ammo, muzzle.position, muzzle.rotation);
		//newAmmo.transform.rotation = Quaternion.Euler (newAmmo.transform.rotation.x *Mathf.Rad2Deg, newAmmo.transform.rotation.y *Mathf.Rad2Deg, newAmmo.transform.rotation.z + (90 *Mathf.Rad2Deg));

		//Apply force
	//	float angle = Mathf.Atan(-newAmmo.transform.position.y / newAmmo.transform.position.x) * Mathf.Rad2Deg;

	//	Debug.Log ("The Angle Is: " + angle.ToString ());
	//	newAmmo.GetComponent<Rigidbody2D>().velocity += new Vector2(5*Mathf.Cos(angle) , 5*Mathf.Sin(angle) );


		newAmmo.GetComponent<Rigidbody2D> ().AddForce (newAmmo.transform.up * ammoSpeed * Time.deltaTime, ForceMode2D.Impulse);
		//newAmmo.GetComponent<Rigidbody2D>().AddForce (muzzle.transform.forward * ammoSpeed , ForceMode2D.Impulse);
	}
}
