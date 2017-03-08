using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorSwap : MonoBehaviour
{

	public ColorSwap hairScript, eyeScript, skinScript, handScript;
	public ColorSwap w_hairScript, w_eyeScript, w_skinScript, w_handScript;
	public ColorControl colorScript;
	public ShopDataControl shopData;
	public SoundControl soundScript;

	private int SKINCOST, DYECOST, EYECOST;
	private int genePoint;

	void Start ()
	{
		Messenger.AddListener ("GameRestart", ColorSetUp);
		//InitColor ();
	}

	void Awake ()
	{
		SKINCOST = 800;
		DYECOST = 800;
		EYECOST = 500;
	}

	void InitColor ()
	{
		if (gameObject.name.Equals ("haircolorbutton")) {
			hairScript.SetColor ("HAIR");
			w_hairScript.SetColor ("HAIR");
		} else if (gameObject.name.Equals ("eyebutton")) {
			eyeScript.SetColor ("EYE");
			w_eyeScript.SetColor ("EYE");
		} else if (gameObject.name.Equals ("skinbutton")) {
			skinScript.SetColor ("SKIN");
			handScript.SetColor ("SKIN");
			w_skinScript.SetColor ("SKIN");
			w_handScript.SetColor ("SKIN");
		} 
	}

	void OnMouseDown ()
	{
		if (gameObject.name.Equals ("haircolorbutton")) {
			if (shopData.EnoughGene (DYECOST)) {
				haircolor hc;
				do {
					hc = (haircolor)Random.Range (0, (int)haircolor.NUM);
				} while (hc == colorScript.hairColor);
				colorScript.hairColor = hc;
				hairScript.SetColor ("HAIR");
				w_hairScript.SetColor ("HAIR");
				soundScript.PlayClick ();
			}
		} else if (gameObject.name.Equals ("eyebutton")) {
			if (shopData.EnoughGene (EYECOST)) {
				eyecolor ec;
				do {
					ec = (eyecolor)Random.Range (0, (int)eyecolor.NUM);
				} while (ec == colorScript.eyeColor);
				colorScript.eyeColor = ec;
				eyeScript.SetColor ("EYE");
				w_eyeScript.SetColor ("EYE");
				soundScript.PlayClick ();
			}
		} else if (gameObject.name.Equals ("skinbutton")) {
			if (shopData.EnoughGene (SKINCOST)) {
				skincolor sc;
				do {
					sc = (skincolor)Random.Range (0, (int)skincolor.NUM);
				} while (sc == colorScript.skinColor);
				colorScript.skinColor = sc;
				skinScript.SetColor ("SKIN");
				handScript.SetColor ("SKIN");
				w_skinScript.SetColor ("SKIN");
				w_handScript.SetColor ("SKIN");
				soundScript.PlayClick ();
			}
		} 
		ColorSetUp ();
	}

	void ColorSetUp ()
	{
		if (gameObject.name.Equals ("haircolorbutton")) {
			PlayerPrefs.SetInt ("HairColor", (int)colorScript.hairColor);
		} else if (gameObject.name.Equals ("eyebutton")) {
			PlayerPrefs.SetInt ("EyeColor", (int)colorScript.eyeColor);
		} else if (gameObject.name.Equals ("skinbutton")) {
			PlayerPrefs.SetInt ("SkinColor", (int)colorScript.skinColor);
		} 
	}
}
