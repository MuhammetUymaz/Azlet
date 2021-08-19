using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondBattleModePlaneButton : MonoBehaviour {

	public GameObject player;
	public GameObject parent; //Its parent of parent

	public bool rotatePlayer; 


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       /*if (rotatePlayer) {

			Vector3 direction = parent.transform.position - player.transform.position;
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;

			Debug.Log ("Angle is: " + angle.ToString ());

			player.GetComponent<Rigidbody2D> ().rotation += angle;

			Debug.Log ("Rotation value is: " + player.GetComponent<Rigidbody2D> ().rotation.ToString ());

			Debug.Log (rotatePlayer);


		}*/

	}

	public void Clicking()
	{
		//rotatePlayer = true;
		if(parent.GetComponent<secondBattleModeEnemyCode>().stop && !player.GetComponent<secondBattleModePlayer>().hasShoot)
		{
			Debug.Log("Clicking is working");
			player.GetComponent<secondBattleModePlayer>().rotate = true;
			player.GetComponent<secondBattleModePlayer> ().plane = parent.transform;
			player.GetComponent<secondBattleModePlayer> ().hasShoot = true;
			parent.GetComponent<secondBattleModeEnemyCode> ().hasBeenShooted = true;

			//Shooting sound
			player.GetComponent<AudioSource>().Play();
		}

	}


}
