using UnityEngine;
using System.Collections;

public class KitButton : MonoBehaviour
{

	public bool kitOn;
	public ShopDataControl shopData;
	int KITCOST;

	public SoundControl soundScript;

	// Use this for initialization
	void Start ()
	{
		if (PlayerPrefs.GetInt ("KitOn") == 0) {
			kitOn = false;
		} else {
			kitOn = true;
		}
	}

	void Awake ()
	{
		KITCOST = 2000;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (kitOn) {
			GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

	void OnMouseDown ()
	{		
		if (!kitOn) {
			if (shopData.EnoughGene(KITCOST)) {
				soundScript.PlayClick ();
				PlayerPrefs.SetInt ("KitOn", 1);
				kitOn = true;
			}
		} else {
			Debug.Log ("You already have your kit");
		}
	}
}
