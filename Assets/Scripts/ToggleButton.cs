using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToggleType
{
	BGM,
	SFX,
	CTR
}

public class ToggleButton : MonoBehaviour
{

	public ToggleType type;
	public bool bgm, sfx, control;
	public Sprite toggleon, toggleoff;
	public AudioSource bgmSource, click;
	public SoundControl soundScript;

	void Start() {
		switch(type){
		case ToggleType.BGM:
			bgm = soundScript._bgm;
			SetToggle ("BGM", bgm);
			break;
		case ToggleType.SFX:
			sfx = soundScript._sfx;
			SetToggle ("SFX", sfx);
			break;
		case ToggleType.CTR:
			control = (PlayerPrefs.GetInt ("Controller", 0)==0)?false:true;
			SetToggle ("Controller", control);
			break;
		}
	}

	void OnMouseDown ()
	{
		switch (type) {
		case ToggleType.BGM:
			bgm = !bgm;
			SetToggle ("BGM", bgm);
			if (bgm)
				bgmSource.Play ();
			else
				bgmSource.Pause ();
			break;
		case ToggleType.SFX:
			sfx = !sfx;
			SetToggle ("SFX", sfx);
			break;
		case ToggleType.CTR:
			control = !control;
			SetToggle ("Controller", control);
			break;
		}
	}

	void SetToggle (string type, bool _on)
	{
		SpriteRenderer sp = gameObject.GetComponent<SpriteRenderer> ();
		if (_on) {
			PlayerPrefs.SetInt (type, 1);
			sp.sprite = toggleon;
		} else {
			PlayerPrefs.SetInt (type, 0);
			sp.sprite = toggleoff;
		}
	}
}
