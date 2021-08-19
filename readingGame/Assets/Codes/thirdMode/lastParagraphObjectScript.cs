using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class lastParagraphObjectScript : MonoBehaviour {

	public GameObject textBoard;
	
	public bool canSlide = true;
	public float slidingSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		//Slide the text board

		if (canSlide == true) {

			textBoard.GetComponent<RectTransform> ().position += new Vector3 (0,slidingSpeed,0);

		}


	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.tag == "lastPointThirdMode") {
		
			//Stop sliding
			canSlide = false;
			//Hide the sliding text
			GameObject.Find("Text").gameObject.SetActive(false);
			//Show the words
			GameObject.Find ("Camera").GetComponent<cameraThirdMode> ().settingWritten ();

			Debug.Log ("Finish The Reading");
			
		}
	}



}
