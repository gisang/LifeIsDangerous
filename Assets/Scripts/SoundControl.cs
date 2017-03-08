using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundControl : MonoBehaviour
{
	public bool _bgm, _sfx;
	public AudioSource[] bgm;
	public AudioSource click;

	void Awake() {
		InitSound ();
	}

	void InitSound ()
	{
		int s = PlayerPrefs.GetInt ("SFX", 1);
		if (s == 0)
			_sfx = false;
		else
			_sfx = true;
		int b = PlayerPrefs.GetInt ("BGM", 1);
		if (b == 0)
			_bgm = false;
		else
			_bgm = true;
		if (_bgm) {
			for (int i = 0; i < bgm.Length; i++) {
				bgm [i].Play ();
			}
		}
	}

	public void PlayClick() {
		if (_sfx) {
			click.Play ();
		}
	}
}
