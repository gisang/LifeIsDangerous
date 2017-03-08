using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorSwap : MonoBehaviour
{

	Texture2D mColorSwapTex;
	Color[] mSpriteColors;
	SpriteRenderer mSpriteRenderer;

	public ColorControl colorScript;

	public void SetColor (string type)
	{
		if (type.Equals ("ALL")) {
			haircolor mHair = colorScript.hairColor;
			Color32 mHairColor = colorScript.HAIRCOLORS [(int)mHair].HAIR;
			Color32 mHairEarColor = colorScript.HAIRCOLORS [(int)mHair].HAIREAR;
			skincolor mSkin = colorScript.skinColor;
			Color32 mSkinColor = colorScript.SKINCOLORS [(int)mSkin].SKIN;
			Color32 mEarColor = colorScript.SKINCOLORS [(int)mSkin].EAR;
			Color32 mChinColor = colorScript.SKINCOLORS [(int)mSkin].CHIN;
			eyecolor mEye = colorScript.eyeColor;
			Color32 mEyeColor = colorScript.EYECOLORS [(int)mEye];
			InitColorSwapTex ();
			SwapColor (SwapIndex.skin, mSkinColor);
			SwapColor (SwapIndex.ear, mEarColor);
			SwapColor (SwapIndex.chin, mChinColor);
			SwapColor (SwapIndex.hair, mHairColor);
			SwapColor (SwapIndex.hairear, mHairEarColor);
			SwapColor (SwapIndex.eye, mEyeColor);
		} else if (type.Equals ("HAIR")) {
			haircolor mHair = colorScript.hairColor;
			Color32 mHairColor = colorScript.HAIRCOLORS [(int)mHair].HAIR;
			Color32 mHairEarColor = colorScript.HAIRCOLORS [(int)mHair].HAIREAR;
			InitColorSwapTex ();
			SwapColor (SwapIndex.hair, mHairColor);
			SwapColor (SwapIndex.hairear, mHairEarColor);
		} else if (type.Equals ("SKIN")) {
			skincolor mSkin = colorScript.skinColor;
			Color32 mSkinColor = colorScript.SKINCOLORS [(int)mSkin].SKIN;
			Color32 mEarColor = colorScript.SKINCOLORS [(int)mSkin].EAR;
			Color32 mChinColor = colorScript.SKINCOLORS [(int)mSkin].CHIN;
			InitColorSwapTex ();
			SwapColor (SwapIndex.skin, mSkinColor);
			SwapColor (SwapIndex.ear, mEarColor);
			SwapColor (SwapIndex.chin, mChinColor);
		} else if (type.Equals ("EYE")) {
			eyecolor mEye = colorScript.eyeColor;
			Color32 mEyeColor = colorScript.EYECOLORS [(int)mEye];
			InitColorSwapTex ();
			SwapColor (SwapIndex.eye, mEyeColor);
		}
		mColorSwapTex.Apply ();
	}

	public void InitColorSwapTex ()
	{
		mSpriteRenderer = GetComponent<SpriteRenderer> ();
		Texture2D colorSwapTex = new Texture2D (256, 1, TextureFormat.RGBA32, false, false);
		colorSwapTex.filterMode = FilterMode.Point;

		for (int i = 0; i < colorSwapTex.width; ++i) {
			colorSwapTex.SetPixel (i, 0, new Color (0.0f, 0.0f, 0.0f, 0.0f));
		}
		colorSwapTex.Apply ();

		mSpriteRenderer.material.SetTexture ("_SwapTex", colorSwapTex);

		mSpriteColors = new Color[colorSwapTex.width];
		mColorSwapTex = colorSwapTex;
	}

	public void SwapColor (SwapIndex index, Color color)
	{
		mSpriteColors [(int)index] = color;
		mColorSwapTex.SetPixel ((int)index, 0, color);
	}
}
