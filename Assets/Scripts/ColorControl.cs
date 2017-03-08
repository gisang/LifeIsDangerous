using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum haircolor
{
	DARKBROWN,
	BROWN,
	GREY,
	WHITE,
	YELLOW,
	BLONDE,
	RED,
	PINK,
	SKYBLUE,
	PASTELPINK,
	ASHGREEN,
	ORANGE,
	NUM
}

public enum eyecolor
{
	BLACK,
	BLUE,
	RED,
	BROWN,
	GREY,
	GREEN,
	WHITE,
	BLUEBACK,
	GOLD,
	PURPLE,
	NUM
}

public enum skincolor
{
	ALBINO,
	WHITE,
	YELLOW1,
	YELLOW2,
	YELLOW3,
	DARK,
	BLACK,
	NIGHTBLACK,
	ORK,
	NUM
}

[System.Serializable]
public class SKINCOLOR {
	public skincolor sc;
	public Color32 SKIN;
	public Color32 CHIN;
	public Color32 EAR;
}

[System.Serializable]
public class HAIRCOLOR {
	public haircolor hc;
	public Color32 HAIR;
	public Color32 HAIREAR;
}

public enum SwapIndex
{
	skin = 237,
	ear = 207,
	chin = 223,
	hair = 22,
	hairear = 4,
	eye = 2,
	mouth = 7
}

public class ColorControl : MonoBehaviour {

	public haircolor hairColor;
	public eyecolor eyeColor;
	public skincolor skinColor;

	public ColorSwap skinScript, hairScript, eyeScript, handScript;
	public ColorSwap w_skinScript, w_hairScript, w_eyeScript, w_handScript;

	public SKINCOLOR[] SKINCOLORS;
	public HAIRCOLOR[] HAIRCOLORS;
	public Color32[] EYECOLORS;

	void Start () {
		hairColor = (haircolor)PlayerPrefs.GetInt ("HairColor", (int)haircolor.DARKBROWN);
		eyeColor = (eyecolor)PlayerPrefs.GetInt ("EyeColor", (int)eyecolor.BLACK);
		skinColor = (skincolor)PlayerPrefs.GetInt ("SkinColor", (int)skincolor.YELLOW2);
		Messenger.AddListener ("GameRestart", RestartSetup);
		skinScript.SetColor ("SKIN");
		handScript.SetColor ("SKIN");
		hairScript.SetColor ("HAIR");
		eyeScript.SetColor ("EYE");
		w_skinScript.SetColor ("SKIN");
		w_handScript.SetColor ("SKIN");
		w_hairScript.SetColor ("HAIR");
		w_eyeScript.SetColor ("EYE");
	}

	void RestartSetup() {
		PlayerPrefs.SetInt ("HairColor", (int)hairColor);
		PlayerPrefs.SetInt ("SkinColor", (int)skinColor);
		PlayerPrefs.SetInt ("EyeColor", (int)eyeColor);
	}

}
