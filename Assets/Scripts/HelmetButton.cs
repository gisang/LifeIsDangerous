using UnityEngine;
using System.Collections;

public class HelmetButton : MonoBehaviour
{

	public bool helmetOn;
	public ShopDataControl shopData;
	int HELMETCOST;
	public SoundControl soundScript;

	// Use this for initialization
	void Start ()
	{
		if (PlayerPrefs.GetInt ("HelmetOn") == 0) {
			helmetOn = false;
		} else {
			helmetOn = true;
		}
	}

	void Awake ()
	{
		HELMETCOST = 500;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (helmetOn) {
			GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

	void OnMouseDown ()
	{		
		if (!helmetOn) {
			if (shopData.EnoughGene(HELMETCOST)) {
				PlayerPrefs.SetInt ("HelmetOn", 1);
				soundScript.PlayClick ();
				helmetOn = true;
			}
		} else {
			Debug.Log ("You already have your helmet");
		}
	}
}
