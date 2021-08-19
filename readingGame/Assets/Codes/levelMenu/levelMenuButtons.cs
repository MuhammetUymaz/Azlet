using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class levelMenuButtons : MonoBehaviour, IDragHandler {

	public bool canClick = false;
	public string thisLevelName;

	//Screen Slider
	public bool isSliderScreen;
	public float slidingSpeed = 1;

	//Upper bounded is downer than bottomBounded in the scene.
	//Because the backgrounds will be fallen down when we drag the screen downward
	public GameObject upperBounded, bottomBounded, firstBg, LastBg;


	// Use this for initialization
	void Start () {

		if (gameObject.GetComponent<EventTrigger> ()) {
		//	theEvent = GetComponent<EventTrigger> ();
		}

		//If we open this level
		if (PlayerPrefs.GetInt (thisLevelName) == 1) {

			//Edit the color of this game object
			gameObject.GetComponent<Image> ().color = new Color (gameObject.GetComponent<Image> ().color.r, gameObject.GetComponent<Image> ().color.g, gameObject.GetComponent<Image> ().color.b, 1);
			canClick = true; //We can click anymore
		}

		PlayerPrefs.SetInt ("howManyTimesWeTry", 0);

	}

	public void Click()
	{
		if (canClick) {
			//Go to the level
			Application.LoadLevel (thisLevelName);
		}

	}

	public void OnDrag(PointerEventData eventData)
	{
		if (isSliderScreen &&
			firstBg.GetComponent<RectTransform>().position.y <= bottomBounded.GetComponent<RectTransform>().position.y &&
			LastBg.GetComponent<RectTransform>().position.y >= bottomBounded.GetComponent<RectTransform>().position.y)
		{
			GameObject.Find("backgroundScreen").GetComponent<RectTransform>().position +=  new Vector3(0, eventData.delta.y / 1000 * slidingSpeed, 0);
			//gameObject.GetComponent<RectTransform> ().position += new Vector3(0, eventData.delta.y / 1000, 0);

			//Exceeding Bottom
			if (firstBg.GetComponent<RectTransform> ().position.y > bottomBounded.GetComponent<RectTransform> ().position.y) {
				GameObject.Find ("backgroundScreen").GetComponent<RectTransform> ().position = bottomBounded.GetComponent<RectTransform>().position + new Vector3(0,-0.05f,0);
				Debug.Log ("It has exceeded");
			}

			//Exceeding Upper
			if (LastBg.GetComponent<RectTransform> ().position.y < bottomBounded.GetComponent<RectTransform> ().position.y) {
				Debug.Log ("Burası çalışıyor!");
				GameObject.Find ("backgroundScreen").GetComponent<RectTransform> ().position = upperBounded.GetComponent<RectTransform>().position/* + new Vector3(0,0.05f,0)*/;
			}


		}
		//Debug.Log ("fistBg position Y is: " + firstBg.GetComponent<RectTransform>().position.y.ToString() + "lastBg position Y is:" + LastBg.GetComponent<RectTransform>().position.y.ToString() + "bottom bounded position Y is: " + bottomBounded.GetComponent<RectTransform>().position.y.ToString());



	}
}